namespace System.Mvvm.Base
{
    /// <summary> Provides support for caching a value. </summary>
    /// <typeparam name="T">The type of object that is being cached.</typeparam>
    public sealed class Cache<T>
    {
        private readonly Func<T> m_ValueFactory;
        private T m_Value;
        private bool m_IsDirty;

        /// <summary> Initializes a new instance of the <see cref="Cache{T}"/> class. </summary>
        /// <param name="_valueFactory">The delegate that is invoked to produce the value when it is needed.</param>
        public Cache(Func<T> _valueFactory)
        {
            if (null == _valueFactory)
                throw new ArgumentNullException(nameof(_valueFactory));

            m_ValueFactory = _valueFactory;
            m_IsDirty = true;
        }

        /// <summary> Gets the value. </summary>
        public T Value
        {
            get
            {
                if (m_IsDirty)
                {
                    m_Value = m_ValueFactory();
                    m_IsDirty = false;
                }
                return m_Value;
            }
        }

        /// <summary> Indicates that the Cache is dirty and will update itself at the next Value read. </summary>
        public bool IsDirty => m_IsDirty;

        /// <summary> Sets the Cache dirty. This ensures that the Cache is updated at the next Value read. </summary>
        public void SetDirty() => m_IsDirty = true;
    }
}