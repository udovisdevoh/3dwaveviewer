using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dWaves
{
    class BraneMatrixViewModel : IList<SingleWaveViewModel>
    {
        #region Fields
        private List<SingleWaveViewModel> internalList = new List<SingleWaveViewModel>();
        #endregion

        #region IList<SingleWaveViewModel> Members
        public int IndexOf(SingleWaveViewModel item)
        {
            return internalList.IndexOf(item);
        }

        public void Insert(int index, SingleWaveViewModel item)
        {
            internalList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            internalList.RemoveAt(index);
        }

        public SingleWaveViewModel this[int index]
        {
            get
            {
                return internalList[index];
            }
            set
            {
                internalList[index] = value;
            }
        }

        public void Add(SingleWaveViewModel item)
        {
            internalList.Add(item);
        }

        public void Clear()
        {
            internalList.Clear();
        }

        public bool Contains(SingleWaveViewModel item)
        {
            return internalList.Contains(item);
        }

        public void CopyTo(SingleWaveViewModel[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return internalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(SingleWaveViewModel item)
        {
            return internalList.Remove(item);
        }

        public IEnumerator<SingleWaveViewModel> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
        #endregion
    }
}
