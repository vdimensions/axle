using System;
using System.Collections;
using System.Collections.Generic;
using Axle.Verification;

namespace Axle.Data.Records
{
    /// <summary>
    /// A class representing a data record.
    /// </summary>
    public sealed partial class DataRecord : IEnumerable<object>
    {
        /// <summary>
        /// An interface that represents a data record object.
        /// </summary>
        private interface IDataRecord : IEnumerable<object>
        {
            string GetName(int i);
    
            Type GetType(int i);
    
            int GetOrdinal(string name);

            T GetValue<T>(int i);
            T GetValue<T>(string name);
            
            void SetValue<T>(int i, T value);
            void SetValue<T>(string name, T value);
    
            /// <summary>
            /// Gets the number of columns in the current record.
            /// </summary>
            /// <returns>
            /// When not positioned in a valid recordset, <c>0</c>; otherwise, the number of columns in the current record.
            /// The default is <c>-1</c>.
            /// </returns>
            int FieldCount { get; }
            string[] FieldNames { get; }
    
            object this[int i] { get; }
            object this[string name] { get; }
        }

        private readonly IDataRecord _target;

        private DataRecord(IDataRecord target)
        {
            _target = target.VerifyArgument(nameof(target)).IsNotNull().Value;
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator() => _target.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _target.GetEnumerator();

        public string GetName(int i) => _target.GetName(i);

        public int GetOrdinal(string name) => _target.GetOrdinal(name);

        public T GetValue<T>(int i) => _target.GetValue<T>(i);
        public T GetValue<T>(string name) => _target.GetValue<T>(name);

        public Type GetType(int i) => _target.GetType(i);

        public void SetValue<T>(int i, T value) => _target.SetValue(i, value);
        public void SetValue<T>(string name, T value) => _target.SetValue(name, value);

        public int FieldCount => _target.FieldCount;

        public string[] FieldNames => _target.FieldNames;

        public object this[int i] => _target[i];
        public object this[string name] => _target[name];
    }
}