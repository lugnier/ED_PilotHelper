using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace ED_PilotHelper.Helpers
{
    internal enum OptionsKeys
    {
        LogFile
    }

    internal static class DataHelper
    {
        private static Dictionary<String, object> _datas = new Dictionary<string, object>();
        private static String filename = "data.json";

        /// <summary>
        /// load the options in memory
        /// </summary>
        internal static void Load()
        {
            if (!(File.Exists(filename)))
            {
                if (_datas != null)
                {
                    _datas.Clear();
                }
                else
                {
                    _datas = new Dictionary<string, object>();
                }
            }
            else
            {
                _datas = JsonConvert.DeserializeObject<Dictionary<String, object>>(File.ReadAllText(filename));
            }
        }

        /// <summary>
        /// persit the options into a file
        /// </summary>
        internal static void Save()
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(_datas));
        }

        /// <summary>
        /// Add a key/value. Update the key if exists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="saveAfter">save the data after add or update</param>
        internal static void AddOrUpdate(String key, object data, Boolean saveAfter = true)
        {
            if (_datas.ContainsKey(key))
            {
                _datas[key] = data;
            }
            else
            {
                _datas.TryAdd(key, data);
            }
            if (saveAfter)
            {
                Save();
            }
        }

        /// <summary>
        /// try get the key. return null if not exist
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static object Get(String key)
        {
            if (_datas.TryGetValue(key, out object ret))
            {
                return ret;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// get the data in string format
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static String GetString(String key)
        {
            return Get(key)?.ToString();
        }

        /// <summary>
        /// supprime une clef
        /// </summary>
        /// <param name="key"></param>
        /// <param name="saveAfter">sauvegarde après la suppression</param>
        internal static void Delete(String key, Boolean saveAfter = true)
        {
            if (_datas.ContainsKey(key))
            {
                _datas.Remove(key);
            }

            if (saveAfter)
            {
                Save();
            }
        }
    }
}
