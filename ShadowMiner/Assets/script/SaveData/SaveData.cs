using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveData
{
    public static class SaveData {
    
        public static void SaveObject<T>(string key, T elt)
        {
            PlayerPrefs.SetString(key, Formatter.ToJson<T>(elt));
            PlayerPrefs.Save();
        }

        public static T GetObject<T>(string key)
        {
            try
            {
                string value = PlayerPrefs.GetString(key, null);
                if (value == "" || value == null)
                {
                    return default(T);
                }
                return Formatter.ToObject<T>(value);
            }
            catch (PlayerPrefsException)
            {
                return default(T);
            }
        }

        public static void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public static int GetInt(string key)
        {
            try
            {
                int value = PlayerPrefs.GetInt(key, 0);
                return value;
            }
            catch (PlayerPrefsException)
            {
                return 0;
            }
        }

        public static void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public static float GetFloat(string key)
        {
            try
            {
                float value = PlayerPrefs.GetFloat(key, 0f);
                return value;
            }
            catch (PlayerPrefsException)
            {
                return 0f;
            }
        }

        public static void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetString(key, value.ToString());
            PlayerPrefs.Save();
        }

        public static bool GetBool(string key)
        {
            try
            {
                string value = PlayerPrefs.GetString(key, "false");
                return bool.Parse(value);
            }
            catch (PlayerPrefsException)
            {
                return false;
            }
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void DeleteKey(string key)
        {
            try
            {
                PlayerPrefs.DeleteKey(key);
            }
            catch(PlayerPrefsException)
            { }
        }
    }
}