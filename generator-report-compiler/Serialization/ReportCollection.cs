using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Brady.Serialization
{
    public class ReportCollection<TCollectionType> : ICollection
    {
        private readonly ArrayList _theCollection = new ArrayList();

        public IEnumerator GetEnumerator() => _theCollection.GetEnumerator();
        public void CopyTo(Array array, int index) => _theCollection.CopyTo(array, index);

        public int Count => _theCollection.Count;
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        public TCollectionType this[int index] 
            => (TCollectionType)_theCollection[index];

        public void Add(TCollectionType item) 
            => _theCollection.Add(item);

        public IEnumerable<TCollectionType> AsEnumerable() 
            => _theCollection.ToArray().Select(t => (TCollectionType)t);
    }
}