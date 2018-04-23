using System.Text;
using Newtonsoft.Json;

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
            byte[] mesBytes = Encoding.Default.GetBytes(json);
            return mesBytes;
        }

        public static T ToObject<T>(byte[] mesBytes)
        {
            if (mesBytes == null)
            {
                return default(T);
            }

            string json = Encoding.Default.GetString(mesBytes);
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}