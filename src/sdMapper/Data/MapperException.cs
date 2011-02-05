using System;
using System.Runtime.Serialization;

namespace sdMapper.Data
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MapperException : Exception
    {
        // constructors...
        #region MapperException()
        /// <summary>
        /// Constructs a new MapperException.
        /// </summary>
        public MapperException() { }
        #endregion
        #region MapperException(string message)
        /// <summary>
        /// Constructs a new MapperException.
        /// </summary>
        /// <param name="message">The exception message</param>
        public MapperException(string message) : base(message) { }
        #endregion
        #region MapperException(string message, Exception innerException)
        /// <summary>
        /// Constructs a new MapperException.
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public MapperException(string message, Exception innerException) : base(message, innerException) { }
        #endregion
        #region MapperException(SerializationInfo info, StreamingContext context)
        /// <summary>
        /// Serialization constructor.
        /// </summary>
        protected MapperException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion
    }
}
