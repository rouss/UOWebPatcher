//Copyright (C) 2015 Redux Dev Team (uo-redux.com) All Rights Reserved.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PatchBuilder.Modules
{
    internal class WebDirectory
    {
        internal static int CurrentDirectories = 0;

        internal string URL;

        internal List<string> AddressIndex = new List<string>();
        internal List<string> NameIndex = new List<string>();

        internal List<int> SizeIndex = new List<int>();

        internal List<WebDirectory> SubDirectories = new List<WebDirectory>();

        BuildHandler Handler = null;

        public WebDirectory(string url, BuildHandler handler)
        {
            URL = url;
            Handler = handler;
            CurrentDirectories++;
        }

        ~WebDirectory() { CurrentDirectories--; }

        public string DirectoryName()
        {
            string temp = URL.Substring(BuildHandler.MasterUrl.Length); return temp;
        }

        internal void GenerateSizeIndex()
        {
            if (AddressIndex.Count > 0)
            {
                if (DirectoryName() != string.Empty)
                    Handler.UserInterface.UpdatePatchNotes
                        (string.Format("Generating Size Indexing For: {0}", DirectoryName()));

                for (int i = 0; i < AddressIndex.Count; i++)
                {
                    SizeIndex.Add(ParseFileSizeViaHTTP(AddressIndex[i]));
                    Handler.UserInterface.UpdateProgressBar();
                }
            }

            for (int i = 0; i < SubDirectories.Count; i++)
                SubDirectories[i].GenerateSizeIndex();
        }

        internal static int ParseFileSizeViaHTTP(string url)
        {
            int ContentLength = -1;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 3 * 60 * 1000;
                request.KeepAlive = true;
                request.Method = "HEAD";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (!(int.TryParse(response.Headers.Get("Content-Length"), out ContentLength)))
                        LogHandler.LogErrors("Unable to locate size via http for: " + url);
                }
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString());
                if (e.InnerException != null)
                    LogHandler.LogErrors(e.InnerException.ToString());
            }

            return ContentLength;
        }
    }
}
