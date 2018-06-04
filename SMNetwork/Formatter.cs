﻿using System.IO;
using System.Text;
using Newtonsoft.Json;

//using Newtonsoft.Json;

namespace SMNetwork
{
    internal static class Formatter
    {
        public static byte[] ToByteArray<T>(T elm)
        {
            if (elm == null)
            {
                return null;
            }
            
            string json = JsonConvert.SerializeObject(elm);            
            byte[] mesBytes = Encoding.UTF8.GetBytes(json);
            return mesBytes;
        }

        public static T ToObject<T>(byte[] mesBytes)
        {
            if (mesBytes == null)
            {
                return default(T);
            }

            string json = Encoding.UTF8.GetString(mesBytes);
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        public static string ToJson<T>(T elm)
        {
            return JsonConvert.SerializeObject(elm);
        }

        public static T JsonToObejct<T>(string json)
        {
            if (json == null)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}