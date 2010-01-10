using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dWaves
{
    class WavePack : IWave, IList<IWave>
    {
        #region Const
        public const int JunctionAdd = 0;

        public const int JunctionMultiply = 1;
        #endregion

        #region Fields
        private List<IWave> waveList = new List<IWave>();

        private double normalizationMultiplicator = 1.0;

        private WaveCache waveCache = new WaveCache();

        private int junctionType = JunctionAdd;
        #endregion

        #region Constructor
        public WavePack(int junctionType)
        {
            this.junctionType = junctionType;
        }

        public WavePack()
        {
        }

        public WavePack(IWave wave)
        {
            Add(wave);
        }
        #endregion

        #region Operator Overloads
        public static IWave operator +(WavePack wavePack1, IWave wave2)
        {
            WavePack summ = new WavePack();
            summ.Add(wavePack1);
            summ.Add(wave2);
            return summ;
        }
        #endregion

        #region IList<IWave> Members
        public void Add(IWave item)
        {
            if (item is Wave)
                waveList.Add(item);
            else if (item is WavePack)
            {
                foreach (IWave iWave in ((WavePack)item))
                {
                    if (!this.Contains(iWave))
                    {
                        this.Add(iWave);
                    }
                }
            }
        }

        public void Clear()
        {
            waveList.Clear();
        }

        public bool Contains(IWave item)
        {
            foreach (IWave child in waveList)
                if (child.Equals(item))
                    return true;
            return false;
        }

        public void CopyTo(IWave[] array, int arrayIndex)
        {
            waveList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return waveList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IWave item)
        {
            foreach (IWave iWave in waveList)
                if (iWave.Equals(item))
                    return waveList.Remove(iWave);
            return false;
        }

        public IEnumerator<IWave> GetEnumerator()
        {
            return waveList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return waveList.GetEnumerator();
        }

        public int IndexOf(IWave item)
        {
            int index = 0;
            foreach (IWave iWave in waveList)
            {
                if (item.Equals(iWave))
                    return index;
                index++;
            }

            return -1;
        }

        public void Insert(int index, IWave item)
        {
            waveList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            waveList.RemoveAt(index);
        }

        public IWave this[int index]
        {
            get
            {
                return waveList[index];
            }
            set
            {
                waveList[index] = value;
            }
        }
        #endregion

        #region IEquatable<IWave> Members
        public bool Equals(IWave other)
        {
            if (other is WavePack)
            {
                WavePack otherWavePack = (WavePack)other;

                foreach (IWave iWave in otherWavePack)
                    if (this.Contains(iWave))
                        return false;

                foreach (IWave iWave in this)
                    if (otherWavePack.Contains(iWave))
                        return false;

                return true;
            }
            else if (Count == 1 && other is Wave && this[0] is Wave)
            {
                return this[0].Equals(other);
            }
            return false;
        }
        #endregion

        #region IWave Members
        public double GetYValueAt(double x)
        {
            double value = 0.0;

            if (junctionType == JunctionMultiply)
                value = 1.0;
            else if (junctionType == JunctionAdd)
                value = 0.0;

            if (waveCache.ContainsKey(x))
            {
                value = waveCache.Get(x);
            }
            else
            {
                foreach (IWave iWave in waveList)
                {
                    if (junctionType == JunctionMultiply)
                        value *= iWave.GetYValueAt(x);
                    else if (junctionType == JunctionAdd)
                        value += iWave.GetYValueAt(x);
                }

                waveCache.Add(x, value);
            }

            value *= normalizationMultiplicator;

            return value;
        }

        /// <summary>
        /// Normalize the wave pack
        /// </summary>
        /// <returns></returns>
        public void Normalize()
        {
            double y;

            double maxY = double.NegativeInfinity;
            double minY = double.PositiveInfinity;
            for (double x = -2.0; x < 2.0; x += 0.001)
            {
                y = GetYValueAt(x);
                if (y > maxY)
                    maxY = y;

                if (y < minY)
                    minY = y;
            }

            maxY = Math.Max(maxY, minY * -1.0);

            normalizationMultiplicator = 1.0 / maxY;
        }
        #endregion
    
        #region Static Methods
        public static int GetRandomJunctionType(Random random)
        {
            return random.Next(0, 2);
        }
        #endregion
    }

}