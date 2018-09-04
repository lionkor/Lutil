/*
 * Copyright © 2018 Lion Kortlepel
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lutil.Ini
{
    public partial class Ini // READ / WRITE METHODS
    {
        private void writeToData ( string section, string key, string value )
        {
            if (!rawData.ContainsKey (section))
            {
                rawData.Add (section, new Dictionary<string, string> ());
            }
            rawData[section].Add (key, value);
        }
        /// <summary>
        /// Looks up the specified key in the specified section and returns the corresponding value.
        /// </summary>
        /// <param name="section">Section in which the key and value are held.</param>
        /// <param name="key">Key which holds the value.</param>
        /// <returns>Value held by the key in the section. If section or key does not exist returns <see langword="null"/></returns>
        public string Get ( string section, string key )
        {
            var Section = section.Trim ();
            var Key = key.Trim ();

            if (rawData.ContainsKey (Section))
            {
                if (rawData[Section].ContainsKey (Key))
                {
                    return rawData[Section][Key];
                }
            }
            return null;
        }

        /// <summary>
        /// Returns all keys within the section.
        /// </summary>
        public string[] GetAll ( string section )
        {
            List<string> Keys = new List<string> ();
            var Section = section.Trim ();
            if (rawData.ContainsKey (Section))
            {
                foreach (var key in rawData[Section])
                {
                    Keys.Add (key.Key);
                }
                return Keys.ToArray ();
            }
            return null;
        }

        /// <summary>
        /// Get method for comma-seperated-values. This reads the value from the key in the section, 
        /// splits it by comma and returns the values in an array. 
        /// </summary>
        /// <param name="section">Section in which the key and value are held.</param>
        /// <param name="key">Key which holds the value.</param>
        /// <returns>Values held by the key in the section. If section or key does not exist returns <see langword="null"/></returns>
        public string[] GetCSV ( string section, string key )
        {
            // TODO implement AddCSV or similar
            var Section = section.Trim ();
            var Key = key.Trim ();

            if (rawData.ContainsKey (Section))
            {
                if (rawData[Section].ContainsKey (Key))
                {
                    var data = rawData[Section][Key];
                    return data.Split (',');
                }
            }
            return null;
        }
        /// <summary>
        /// Returns a string[] containing all of the section labels in the ini.
        /// </summary>
        public string[] GetSections ()
        {
            return rawData.Keys.ToArray ();
        }
        /// <summary>
        /// Sets a value to a key in a section.
        /// </summary>
        /// <param name="section">Section containing the key</param>
        /// <param name="key">Existing key or new key in the section, holding the value</param>
        /// <param name="value">Changed value or new value the key should hold</param>
        /// <param name="saveAll">Whether the data should be saved to the file after the operation. 
        /// If this is set to <see langword="false"/> it is encouraged to call Ini.SaveData() shortly after this in 
        /// order to prevent data loss. Not saving the data to the file before this instance is deleted
        /// will result in loss of any changes. Default: <see langword="true"/></param>
        /// <param name="allowNewSection">Whether a new section is allowed to be created if it does not already exist.
        /// It is encouraged that Ini.Add(...) is used when adding, however setting this parameter to <see langword="true"/> 
        /// will have the same effect.</param>
        public void Set ( string section, string key, string value, bool saveAll = true, bool allowNewSection = false )
        {
            if (allowNewSection)
            {
                if (!rawData.ContainsKey (section))
                {
                    rawData.Add (section, new Dictionary<string, string> ());
                    rawData[section].Add (key, value);
                }
                else
                {
                    rawData[section][key] = value;
                }
            }
            else
            {
                if (rawData.ContainsKey (section))
                {
                    rawData[section][key] = value;
                }
            }
            if (saveAll)
            {
                SaveData ();
            }
        }
        /// <summary>
        /// Adds a value to a key in a section. Adds a new key and section if either does not yet exist.
        /// </summary>
        /// <param name="section">Section containing the key</param>
        /// <param name="key">Existing key or new key in the section, holding the value</param>
        /// <param name="value">Changed value or new value the key should hold</param>
        /// <param name="saveAll">Whether the data should be saved to the file after the operation. 
        /// If this is set to <see langword="false"/> it is encouraged to call Ini.SaveData() shortly after this in 
        /// order to prevent data loss. Not saving the data to the file before this instance is deleted
        /// will result in loss of any changes. Default: <see langword="true"/></param>
        /// <param name="allowNewSection">Whether a new section is allowed to be created if it does not already exist.
        /// It is encouraged that Ini.Set(...) is used to modify values in existing sections.</param>
        public void Add ( string section, string key, string value, bool saveAll = true, bool allowNewSection = true )
        {
            Set (section, key, value, saveAll, allowNewSection);
        }
        /// <summary>
        /// Removes a key and its value in a specific section.
        /// </summary>
        /// <param name="section">Section containing the key</param>
        /// <param name="key">Key that is to be removed</param>
        /// <param name="saveAll">Whether the data should be saved to the file after the operation. 
        /// If this is set to <see langword="false"/> it is encouraged to call Ini.SaveData() shortly after this in 
        /// order to prevent data loss. Not saving the data to the file before this instance is deleted
        /// will result in loss of any changes. Default: <see langword="true"/></param>
        public void RemoveKey ( string section, string key, bool saveAll = true )
        {
            rawData[section].Remove (key);
            if (saveAll)
            {
                SaveData ();
            }
        }
        /// <summary>
        /// Deletes a section and all the keys and corresponding values inside. Use with caution.
        /// </summary>
        /// <param name="section">Section to be deleted</param>
        /// <param name="saveAll">Whether the data should be saved to the file after the operation. 
        /// If this is set to <see langword="false"/> it is encouraged to call Ini.SaveData() shortly after this in 
        /// order to prevent data loss. Not saving the data to the file before this instance is deleted
        /// will result in loss of any changes. Default: <see langword="true"/></param>
        public void RemoveSection ( string section, bool saveAll = true )
        {
            rawData.Remove (section);
            if (saveAll)
            {
                SaveData ();
            }
        }
    }
}
