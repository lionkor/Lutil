/*
 * Copyright © 2018 Lion Kortlepel
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace Lutil.SimpleIni
{
    /// <summary>
    /// Manages an INI file with one or no sections.
    /// For an INI file with multiple sections use Ini class.
    /// Keys and sections are case-sensitive.
    /// </summary>
    public partial class SimpleIni
    {
        /// <summary>
        /// The path of this ini file as defined by the constructor of this class.
        /// </summary>
        public string Path
        {
            get;
            private set;
        }

        private Dictionary<string, string> rawData
        {
            get;
            set;
        }
        /// <summary>
        /// The section this ini reads from and writes to. This is either the label of the section or an empty string.
        /// </summary>
        public string Section
        {
            get;
            private set;
        }

        /// <summary>
        /// number of values in the ini
        /// </summary>
        public int Count
        {
            get
            {
                return rawData.Count;
            }
        }

        /// <summary>
        /// Opens the given file or creates a new one and loads the data into memory.
        /// </summary>
        /// <param name="path">The relative or absolute path of the ini file with extension.</param>
        public SimpleIni (string path)
        {
            Path = path;
            rawData = new Dictionary<string, string> ();
            Section = "";
            loadData ();
        }
        /// <summary>
        /// Opens the given file or creates a new one and loads the data into memory.
        /// SimpleIni only provides support for a maximum of one section, which can be named
        /// in this constructor.
        /// </summary>
        /// <param name="path">Relative or absolute path of the ini file with extension.</param>
        /// <param name="section">Section name</param>
        public SimpleIni (string path, string section)
        {
            Path = path;
            rawData = new Dictionary<string, string> ();
            Section = $"[{section}]\n";
            loadData ();
        }

        private void loadData ()
        {
            if (!File.Exists (Path))
            {
                File.WriteAllText (Path, "");
            }

            foreach (var item in File.ReadLines (Path))
            {
                if (item.Length >= 3)
                {
                    if (item[0] != '[')
                    {
                        var s = item.Split ('=');
                        Set (s[0].Trim (), s[1].Trim (), false);
                    }
                    else
                    {
                        var s = item.Trim (new char[] { '[', ']' });
                        Section = $"[{s}]\n";
                    }
                }
            }
        }
        /// <summary>
        /// Forces all data to be reloaded from the file. 
        /// This is redundant unless the file is changed
        /// in an unexpected way (for example by another 
        /// program during runtime).
        /// </summary>
        public void ReloadData ()
        {
            rawData = new Dictionary<string, string> ();
            loadData ();
        }
        /// <summary>
        /// Forces the data to be saved to the ini file.
        /// This is to be used if "saveAll" is set to "false"
        /// in any method.
        /// </summary>
        public void SaveData ()
        {
            writeToFile (Path);
        }
        /// <summary>
        /// Forces the data to be saved to a different ini file.
        /// This does not modify the Path field of this instance.
        /// </summary>
        /// <param name="path">path to write the data to, one time.</param>
        public void SaveData (string path)
        {
            writeToFile (path);
        }

        private void writeToFile (string path)
        {
            string tmp = System.IO.Path.GetTempFileName ();
            string backup = path + ".backup";
            using (StreamWriter file = new StreamWriter (tmp))
            {
                file.Write (Section);
                foreach (var item in rawData)
                {
                    file.WriteLine (item.Key + '=' + item.Value);
                }
            }
            try
            {
                File.Replace (tmp, path, backup);
            }
            catch (Exception e)
            {
                throw new Exception ($"backup file={backup}", e);
            }
            File.Delete (backup);
        }
    }
}
