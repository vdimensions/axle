using System;
using System.Data;

using Axle.Verification;


namespace Axle.Data.Common
{
    public class DbConnectionDecorator : IDbConnection
    {
        protected IDbConnection Target;

        public DbConnectionDecorator(IDbConnection target)
        {
            Target = target.VerifyArgument(nameof(target)).IsNotNull().Value;
        }

        public virtual IDbTransaction BeginTransaction(IsolationLevel il) => Target.BeginTransaction(il);
        public virtual IDbTransaction BeginTransaction() => Target.BeginTransaction();

        public virtual void Close() => Target.Close();

        public virtual void ChangeDatabase(string databaseName) => Target.ChangeDatabase(databaseName);

        public virtual IDbCommand CreateCommand() => Target.CreateCommand();

        public virtual void Open() => Target.Open();

        void IDisposable.Dispose() => Dispose();

        public void Dispose() => Target?.Dispose();

        public virtual string ConnectionString
        {
            get => Target.ConnectionString;
            set => Target.ConnectionString = value;
        }

        public virtual int ConnectionTimeout => Target.ConnectionTimeout;

        public virtual string Database => Target.Database;

        public virtual ConnectionState State => Target.State;
    }
}