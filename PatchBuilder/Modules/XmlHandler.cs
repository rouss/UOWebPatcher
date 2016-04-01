//Copyright (C) 2015 Redux Dev Team (uo-redux.com) All Rights Reserved.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PatchBuilder.Modules
{
    internal static class PatchHelper
    {
        internal static string PatchURL = string.Empty;
        internal static string MasterURL = string.Empty;
        internal static string VersionURL = string.Empty;
        internal static string BackgroundURL = string.Empty;
        internal static string UpdateLogURL = string.Empty;

        internal static Dictionary<string, List<PatchFile>> 
            Versions = new Dictionary<string, List<PatchFile>>();

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

        internal static string VersionString()
        {
            return System.Text.Encoding.UTF8
                .GetString(VersionBytes()).Trim();
        }
    }

    class XmlHandler
    {
        private static readonly string savePath = "XML";

        internal static void GeneratePatchXML(string xmlPath, List<PatchFile> toPatch)
        {
            try
            {
                if (PatchHelper.Versions.Count == 0)
                {
                    if (Attempt_ReadPatchFile(xmlPath) == false)
                    {
                        if (!PatchHelper.Versions.ContainsKey(toPatch[0].patchVersion))
                            PatchHelper.Versions.Add(toPatch[0].patchVersion, toPatch);
                    }
                }

                else
                {

                    if (!PatchHelper.Versions.ContainsKey(toPatch[0].patchVersion))
                        PatchHelper.Versions.Add(toPatch[0].patchVersion, toPatch);
                }          
                
                GeneratePatchIndex();
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); }
        }

        internal static void AddPatchManually(string xmlPath, PatchFile patch)
        {
            try
            {
                if (PatchHelper.Versions.Count == 0)
                    Attempt_ReadPatchFile(xmlPath);

                AddPatch(patch); GeneratePatchIndex();
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); }
        }

        internal static void AddPatch(PatchFile file) 
        {
            if (PatchHelper.Versions.ContainsKey(file.patchVersion))
            {
                if(!PatchHelper.Versions[file.patchVersion].Contains(file))
                    PatchHelper.Versions[file.patchVersion].Add(file);
            }

            else
            {
                List<PatchFile> newFileList = new List<PatchFile>();
                newFileList.Add(file);

                PatchHelper.Versions.Add(file.patchVersion, newFileList);
            }
        }

        internal static void ClearPatches(string version) { PatchHelper.Versions[version].Clear(); }

        internal static void GenerateSettingsXml()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("settings.xml"))
                {
                    XmlTextWriter xml = new XmlTextWriter(writer);

                    xml.Formatting = Formatting.Indented;
                    xml.IndentChar = '\t';
                    xml.Indentation = 2;

                    xml.WriteStartDocument(true);
                    xml.WriteStartElement("Patch-Settings");

                    xml.WriteAttributeString("Update"    , PatchHelper.UpdateLogURL);
                    xml.WriteAttributeString("Patch"     , PatchHelper.PatchURL);
                    xml.WriteAttributeString("Master"    , PatchHelper.MasterURL);
                    xml.WriteAttributeString("Version"   , PatchHelper.VersionURL);
                    xml.WriteAttributeString("Background", PatchHelper.BackgroundURL);

                    xml.WriteEndElement();
                    xml.WriteEndDocument();
                    xml.Close();
                }
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); }
        }

        internal static void GeneratePatchIndex()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("patch.xml"))
                {
                    XmlTextWriter xml = new XmlTextWriter(writer);

                    xml.Formatting = Formatting.Indented;
                    xml.IndentChar = '\t';
                    xml.Indentation = 2;

                    xml.WriteStartDocument(true);
                    xml.WriteStartElement("Patch-Index");

                    foreach (KeyValuePair<string, List<PatchFile>> kvp in PatchHelper.Versions)
                    {
                        IteratePatchIndex(kvp.Value, kvp.Key, xml);
                    }

                    xml.WriteEndElement();
                    xml.WriteEndDocument();
                    xml.Close();
                }
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); }
        }

        internal static void GenerateXMLSizeIndex(LocalDirectory directory, BuildHandler handler)
        {
            try
            {
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);

                using (StreamWriter writer = new StreamWriter
                    (Path.Combine(savePath, "index.xml")))
                {
                    XmlTextWriter xml = new XmlTextWriter(writer);

                    xml.Formatting = Formatting.Indented;
                    xml.IndentChar = '\t';
                    xml.Indentation = 2;

                    xml.WriteStartDocument(true);
                    xml.WriteStartElement("Size-Index");

                    ParseLocalDirectory(directory, handler, xml);

                    xml.WriteEndElement();
                    xml.WriteEndDocument();
                    xml.Close();
                }
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); }
        }

        private static void ParseLocalDirectory
            (LocalDirectory directory, BuildHandler handler, XmlTextWriter xml)
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

        internal static void IteratePatchIndex(List<PatchFile> patches, string version, XmlTextWriter xml)
        {
            try
            {
                xml.WriteStartElement("Patch");
                xml.WriteAttributeString("Version", version);

                for (int n = 0; n < patches.Count; n++)
                {
                    xml.WriteStartElement("File");
                    xml.WriteAttributeString("Date", patches[n].creation.ToString());
                    xml.WriteAttributeString("Name", patches[n].fileName);
                    xml.WriteAttributeString("Location", patches[n].filePath);
                    xml.WriteAttributeString("Bytes", patches[n].fileBytes.ToString("N0"));
                    xml.WriteEndElement();
                }

                xml.WriteEndElement();
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); }
        }

        internal static bool CanReadSettings(string path)
        {
            if (!File.Exists(path))
                LogHandler.LogErrors("Unable to find settings file.");

            XmlDocument doc = new XmlDocument();
            XmlElement root;

            try
            {
                doc.Load(path);
                root = doc["Patch-Settings"];

                PatchHelper.PatchURL = root.GetAttribute("Patch");
                PatchHelper.MasterURL = root.GetAttribute("Master");
                PatchHelper.VersionURL = root.GetAttribute("Version");
                PatchHelper.BackgroundURL = root.GetAttribute("Background");
                PatchHelper.UpdateLogURL = root.GetAttribute("Update");
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); return false; }

            return true;
        }

        internal static bool Attempt_ReadPatchFile(string path)
        {
            if (!File.Exists(path))
                return false;

            XmlDocument doc = new XmlDocument();
            XmlElement root;

            try
            {
                doc.Load("patch.xml");
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

                      tempPatches.Add( new PatchFile(location, name, bytes, version) );
                    }

                    if (PatchHelper.Versions.ContainsKey(version))
                    {
                        for (int i = 0; i < tempPatches.Count; i++)
                            PatchHelper.Versions[version].Add(tempPatches[i]);
                    }

                    else PatchHelper.Versions.Add(version, tempPatches);
                }

                return true;
            }

            catch (Exception e) { LogHandler.LogErrors(e.ToString()); return false; }
        }
    }
}
