using System;
using SaveData;

namespace SMProgress
{
    [Serializable]
    public class Progress
    {
        public int SoloStats = 0;
        public int MultiStats = 0;
        public string LastUpdate = DateTime.Now.ToString();

        public Progress()
        {

        }

        public static Progress Load()
        {
            Progress p = SaveData.SaveData.GetObject<Progress>("Progress");
            if (p == null)
            {
                return new Progress();
            }
            return p;
        }

        public void Save()
        {
            SaveData.SaveData.SaveObject("Progress", this);
        }

        public static void IncrementSolo(int sceneid)
        {
            Progress p = Progress.Load();
            if (sceneid > p.SoloStats)
            {
                p.SoloStats++;
                p.LastUpdate = DateTime.Now.ToString();
                p.Save();
            }
        }

        public static void IncrementMulti()
        {
            Progress p = Progress.Load();
            p.MultiStats++;
            p.LastUpdate = DateTime.Now.ToString();
            p.Save();
        }

    }
}
