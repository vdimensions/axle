using System;
using System.Collections;
using System.Collections.Generic;

using Axle.Verification;


namespace Axle.Data.Common
{
    public abstract class DbRecordDecorator : IDbRecord
    {
        protected readonly IDbRecord Target;

        protected DbRecordDecorator(IDbRecord target)
        {
            Target = target.VerifyArgument(nameof(target)).IsNotNull().Value;
        }

        public virtual IEnumerator<object> GetEnumerator() => Target.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public virtual string GetFieldName(int i) => Target.GetFieldName(i);

        public virtual Type GetFieldType(int i) => Target.GetFieldType(i);

        public virtual Guid GetGuid(int i) => Target.GetGuid(i);
        public virtual Guid GetGuid(string name) => Target.GetGuid(name);

        public virtual string GetString(int i) => Target.GetString(i);
        public virtual string GetString(string name) => Target.GetString(name);

        public virtual char GetChar(int i) => Target.GetChar(i);
        public virtual char GetChar(string name) => Target.GetChar(name);

        public virtual bool GetBoolean(int i) => Target.GetBoolean(i);
        public virtual bool GetBoolean(string name) => Target.GetBoolean(name);

        public virtual byte GetByte(int i) => Target.GetByte(i);
        public virtual byte GetByte(string name) => Target.GetByte(name);

        public virtual short GetInt16(int i) => Target.GetInt16(i);
        public virtual short GetInt16(string name) => Target.GetInt16(name);

        public virtual int GetInt32(int i) => Target.GetInt32(i);
        public virtual int GetInt32(string name) => Target.GetInt32(name);

        public virtual long GetInt64(int i) => Target.GetInt64(i);
        public virtual long GetInt64(string name) => Target.GetInt64(name);

        public virtual float GetFloat(int i) => Target.GetFloat(i);
        public virtual float GetFloat(string name) => Target.GetFloat(name);

        public virtual double GetDouble(int i) => Target.GetDouble(i);
        public virtual double GetDouble(string name) => Target.GetDouble(name);

        public virtual decimal GetDecimal(int i) => Target.GetDecimal(i);
        public virtual decimal GetDecimal(string name) => Target.GetDecimal(name);

        public virtual DateTime GetDateTime(int i) => Target.GetDateTime(i);
        public virtual DateTime GetDateTime(string name) => Target.GetDateTime(name);

        public virtual int GetOrdinal(string name) => Target.GetOrdinal(name);

        public virtual int FieldCount => Target.FieldCount;

        public virtual string[] FieldNames => Target.FieldNames;

        public virtual object this[int i] => Target[i];
        public virtual object this[string name] => Target[name];
    }
}