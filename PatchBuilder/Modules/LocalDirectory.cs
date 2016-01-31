//Copyright (C) 2015 Redux Dev Team (uo-redux.com) All Rights Reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatchBuilder.Modules
{
    class LocalDirectory
    {
        DirectoryInfo directoryInfo = null;

        internal string DirectoryPath = string.Empty;

        internal List<FileInfo> FileIndex = new List<FileInfo>();
        internal List<LocalDirectory> subDirectories = new List<LocalDirectory>();

        BuildHandler Handler;

        public LocalDirectory(string path, BuildHandler handler)
        {
            Handler = handler;
            DirectoryPath = path;

            directoryInfo = new DirectoryInfo(path);
            BuildFileIndex(directoryInfo);
        }

        internal string DirectoryName()
        {
            string localPath = new FileInfo
                (System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();

            return  DirectoryPath.Substring(localPath.Length);
        }

        internal void BuildFileIndex(DirectoryInfo info)
        {    
            Handler.UserInterface.UpdatePatchNotes
                ("Building Local File Index: \n" + DirectoryPath);

            FileInfo[] file_ = info.GetFiles();
            for (int i = 0; i < file_.Length; i++ )
            { 
                Handler.UserInterface.UpdatePatchNotes
                    ("New Local File Found: " + file_[i].Name);
                FileIndex.Add(file_[i]);
            }

            string[] subs = Directory.GetDirectories
                (DirectoryPath, "*", SearchOption.AllDirectories);

            for (int i = 0; i < subs.Length; i++ )
            {
                Handler.UserInterface.UpdatePatchNotes
                    ("New Local Directory Found: \n" + DirectoryPath);
                subDirectories.Add(new LocalDirectory(subs[i], Handler));
            }
        }
    }
}
