//Copyright (C) 2015 Redux Dev Team (uo-redux.com) All Rights Reserved.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchBuilder.Modules
{
    internal class LogHandler
    {
        public static string ActionLog
        {
            get
            {
                string path = new FileInfo
                    (System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();
 
                if (!path.EndsWith("\\"))
                    path += "\\";

                path += "Action.log";

                return path;
            }
        }

        public static string ErrorLog
        {
            get
            {
                string path = new FileInfo
                    (System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();

                if (!path.EndsWith("\\"))
                    path += "\\";

                path += "errors.log";

                return path;
            }
        }

        public static string GlobalErrorLog
        {
            get
            {
                string path = new FileInfo
                    (System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();

                if (!path.EndsWith("\\"))
                    path += "\\";

                path += "unhandled.log";

                return path;
            }
        }

        public static void LogActions(string text)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ActionLog, true))
                {
                    writer.WriteLine("[{0:G}] {1}", DateTime.Now, text);
                    writer.WriteLine("/!/\n");
                    writer.Flush(); writer.Close();
                }
            }

            catch { }
        }

        internal static void LogErrors(string text, BuildHandler handler)
        {
            handler.UserInterface.UpdatePatchNotes(text); LogErrors(text);
        }

        internal static void LogErrors(string text)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ErrorLog, true))
                {
                    writer.WriteLine("[{0:G}] {1}", DateTime.Now, text);
                    writer.WriteLine("/!/\n");
                    writer.Flush(); writer.Close();
                }
            }

            catch { }
        }

        public static void LogGlobalErrors(string text)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(GlobalErrorLog, true))
                {
                    writer.WriteLine("@UNHANDLED@");
                    writer.WriteLine("[{0:G}] {1}", DateTime.Now, text);
                    writer.WriteLine("/!/\n");
                    writer.Flush(); writer.Close();
                }
            }

            catch { }
        }
    }
}
