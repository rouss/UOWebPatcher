//Copyright (C) 2015 Redux Dev Team (uo-redux.com) All Rights Reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReduxLauncher.Modules
{
    internal static class PatchData
    {
        internal static string PatchURL = string.Empty;
        internal static string MasterURL = string.Empty;
        internal static string VersionURL = string.Empty;
        internal static string BackgroundURL = string.Empty;

        internal static Dictionary<string, List<PatchFile>>
            Versions = new Dictionary<string, List<PatchFile>>();

        internal static List<WebDirectory> 
            PatchDirectories = new List<WebDirectory>();

        static byte[] VersionBytes()
        {
            if (VersionURL != string.Empty)
            {
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        return webClient.DownloadData(VersionURL);
                    }
                }

                catch (Exception e)
                {
                    LogHandler.LogErrors(e.ToString());
                }
            }

            else LogHandler.LogErrors("Attempted to gather version bytes from empty URL.");

            return new byte[0];
        }

        internal static string HostVersionString()
        {
            return System.Text.Encoding.UTF8
                .GetString(VersionBytes()).Trim();
        }

        internal static string UpdatesURL { get; set; }
    }

    class XmlHandler
    {
        private static readonly string savePath = "XML";

        private static List<PatchFile> patches = new List<PatchFile>();

        internal static void PopulatePatchDirectories(PatchHandler handler)
        {
            try
            {
                for (int i = 0; i < PatchData.Versions.Count; i++)
                {
                    List<PatchFile> temp = PatchData.Versions[PatchData.Versions.Keys.ElementAt(i)];

                    for (int n = 0; n < temp.Count; n++)
                    {
                        string[] segments = temp[n].filePath.Split('/');
                        int tempLength = 0;

                        for (int z = 0; z < segments.Length - 1; z++)
                            tempLength += segments[z].Length;

                        string master = temp[n].filePath.Substring
                            (0, tempLength + segments.Length - 1);

                        WebDirectory tempDir = new WebDirectory(master, handler);

                        tempDir.AddressIndex.Add(temp[n].filePath);
                        tempDir.NameIndex.Add(temp[n].fileName);

                        PatchData.PatchDirectories.Add(tempDir);
                    }
                }
            }

            catch (Exception e)
            {
                LogHandler.LogErrors(e.ToString());
            }
        }

        internal static bool CanReadSettings(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root;

            try
            {
                doc.Load(path);
                root = doc["Patch-Settings"];

                PatchData.PatchURL = root.GetAttribute("Patch");
                PatchData.MasterURL = root.GetAttribute("Master");
                PatchData.VersionURL = root.GetAttribute("Version");
                PatchData.BackgroundURL = root.GetAttribute("Background");
                PatchData.UpdatesURL = root.GetAttribute("Update");
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); return false; }

            return true;
        }

        private static void ParseLocalDirectory
            (LocalDirectory directory, PatchHandler handler, XmlTextWriter xml)
        {
            IterateFileSizeIndex
                (directory.FileIndex, directory.DirectoryPath, xml);

            for (int i = 0; i < directory.subDirectories.Count; i++ )
            {
                ParseLocalDirectory(directory.subDirectories[i], handler, xml);
            }
        }

        internal static void IterateFileSizeIndex(List<FileInfo> files, string path, XmlTextWriter xml)
        {
            try
            {
                xml.WriteStartElement(path.Replace(":", string.Empty).Replace("\\", "-"));

                for (int n = 0; n < files.Count; n++)
                {
                    xml.WriteStartElement(files[n].Name);
                    xml.WriteAttributeString("Bytes", files[n].Length.ToString("N0"));
                    xml.WriteString(files[n].FullName);
                    xml.WriteEndElement();
                }

                xml.WriteEndElement();
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); }
        }

        internal static void IteratePatchIndex(List<PatchFile> patches, XmlTextWriter xml)
        {
            try
            {
                for (int n = 0; n < patches.Count; n++)
                {
                    xml.WriteStartElement("Patch");
                    xml.WriteAttributeString("Date", patches[n].creation.ToString());
                    xml.WriteAttributeString("Name", patches[n].fileName);
                    xml.WriteAttributeString("Location", patches[n].filePath);
                    xml.WriteAttributeString("Bytes", patches[n].fileBytes.ToString("N0"));
                    xml.WriteString(n.ToString("N0"));
                    xml.WriteEndElement();
                }
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); }
        }

        internal static bool Attempt_ReadPatchFile(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root;

            try
            {
                doc.Load(path);
                root = doc["Patch-Index"];

                foreach (XmlElement ele in root.GetElementsByTagName("Patch"))
                {
                    string version = ele.Attributes["Version"].Value;
                    List<PatchFile> tempPatches = new List<PatchFile>();

                    foreach (XmlElement subEle in ele.GetElementsByTagName("File"))
                    {
                        long bytes;

                        string location = subEle.Attributes["Location"].Value;
                        string name = subEle.Attributes["Name"].Value;

                        if (!long.TryParse(subEle.Attributes["Bytes"].Value.Replace(",", string.Empty), out bytes))
                            LogHandler.LogErrors(string.Format("Unable to parse size of [{0}]", name));

                        tempPatches.Add(new PatchFile(location, name, bytes, version));
                    }

                    if (PatchData.Versions.ContainsKey(version))              
                        for (int i = 0; i < tempPatches.Count; i++)
                            PatchData.Versions[version].Add(tempPatches[i]);
                    
                    else PatchData.Versions.Add(version, tempPatches);
                }

                return true;
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); return false; }
        }
    }
}
