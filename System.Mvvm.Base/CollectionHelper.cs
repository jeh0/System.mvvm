using System.Collections.Generic;

namespace System.Mvvm.Base
{
    /// <summary> Provides helper methods for collections. </summary>
    public static class CollectionHelper
    {
        /// <summary> Gets the next element in the collection or default when no next element can be found. </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="_collection">The collection.</param>
        /// <param name="_current">The current item.</param>
        /// <returns>The next element in the collection or default when no next element can be found.</returns>
        /// <exception cref="ArgumentNullException">collection must not be <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The collection does not contain the specified current item.</exception>
        public static T GetNextElementOrDefault<T>(IEnumerable<T> _collection, T _current)
        {
            if (_collection == null)
                throw new ArgumentNullException(nameof(_collection));

            bool __found = false;
            IEnumerator<T> __enumerator = _collection.GetEnumerator();
            while (__enumerator.MoveNext())
            {
                if (EqualityComparer<T>.Default.Equals(__enumerator.Current, _current))
                {
                    __found = true;
                    break;
                }
            }

            if (!__found)
                throw new ArgumentException("The collection does not contain the current item.");
            
            return __enumerator.MoveNext() ? __enumerator.Current : default(T);
        }
    }
}
