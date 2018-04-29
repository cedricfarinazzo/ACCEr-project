using System;

namespace SMProgress
{
    [Serializable]
    public class ProgressSolo {

        private int nivSolo = -1;

        public int NivSolo
        {
            get
            {
                return nivSolo;
            }

            set
            {
                nivSolo = value;
            }
        }

        public ProgressSolo()
        {

        }

        public override string ToString()
        {
            return NivSolo.ToString();
        }

        public static ProgressSolo Parse(string text)
        {
            try
            {
                int i = int.Parse(text);
                ProgressSolo m = new ProgressSolo();
                m.NivSolo = i;
                return m;
            }
            catch (Exception)
            {
                return new ProgressSolo();
            }
        }
    }
}