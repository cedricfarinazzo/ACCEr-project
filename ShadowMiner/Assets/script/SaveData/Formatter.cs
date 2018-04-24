using Newtonsoft.Json;
using System.Text;

namespace SaveData
{
    public static class Formatter {

        public static string ToJson<T>(T elm)
        {
            if (elm == null)
            {
                return null;
            }

            string json = JsonConvert.SerializeObject(elm);
            return json;
        }

        public static T ToObject<T>(string json)
        {
            if (json == null || json == "")
            {
                return default(T);
            }

            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

    }
}