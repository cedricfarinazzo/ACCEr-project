using System;

namespace SMProgress
{
    [Serializable]
    public class Progress
    {

        private ProgressMulti multi;
        private ProgressSolo solo;

        public ProgressMulti Multi
        {
            get
            {
                return multi;
            }

            set
            {
                multi = value;
            }
        }

        public ProgressSolo Solo
        {
            get
            {
                return solo;
            }

            set
            {
                solo = value;
            }
        }

        public Progress()
        {
            solo = new ProgressSolo();
            multi = new ProgressMulti();
        }

        public override string ToString()
        {
            return Solo.ToString() + " " + Multi.ToString();
        }

        public static Progress Parse(string text)
        {
            try
            {
                string[] data = text.Split(' ');
                Progress p = new Progress();
                p.Solo = ProgressSolo.Parse(data[0]);
                p.Multi = ProgressMulti.Parse(data[1]);
                return p;
            }
            catch(Exception)
            {
                return new Progress();
            }
        }
    }
}
