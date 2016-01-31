//Copyright (C) 2015 Redux Dev Team (uo-redux.com) All Rights Reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchBuilder.Modules
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

        internal static void RelayPatchCreation()
        {
            if(DifferenceIndex != null)
                XmlHandler.GeneratePatchXML("patch.xml", DifferenceIndex);
        }

        internal static void CacheDirectories
            (LocalDirectory local, WebDirectory web, BuildHandler handler)
        {
            CacheLocalDirectories(local, handler); CacheWebDirectories(web, handler);

            GenerateDifferenceIndex(handler);
        }

        internal static void GenerateDifferenceIndex(BuildHandler handler)
        {
            try
            {
                DifferenceIndex.Clear();
                foreach (KeyValuePair<string, long> kvp in WebCache)
                {
                    handler.UserInterface.UpdateProgressBar();

                    string[] addressSplit = kvp.Key.Split('/');
                    string name = addressSplit[addressSplit.Length - 1];

                    if (!LocalCache.ContainsKey(name))
                    {
                        handler.UserInterface.UpdatePatchNotes
                            (string.Format("Unable to find local match for: {0}", name));

                        if (!DifferenceIndex.Contains(new PatchFile(kvp.Key, name, kvp.Value, PatchHelper.VersionString())))
                            DifferenceIndex.Add(new PatchFile(kvp.Key, name, kvp.Value, PatchHelper.VersionString()));
                    }

                    else if (LocalCache.ContainsKey(name))
                    {
                        if (LocalCache.ContainsKey(kvp.Key) && kvp.Value != LocalCache[kvp.Key])
                        {
                            handler.UserInterface.UpdatePatchNotes
                                (string.Format("File Size Difference Found For: {0}", kvp.Key));

                            if (!DifferenceIndex.Contains(new PatchFile(kvp.Key, name, kvp.Value, PatchHelper.VersionString())))
                                DifferenceIndex.Add(new PatchFile(kvp.Key, name, kvp.Value, PatchHelper.VersionString()));
                        }
                    }
                }
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString(), handler);
            }
        }

        internal static void CacheLocalDirectories(LocalDirectory local, BuildHandler handler)
        {
            try
            {
                LocalCache.Clear();
                for (int i = 0; i < local.FileIndex.Count; i++)
                {
                    string name = local.FileIndex[i].
                        FullName.Substring(local.DirectoryPath.Length);

                    if(!LocalCache.ContainsKey(name.Replace("\\", string.Empty)))
                        LocalCache.Add
                            (name.Replace("\\", string.Empty), local.FileIndex[i].Length);

                    handler.UserInterface.UpdatePatchNotes
                        (string.Format("Caching Local Bytes: [{1}] {0}",
                            name.Replace("\\", string.Empty), local.FileIndex[i].Length));

                    handler.UserInterface.UpdateProgressBar();
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

        internal static void CacheWebDirectories(WebDirectory web, BuildHandler handler)
        {
            string temp_error_data = string.Empty;

            try
            {
                WebCache.Clear();
                for (int i = 0; i < web.AddressIndex.Count; i++)
                {
                    temp_error_data += web.AddressIndex[i] + " : " + web.SizeIndex[i].ToString() + " | ";

                    if (!WebCache.ContainsKey(web.AddressIndex[i]))
                        WebCache.Add(web.AddressIndex[i], web.SizeIndex[i]);

                    handler.UserInterface.UpdatePatchNotes
                        (string.Format("Caching Web Bytes: [{1}] {0}", web.AddressIndex[i], web.SizeIndex[i]));

                    handler.UserInterface.UpdateProgressBar();
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
