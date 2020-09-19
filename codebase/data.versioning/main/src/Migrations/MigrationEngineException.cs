using System;
using System.Runtime.Serialization;

namespace Axle.Data.Versioning
{
    [Serializable]
    public class MigrationEngineException : Exception
    {
        public MigrationEngineException() { }
        public MigrationEngineException(string message) : base(message) { }
        public MigrationEngineException(string message, Exception inner) : base(message, inner) { }

        protected MigrationEngineException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}