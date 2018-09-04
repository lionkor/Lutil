/*
 * Copyright © 2018 Lion Kortlepel
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lutil.SimpleIni
{
    public partial class SimpleIni
    {
        /// <summary>
        /// Looks up the specified key and returns the corresponding value.
        /// </summary>
        /// <param name="key">Key which holds the value.</param>
        /// <returns>Value held by the key.</returns>
        public string Get (string key)
        {
            var Key = key.Trim ();
            return rawData[Key];
        }
        /// <summary>
        /// Sets a value to a key in a section.
        /// </summary>
        /// <param name="key">Existing key or new key, holding the value</param>
        /// <param name="value">Changed value or new value the key should hold</param>
        /// <param name="saveAll">Whether the data should be saved to the file after the operation. 
        /// If this is set to <see langword="false"/> it is encouraged to call Ini.SaveData() shortly after this in 
        /// order to prevent data loss. Not saving the data to the file before this instance is deleted
        /// will result in loss of any changes. Default: <see langword="true"/></param>
        public void Set (string key, string value, bool saveAll = true)
        {
            if (rawData.ContainsKey (key))
            {
                rawData[key] = value;
            }
            else
            {
                rawData.Add (key, value);
            }

            if (saveAll)
            {
                SaveData ();
            }
        }
        /// <summary>
        /// Adds a value to a key in a section. Adds a new key and section if either does not yet exist.
        /// </summary>
        /// <param name="key">Existing key or new key in the section, holding the value</param>
        /// <param name="value">Changed value or new value the key should hold</param>
        /// <param name="saveAll">Whether the data should be saved to the file after the operation. 
        /// If this is set to <see langword="false"/> it is encouraged to call <see cref="SimpleIni.SaveData()"/> shortly after this in 
        /// order to prevent data loss. Not saving the data to the file before this instance is deleted
        /// will result in loss of any changes. Default: <see langword="true"/></param>
        public void Add (string key, string value, bool saveAll = true)
        {
            Set (key, value, saveAll);
        }
        /// <summary>
        /// Removes a key and its value.
        /// </summary>
        /// <param name="key">Key that is to be removed</param>
        /// <param name="saveAll">Whether the data should be saved to the file after the operation. 
        /// If this is set to <see langword="false"/> it is encouraged to call <see cref="SimpleIni.SaveData()"/> shortly after this in 
        /// order to prevent data loss. Not saving the data to the file before this instance is deleted
        /// will result in loss of any changes. Default: <see langword="true"/></param>
        public void Remove (string key, bool saveAll = true)
        {
            rawData.Remove (key);
            if (saveAll)
            {
                SaveData ();
            }
        }
    }
}
