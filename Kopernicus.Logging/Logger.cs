/**
 * Kopernicus Planetary System Modifier
 * ====================================
 * Created by: BryceSchroeder and Teknoman117 (aka. Nathaniel R. Lewis)
 * Maintained by: Thomas P., NathanKell and KillAshley
 * Additional Content by: Gravitasi, aftokino, KCreator, Padishar, Kragrathea, OvenProofMars, zengei, MrHappyFace
 * ------------------------------------------------------------- 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301  USA
 * 
 * This library is intended to be used as a plugin for Kerbal Space Program
 * which is copyright 2011-2015 Squad. Your usage of Kerbal Space Program
 * itself is governed by the terms of its EULA, not the license above.
 * 
 * https://kerbalspaceprogram.com
 */

using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Kopernicus
{
    /// <summary>
    /// A message logging class to replace Debug.Log
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Level of the message
        /// </summary>
        public enum MessageType
        {
            Info,
            Warning,
            Error,
            Debug
        }

        /// <summary>
        /// Logger output path
        /// </summary>
        protected static string LogDirectory 
        {
            get { return KSPUtil.ApplicationRootPath + "Logs/"; }
        }

        /// <summary>
        /// The complete path of this log
        /// </summary>
        protected virtual TextWriter loggerStream { get; set; }

        /// <summary>
        /// Write text to the log
        /// </summary>
        public virtual void Log(object o, MessageType type)
        {
            if (loggerStream == null)
                return;

            string level = Enum.GetName(typeof(MessageType), type).ToUpper();
            loggerStream.WriteLine ("[" + level + " " + DateTime.Now.ToString("HH:mm:ss") + "]: " + o);
            Flush();
        }

        /// <summary>
        /// Write text to the log
        /// </summary>
        public void Log(Exception e)
        {
            if (loggerStream == null)
                return;

            Log("Exception Was Recorded: " + e.Message + "\n" + e.StackTrace, MessageType.Warning);
            if(e.InnerException != null)
                Log("Inner Exception Was Recorded: " + e.InnerException.Message + "\n" + e.InnerException.StackTrace, MessageType.Warning);
        }

        /// <summary>
        /// Write the buffer of the log to disk
        /// </summary>
        public void Flush()
        {
            if (loggerStream == null)
                return;
            
            loggerStream.Flush ();
        }

        /// <summary>
        /// Close the logger
        /// </summary>
        public void Close()
        {
            if (loggerStream == null)
                return;

            loggerStream.Flush ();
            loggerStream.Close ();
            loggerStream = null;
        }

        /// <summary>
        /// Create a logger
        /// </summary>
        public Logger (string name, string logHeader = "")
        {
            try
            {
                /// Open the log file (overwrite existing logs)
                string LogFile = LogDirectory + Assembly.GetCallingAssembly().FullName + "/" + name + ".log";
                loggerStream = new StreamWriter(File.Create(LogFile));

                /// Write an opening message
                if (logHeader != "") loggerStream.WriteLine(logHeader);
                Log("Logger \"" + name + "\" was created", MessageType.Debug);
            }
            catch (Exception e) 
            {
                Debug.LogException (e);
            }
        }

        /// <summary>
        /// Cleanup the logger
        /// </summary>
        ~Logger()
        {
            loggerStream.Flush ();
            loggerStream.Close ();
        }

        /// <summary>
        /// Initialize the Logger (i.e. delete old logs) 
        /// </summary>
        static Logger()
        {
            /// Attempt to clean the log directory
            try
            {
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);

                /// Clear out the old log files
                foreach (string file in Directory.GetFiles(LogDirectory))
                {
                    File.Delete(file);
                }
            }
            catch (Exception e) 
            {
                Debug.LogException (e);
                return;
            }
        }
    }
}

