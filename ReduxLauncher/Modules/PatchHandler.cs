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
using System.Timers;

namespace ReduxLauncher.Modules
{

    internal class PatchHandler
    {
        internal static string MasterUrl  { get   { return PatchData.MasterURL;  } }
        internal static string VersionUrl { get   { return PatchData.VersionURL; } }
        internal static string PatchUrl   { get   { return PatchData.PatchURL;   } }
        internal static string UpdatesUrl { get   { return PatchData.UpdatesURL; } }

        static WebClient m_WebClient = new WebClient();

        public static bool IsDownloading()
        {
            return m_WebClient.IsBusy;
        }

        byte[] versionBytes;
        byte[] updateBytes;

        internal PatcherInterface UI;
        internal bool isReady = false;

        int version = -1;

        static int filesDownloaded = 0;

        static WebDirectory webDirectory = null;
        static LocalDirectory localDirectory = null;

        /// <summary>
        /// Object responsible for negotiating modules.
        /// </summary>
        /// <param name="i">Patcher User Interface</param>
        public PatchHandler(PatcherInterface i)
        {
            UI = i;
            if (XmlHandler.CanReadSettings(SettingsLocation()))
            {
                GatherData();
                ObtainCurrentVersion();

                m_WebClient.UseDefaultCredentials = true;

                AppDomain currentDomain = AppDomain.CurrentDomain;

                currentDomain.UnhandledException 
                    += new UnhandledExceptionEventHandler(GlobalErrorHandler);

                m_WebClient.DownloadFileCompleted 
                    += new AsyncCompletedEventHandler(DownloadFinished_Callback);

                m_WebClient.DownloadProgressChanged 
                    += new DownloadProgressChangedEventHandler(DownloadProgress_Callback);

                if (!VersionsMatch())
                    Task.Factory.StartNew(InitializePatcher);

                else
                {
                    UI.ReadyLaunch();
                    isReady = true;
                }
            }
        }

        /// <summary>
        /// Compares version strings.
        /// </summary>
        private bool VersionsMatch()
        {
            UI.UpdatePatchNotes
                (string.Format("Comparing Versions: {0} <?> {1}", LocalVersion(), HostVersion()));

            string local = LocalVersion();
            string host = HostVersion();

            return local == host;
        }

        /// <summary>
        /// Parses version strings to challenge numerical value.
        /// </summary>
        internal static bool ChallengePatch(string delta, string gamma)
        {
            try
            {
                int x, y;

                Int32.TryParse(delta, out x);
                Int32.TryParse(gamma, out y);

                return x > y;
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); return false; }
        }

        /// <summary>
        /// Initializes either installation or patch depending upon state of directory/update.
        /// </summary>
        private void InitializePatcher()
        {
            if (!InitialDownload())
                UI.ReadyDownload();

            else if (InitialDownload())
                UI.ReadyInstall();
        }

        ~PatchHandler() { m_WebClient.Dispose(); }

        private bool VersionExists()    { return File.Exists("version.txt"); }

        private bool ClientExists()     { return File.Exists("client.exe"); }

        internal bool InitialDownload() { return !(ClientExists() && VersionExists()); }



        /// <summary>
        /// Gathers version data from local version.txt
        /// </summary>
        /// <returns>string represent local version.</returns>
        internal static  string LocalVersion()
        {
            string localVersion = string.Empty;

            try
            {
                if (File.Exists("version.txt"))
                {
                    using (FileStream file = new FileStream
                        ("version.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
                            using (StreamReader reader = new StreamReader(file))
                                localVersion = (string)(reader.ReadLine().Trim().ToLower());
                }
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); }

            return localVersion;
        }

        internal static string SettingsLocation()
        {
            string settingsLocation = string.Empty;

            try
            {
                if (File.Exists("settings.cfg"))
                {
                    using (FileStream file = new FileStream
                        ("settings.cfg", FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader reader = new StreamReader(file))
                        settingsLocation = (string)(reader.ReadLine().Trim().ToLower());
                }
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); }

            return settingsLocation;
        }

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

        void ObtainCurrentVersion()
        {
            if (!(Int32.TryParse(HostVersion().Replace(".", string.Empty), out version)))
            {
                LogHandler.LogErrors("Failed to parse Version Data from url.");
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

                    UI.UpdateProgressBar();
                }
            }

            for (int i = 0; i < directory.SubDirectories.Count; i++)
            {
                UI.UpdatePatchNotes
                    ("Parsing Subdirectory: \n" + directory.SubDirectories[i].URL);

                ConstructIndex(directory.SubDirectories[i]);
            }
        }

        void GatherData()
        {
            try
            {
                versionBytes = m_WebClient.DownloadData(VersionUrl);
                updateBytes = m_WebClient.DownloadData(UpdatesUrl);
            }

            catch (Exception e) 
            {
                LogHandler.LogErrors(e.ToString(), this);
            }
        }

        internal string HostVersion()
        {
            return System.Text.Encoding.UTF8.GetString(versionBytes).Trim();
        }

        internal string UpdateNotes()
        {
            return System.Text.Encoding.UTF8.GetString(updateBytes).Trim();
        }

        /// <summary>
        /// Initializes download task.
        /// </summary>
        internal async Task InitializeDownload()
        {
            if (InitialDownload())
            {
                /// Initial Download Gathers all files indescriminately.
                webDirectory = GenerateDirectory(MasterUrl);
                ConstructIndex(webDirectory);
            }

            else /// None Matching Versions && Not Initial Download
            {
                XmlHandler.Attempt_ReadPatchFile(PatchData.PatchURL);
                XmlHandler.PopulatePatchDirectories(this);

                webDirectory = new WebDirectory(MasterUrl, this);

                for (int i = 0; i < PatchData.PatchDirectories.Count; i++)
                    webDirectory.SubDirectories.Add(PatchData.PatchDirectories[i]);
            }

            if (webDirectory != null)
            {
                await DownloadIndexedFiles(webDirectory);
            }

            UI.ReadyLaunch();
            isReady = true;
        }

        WebDirectory GenerateDirectory(string url)
        {
           return new WebDirectory(url, this);        
        }        

        /// <summary>
        /// Initiates DownloadFile() for each address in the directory's index.
        /// </summary>
        /// <param name="o">Directory passed as object to accomodate tasking.</param>
        /// <returns>Async Task</returns>
        internal async Task DownloadIndexedFiles(object o)
        {          
            try
            {
                WebDirectory directory = o as WebDirectory;

                while (directory.AddressIndex.Count > 0)
                {
                    string address = directory.AddressIndex[0];
                    string path = directory.NameIndex[0];

                    await Task.WhenAll(DownloadFile(directory, address)); 
                }

                for (int i = 0; i < directory.SubDirectories.Count; i++)
                    await DownloadIndexedFiles(directory.SubDirectories[i]);
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

                UI.UpdatePatchNotes(string.Format("Downloading File ({0}): " +
                    (address.Remove(address.IndexOf(directory.URL), directory.URL.Length)), filesDownloaded));

                string temp_name = address.Remove(address.IndexOf(directory.URL), directory.URL.Length);

                UI.UpdateFileName(temp_name, 0, 0);

                await m_WebClient.DownloadFileTaskAsync(new Uri(address), path);

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

            Task.Delay(30);

            if (e.Error != null)
            {
                LogHandler.LogErrors(e.Error.ToString(), this);

                if(e.Error.InnerException != null)
                    LogHandler.LogErrors(e.Error.InnerException.ToString(), this);
            }
        }

        void DownloadProgress_Callback(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            UI.UpdatePercentage((int)percentage);
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
                                string[] segments = line.Substring(index).Split('\"');

                                ///Can Parse File Size Here: Add todo
                                if (!segments[1].Contains("/"))
                                {
                                    fileLocations.Add(segments[1]);
                                    UI.UpdatePatchNotes("Web File Found: " + segments[1]);

                                    UI.UpdateProgressBar();
                                }

                                else
                                {
                                    if (segments[1] != @"../")
                                    {
                                        directory.SubDirectories.Add(new WebDirectory(url + segments[1], this));
                                        UI.UpdatePatchNotes("Web Directory Found: " + segments[1].Replace("/", string.Empty));
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
                LogHandler.LogErrors(url, this);
                return null;
            }
        }

        internal void LaunchClient()
        {
            try
            {
                string localPath = new FileInfo
                (System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();

                if (UI.UseRazor)
                {
                    Process client = new Process();
                    client.StartInfo = new ProcessStartInfo(localPath + "/Razor/Razor.exe");

                    client.Start();
                }

                else
                {
                    Process client = new Process();
                    client.StartInfo = new ProcessStartInfo("No_Crypt_Client_2d.exe");
                    client.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;

                    client.Start();
                }

                m_WebClient.Dispose();
                Process.GetCurrentProcess().Kill();
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString());

                if (e.InnerException != null)
                    LogHandler.LogErrors(e.InnerException.ToString());
            }
        }
    }
}
