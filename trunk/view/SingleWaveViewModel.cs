using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dWaves
{
    class SingleWaveViewModel : IDictionary<int,double>
    {
        #region Fields
        private Dictionary<int, double> dictionary = new Dictionary<int, double>();
        #endregion

        #region IDictionary<int,double> Members
        public void Add(int key, double value)
        {
            dictionary.Add(key, value);
        }

        public bool ContainsKey(int key)
        {
            return dictionary.ContainsKey(key);
        }

        public ICollection<int> Keys
        {
            get { return dictionary.Keys; }
        }

        public bool Remove(int key)
        {
            return dictionary.Remove(key);
        }

        public bool TryGetValue(int key, out double value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public ICollection<double> Values
        {
            get { return dictionary.Values; }
        }

        public double this[int key]
        {
            get
            {
                return dictionary[key];
            }
            set
            {
                dictionary[key] = value;
            }
        }
        #endregion

        #region ICollection<KeyValuePair<int,double>> Members
        public void Add(KeyValuePair<int, double> item)
        {
            ((ICollection<KeyValuePair<int, double>>)(dictionary)).Add(item);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        public bool Contains(KeyValuePair<int, double> item)
        {
            return ((ICollection<KeyValuePair<int, double>>)(dictionary)).Contains(item);
        }

        public void CopyTo(KeyValuePair<int, double>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<int, double>>)(dictionary)).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return ((ICollection<KeyValuePair<int, double>>)(dictionary)).Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<int, double>>)(dictionary)).IsReadOnly; }
        }

        public bool Remove(KeyValuePair<int, double> item)
        {
            return ((ICollection<KeyValuePair<int, double>>)(dictionary)).Remove(item);
        }
        #endregion

        #region IEnumerable<KeyValuePair<int,double>> Members
        public IEnumerator<KeyValuePair<int, double>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<int, double>>)(dictionary)).GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<int, double>>)(dictionary)).GetEnumerator();
        }
        #endregion
    }
}
