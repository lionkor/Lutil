/*
 * Copyright © 2018 Lion Kortlepel
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace Lutil.Ini
{
    /// <summary>
    /// Manages an INI file with multiple sections.
    /// For INI files with one or no sections use SimpleIni class.
    /// Keys and sections are case-sensitive.
    /// </summary>
    public partial class Ini
    {
        /// <summary>
        /// The path of this ini file as defined by the constructor of this class.
        /// </summary>
        public string Path
        {
            get;
            private set;
        }

        private Dictionary<string, Dictionary<string, string>> rawData
        {
            get;
            set;
        }

        /// <summary>
        /// Names of all sections in the ini file.
        /// </summary>
        public string[] Sections
        {
            get
            {
                return GetSections ();
            }
        }

        /// <summary>
        /// number of values in the ini (all sections)
        /// </summary>
        public int Count
        {
            get
            {
                int c = 0;
                foreach (var section in rawData)
                {
                    foreach (var item in rawData[section.Key])
                    {
                        c += 1;
                    }
                }
                return c;
            }
        }

        /// <summary>
        /// number of values in the specific section in the ini
        /// </summary>
        public int CountIn (string section)
        {
            int c = 0;
            foreach (var item in rawData[section])
            {
                c += 1;
            }
            return c;
        }

        /// <summary>
        /// Creates a new ini file or opens an existing one and loads the data into memory.
        /// </summary>
        /// <param name="path">Relative or absolute path of the ini file including extension.</param>
        public Ini (string path)
        {
            Path = path;
            rawData = new Dictionary<string, Dictionary<string, string>> ();
            loadData ();
        }

        private void loadData ()
        {
            string activeSection = null;
            if (!File.Exists (Path))
            {
                File.WriteAllText (Path, "");
            }
            foreach (var item in File.ReadLines (Path))
            {
                if (item.Length >= 3)
                {
                    if (item[0] == '[' && item[item.Length - 1] == ']')
                    {
                        var tmp = item.Trim (new char[] { '[', ']' });
                        activeSection = tmp;
                    }
                    else
                    {
                        if (activeSection != null)
                        {
                            var s = item.Split ('=');
                            writeToData (activeSection, s[0].Trim (), s[1].Trim ());
                        }
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
            rawData = new Dictionary<string, Dictionary<string, string>> ();
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
                foreach (var section in rawData)
                {
                    file.WriteLine ('[' + section.Key + ']');
                    foreach (var item in rawData[section.Key])
                    {
                        file.WriteLine (item.Key + '=' + item.Value);
                    }
                    //file.WriteLine ("\n");
                }
            }
            try
            {
                File.Replace (tmp, path, backup);
            }
            catch (InvalidOperationException e)
            {
                throw new Exception ($"backup file={backup}", e);
            }
            File.Delete (backup);
        }


        /// <summary>
        /// Returns the data in a tree-view type manner. 
        /// It is recommended to test this first.
        /// </summary>
        public override string ToString ()
        {
            string tree = "";
            foreach (var section in rawData)
            {
                tree += $"{section.Key}\n";
                foreach (var item in rawData[section.Key])
                {
                    tree += $"  {item.Key}:\n";
                    tree += $"     {item.Value}\n";
                }
                tree += "\n";
            }
            return tree;
        }
    }
}
