using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;

using Axle.Collections;
using Axle.Collections.Extensions;


namespace Axle.Data.Common
{
    //internal sealed class DbConnection<
    //        TDbConnection,
    //        TDbTransaction, 
    //        TDbCommand,
    //        TDbDataAdapter,
    //        TDbDataReader,
    //        TDbParameter,
    //        TDbType> : DbConnectionDecorator
    //    where TDbConnection: class, IDbConnection
    //    where TDbTransaction: class, IDbTransaction
    //    where TDbCommand: DbCommand, IDbCommand
    //    where TDbDataAdapter: DbDataAdapter, IDbDataAdapter
    //    where TDbDataReader: DbDataReader, IDataReader
    //    where TDbParameter: DbParameter
    //    where TDbType: struct
    //{
    //    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    //    private readonly IDbServiceProvider factory;
    //
    //    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    //    private StateChangeEventHandler stateChangeHandler;
    //
    //    internal readonly IStack<TDbTransaction> Transactions = new DoublyLinkedList<TDbTransaction>();
    //   
    //    internal DbConnection(IDbServiceProvider factory, TDbConnection connection) : base(connection)
    //    {
    //        this.factory = factory;
    //    }
    //
    //    public override IDbTransaction BeginTransaction()
    //    {
 	//        return new DbTransaction<TDbConnection, TDbTransaction, TDbCommand, TDbDataAdapter, TDbDataReader, TDbParameter, TDbType>(this, base.BeginTransaction());
    //    }
    //    public override IDbTransaction BeginTransaction(IsolationLevel il)
    //    {
 	//        return new DbTransaction<TDbConnection, TDbTransaction, TDbCommand, TDbDataAdapter, TDbDataReader, TDbParameter, TDbType>(this, base.BeginTransaction(il));
    //    }
    //    
    //    private TDbCommand CreateDbCommandInternal(string commandText, CommandType commandType, int? commandTimeout, TDbTransaction transaction)
    //    {
    //        EnsureNotDisposed();
    //        return (TDbCommand) factory.CreateCommand(commandText, commandType, commandTimeout, Target, transaction);
    //    }
    //
    //    public IDbCommand CreateCommand(CommandType commandType, string commandText, int? commandTimeout, IEnumerable<IDataParameter> parameters)
    //    {
    //        var cmd = CreateDbCommandInternal(commandText, commandType, commandTimeout, Transactions.LastOrDefault());
    //        parameters.ForEach(p => cmd.Parameters.Add(p));
    //        return cmd;
    //    }
    //
    //    public override void Close()
    //    {
    //        var state = Target.State;
    //        var sc = stateChangeHandler;
    //        base.Close();
    //        sc?.Invoke(this, new StateChangeEventArgs(state, Target.State));
    //    }
    //
    //    public override void Open()
    //    {
    //        var state = Target.State;
    //        base.Open();
    //        stateChangeHandler?.Invoke(this, new StateChangeEventArgs(state, Target.State));
    //    }
    //
    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            stateChangeHandler = null;
    //        }
    //        base.Dispose(disposing);
    //    }
    //
    //    public event StateChangeEventHandler StateChange
    //    {
    //        add
    //        {
    //            if (Target is DbConnection connection)
    //            {
    //                connection.StateChange += value;
    //            }
    //            else
    //            {
    //                stateChangeHandler += value;
    //            }
    //        }
    //        remove
    //        {
    //            if (Target is DbConnection connection)
    //            {
    //                connection.StateChange -= value;
    //            }
    //            else
    //            {
    //                stateChangeHandler -= value;
    //            }
    //        }
    //    }
    //
    //    public IDbConnection UnderlyingConnection => Target;
    //    public IDbTransaction CurrentTransaction => Transactions.Peek();
    //
    //    public static explicit operator DbConnection (DbConnection<TDbConnection, TDbTransaction, TDbCommand, TDbDataAdapter, TDbDataReader, TDbParameter, TDbType> conn)
    //    {
    //        return (DbConnection) conn.Target;
    //    }
    //}
}