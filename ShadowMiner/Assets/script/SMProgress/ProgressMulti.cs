using System;

namespace SMProgress
{
    [Serializable]
    public class ProgressMulti
    {

        private int nbWin = 0;

        public int NbWin
        {
            get
            {
                return nbWin;
            }

            set
            {
                nbWin = value;
            }
        }

        public ProgressMulti()
        {

        }

        public override string ToString()
        {
            return NbWin.ToString();
        }

        public static ProgressMulti Parse(string text)
        {
            try
            {
                int i = int.Parse(text);
                ProgressMulti m = new ProgressMulti();
                m.NbWin = i;
                return m;
            }
            catch(Exception)
            {
                return new ProgressMulti();
            }
        }
    }
}