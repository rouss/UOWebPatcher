//Copyright (C) 2015 Redux Dev Team (uo-redux.com) All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReduxLauncher.Modules
{
    class PatchFile
    {
        internal long fileBytes = 0;

        internal string filePath = string.Empty;
        internal string fileName = string.Empty;

        internal DateTime creation = DateTime.Now;

        internal string patchVersion = string.Empty;

        internal PatchFile
            (string path, string name, long size, string version)
        {
            fileBytes = size;
            filePath = path;
            fileName = name;

            patchVersion = version;
        }
    }

    class ComparisonHandler
    {
        static Dictionary<string, long> WebCache = new Dictionary<string, long>();
        static Dictionary<string, long> LocalCache = new Dictionary<string, long>();

        static List<PatchFile> DifferenceIndex = new List<PatchFile>();

        internal static void CacheLocalDirectories(LocalDirectory local, PatchHandler handler)
        {
            try
            {
                for (int i = 0; i < local.FileIndex.Count; i++)
                {
                    string name = local.FileIndex[i].
                        FullName.Substring(local.DirectoryPath.Length);

                    if(!LocalCache.ContainsKey(name.Replace("\\", string.Empty)))
                        LocalCache.Add
                            (name.Replace("\\", string.Empty), local.FileIndex[i].Length);

                    handler.UI.UpdatePatchNotes
                        (string.Format("Caching Local Bytes: [{1}] {0}",
                            name.Replace("\\", string.Empty), local.FileIndex[i].Length));

                    handler.UI.UpdateProgressBar();
                }

                for (int n = 0; n < local.subDirectories.Count; n++)
                {
                    CacheLocalDirectories(local.subDirectories[n], handler);
                }
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString(), handler);
            }
        }

        internal static void CacheWebDirectories(WebDirectory web, PatchHandler handler)
        {
            string temp_error_data = string.Empty;

            try
            {
                for (int i = 0; i < web.AddressIndex.Count; i++)
                {
                    temp_error_data += web.AddressIndex[i] + " : " + web.SizeIndex[i].ToString() + " | ";

                    if (!WebCache.ContainsKey(web.AddressIndex[i])) /// Not sure why there's duplicates.. TBE
                        WebCache.Add(web.AddressIndex[i], web.SizeIndex[i]);

                    handler.UI.UpdatePatchNotes
                        (string.Format("Caching Web Bytes: [{1}] {0}", web.AddressIndex[i], web.SizeIndex[i]));

                    handler.UI.UpdateProgressBar();
                }

                for (int n = 0; n < web.SubDirectories.Count; n++)
                {
                    CacheWebDirectories(web.SubDirectories[n], handler);
                }
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString(), handler);
                LogHandler.LogErrors(temp_error_data, handler);
            }
        }
    }
}
