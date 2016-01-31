//Copyright (C) 2015 Redux Dev Team (uo-redux.com) All Rights Reserved.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchBuilder.Modules
{
    internal class BuildHandler
    {
        static WebClient webClient = new WebClient();

        internal BuilderInterface UserInterface;
        internal bool isReady = false;

        static int filesDownloaded = 0;

        static WebDirectory webDirectory = null;
        static LocalDirectory localDirectory = null;

        internal static string MasterUrl = string.Empty;

        /// <summary>
        /// Object responsible for negotiating modules.
        /// </summary>
        /// <param name="i">Builder User Interface</param>
        public BuildHandler(BuilderInterface i)
        {
            UserInterface = i;

            webClient.UseDefaultCredentials = true;

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalErrorHandler);

            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFinished_Callback);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgress_Callback);
        }

        ~BuildHandler() { webClient.Dispose(); }

        /// <summary>
        /// Generates new local directory object based on path of the .exe
        /// </summary>
        private void QueryLocalDirectory()
        {
            try
            {
                string localPath = new FileInfo
                    (System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();

                localDirectory = new LocalDirectory(localPath, this);
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString(), this); }
        }

        private void GlobalErrorHandler(object sender, UnhandledExceptionEventArgs e)
        {
            LogHandler.LogGlobalErrors(e.ToString());
            LogHandler.LogGlobalErrors(e.ExceptionObject.ToString());
        }

        internal void RelaySettingsGeneration()
        {
            XmlHandler.GenerateSettingsXml();
            UserInterface.UpdatePatchNotes("[Successfully generated new settings.xml]");
        }

        /// <summary>
        /// Relays patch creation from Interface.
        /// </summary>
        internal void RelayPatchGeneration()
        {
            if (!(String.IsNullOrEmpty(PatchHelper.MasterURL)))
            {
                QueryLocalDirectory();

                webDirectory = GenerateDirectory(PatchHelper.MasterURL);
                ConstructIndex(webDirectory);
                webDirectory.GenerateSizeIndex();

                ComparisonHandler.CacheDirectories
                    (localDirectory, webDirectory, this);

                ComparisonHandler.RelayPatchCreation();
            }
        }

        /// <summary>
        /// Uses PaseFolderIndex() to generate an array of url locations for files and subdirectories.
        /// </summary>
        /// <param name="directory">Directory to be affected.</param>
        void ConstructIndex(WebDirectory directory)
        {
            string[] index = ParseFolderIndex(directory.URL, directory);

            if (index != null)
            {
                for (int i = 0; i < index.Length; i++)
                {
                    directory.AddressIndex.Add(directory.URL + index[i]);
                    directory.NameIndex.Add(index[i]);

                    UserInterface.UpdateProgressBar();
                }
            }

            for (int i = 0; i < directory.SubDirectories.Count; i++)
            {
                UserInterface.UpdatePatchNotes
                    ("Parsing Subdirectory: \n" + directory.SubDirectories[i].URL);

                ConstructIndex(directory.SubDirectories[i]);
            }
        }

        WebDirectory GenerateDirectory(string url)
        {
           return new WebDirectory(url, this);        
        }        

        /// <summary>
        /// Initiates DownloadFile() for each address in the directory's index.
        /// </summary>
        /// <param name="o">Directory passed as object to accomodate tasking.</param>
        public async Task DownloadIndexedFiles(object o)
        {          
            try
            {
                WebDirectory directory = o as WebDirectory;

                while (directory.AddressIndex.Count > 0)
                {
                    string address = directory.AddressIndex[0];
                    string path = directory.NameIndex[0];

                    await Task.WhenAll(DownloadFile(directory, address)); 
                    /// Wonder how much overhead when all adds, and why I'm using lol
                }

                for (int i = 0; i < directory.SubDirectories.Count; i++)
                {
                    await DownloadIndexedFiles(directory.SubDirectories[i]);
                }
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString(), this);
            }          
        }

        /// <summary>
        /// Downloads single file at specified address then removes it from the directory's index.
        /// </summary>
        /// <param name="directory">Directory whose index to affect.</param>
        /// <param name="address">Location of file.</param>
        async Task DownloadFile(WebDirectory directory, string address)
        {
            try
            {
                string path = address.Substring(MasterUrl.Length);

                if (path.Contains('/'))
                {
                    string[]  splitPath = path.Split('/');

                    int tempLength = 0;

                    if (splitPath.Length == 1)
                        tempLength = splitPath[0].Length;

                    else
                        for (int i = 0; i < splitPath.Length - 1; i++)
                            tempLength += splitPath[i].Length;

                    string folderName = path.Substring(0, tempLength +1);

                    QueryDirectory(folderName);
                }

                UserInterface.UpdatePatchNotes(string.Format("Downloading File ({0}): " + 
                    (address.Remove(address.IndexOf(MasterUrl), MasterUrl.Length)), filesDownloaded));

                await webClient.DownloadFileTaskAsync(new Uri(address), path);

                directory.NameIndex.RemoveAt(0); directory.AddressIndex.RemoveAt(0);
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString(), this);

                if (e.InnerException != null)
                    LogHandler.LogErrors(e.InnerException.ToString(), this);
            } 
        }

        private void QueryDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private void DownloadFinished_Callback(object sender, AsyncCompletedEventArgs e)
        {
            filesDownloaded++;
            if (e.Error != null)
            {
                LogHandler.LogErrors(e.Error.ToString(), this);
                LogHandler.LogErrors(e.Error.InnerException.ToString(), this);
            }
        }

        void DownloadProgress_Callback(object sender, DownloadProgressChangedEventArgs e)
        {
            UserInterface.UpdateProgressBar();
        }

        /// <summary>
        /// HTTP Web request gathers data based on url parameter to build the web directory object.
        /// </summary>
        /// <param name="url">Initial Parse Location</param>
        /// <param name="directory">Directory object containing properties and methods for indexing and parsing contained folders.</param>
        /// <returns>Returns a string array containing child resource locations parsed from html (href);</returns>
        internal string[] ParseFolderIndex(string url, WebDirectory directory)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 3 * 60 * 1000;
                request.KeepAlive = true;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    List<string> fileLocations = new List<string>(); string line;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            int index = line.IndexOf("<a href=");
                            if (index >= 0)
                            {
                                string[] segments;
                                string[] temp;

                                segments = temp = line.Substring(index).Split('\"');

                                if (!segments[1].Contains("/"))
                                {
                                    fileLocations.Add(segments[1]);
                                    UserInterface.UpdatePatchNotes("Web File Found: " + segments[1]);

                                    UserInterface.UpdateProgressBar();
                                }

                                else
                                {
                                    if (segments[1] != "../")
                                    {
                                        directory.SubDirectories.Add(new WebDirectory(url + segments[1], this));
                                        UserInterface.UpdatePatchNotes("Web Directory Found: " + segments[1].Replace("/", string.Empty));
                                    }
                                }
                            }
                                else if (line.Contains("</pre")) break;
                        }
                    }

                    response.Dispose(); /// After ((line = reader.ReadLine()) != null)
                    return fileLocations.ToArray<string>();
                }

                else return new string[0]; /// !(HttpStatusCode.OK)
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString(), this);
                return null;
            }
        }

        internal void LaunchClient()
        {
            Process client = new Process();
            client.StartInfo = new ProcessStartInfo("client.exe");
            client.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            client.StartInfo.Arguments = "-n";
            client.Start();

            webClient.Dispose();
            Process.GetCurrentProcess().Kill();
        }
    }
}
