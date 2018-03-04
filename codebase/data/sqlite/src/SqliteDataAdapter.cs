#if NETSTANDARD
using System;
using System.Data.Common;

using Microsoft.Data.Sqlite;


namespace Axle.Data.Sqlite
{
    /********************************************************
     * ADO.NET 2.0 Data Provider for SQLite Version 3.X
     * Written by Robert Simpson (robert@blackcastlesoft.com)
     * 
     * Released to the public domain, use at your own risk!
     ********************************************************/
    /// <summary>
    /// SQLite implementation of DbDataAdapter.
    /// 
    /// Since this class is not provided in Microsoft.Data.Sqlite , I've taken it from 
    /// System.Data.SQLite's code from https://github.com/OpenDataSpace/System.Data.SQLite
    /// and did some minor cleanup.
    /// 
    /// The original file is written by Robert Simpson (robert@blackcastlesoft.com)
    /// </summary>
    public sealed class SqliteDataAdapter : DbDataAdapter
    {
        private readonly bool _disposeSelect = true;

        private static readonly object _updatingEventPH = new object();
        private static readonly object _updatedEventPH = new object();

        #region Public Constructors
        /// <overloads>
        /// This class is just a shell around the DbDataAdapter.  Nothing from
        /// DbDataAdapter is overridden here, just a few constructors are defined.
        /// </overloads>
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SqliteDataAdapter()
        {
        }

        /// <summary>
        /// Constructs a data adapter using the specified select command.
        /// </summary>
        /// <param name="cmd">
        /// The select command to associate with the adapter.
        /// </param>
        internal SqliteDataAdapter(SqliteCommand cmd)
        {
            SelectCommand = cmd;
            _disposeSelect = false;
        }

        /// <summary>
        /// Constructs a data adapter with the supplied select command text and
        /// associated with the specified connection.
        /// </summary>
        /// <param name="commandText">
        /// The select command text to associate with the data adapter.
        /// </param>
        /// <param name="connection">
        /// The connection to associate with the select command.
        /// </param>
        internal SqliteDataAdapter(string commandText, SqliteConnection connection)
        {
            SelectCommand = new SqliteCommand(commandText, connection);
        }

        /// <summary>
        /// Constructs a data adapter with the specified select command text,
        /// and using the specified database connection string.
        /// </summary>
        /// <param name="commandText">
        /// The select command text to use to construct a select command.
        /// </param>
        /// <param name="connectionString">
        /// A connection string suitable for passing to a new SQLiteConnection,
        /// which is associated with the select command.
        /// </param>
        internal SqliteDataAdapter(string commandText, string connectionString)
        {
            var cnn = new SqliteConnection(connectionString);
            SelectCommand = new SqliteCommand(commandText, cnn);
        }
        #endregion

        #region IDisposable "Pattern" Members
        private bool _disposed;
        private void CheckDisposed() /* throw */
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(typeof(SqliteDataAdapter).Name);
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        if (_disposeSelect && SelectCommand != null)
                        {
                            SelectCommand.Dispose();
                            SelectCommand = null;
                        }

                        if (InsertCommand != null)
                        {
                            InsertCommand.Dispose();
                            InsertCommand = null;
                        }

                        if (UpdateCommand != null)
                        {
                            UpdateCommand.Dispose();
                            UpdateCommand = null;
                        }

                        if (DeleteCommand != null)
                        {
                            DeleteCommand.Dispose();
                            DeleteCommand = null;
                        }
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
                _disposed = true;
            }
        }
        #endregion

        /// <summary>
        /// Row updating event handler
        /// </summary>
        public event EventHandler<RowUpdatingEventArgs> RowUpdating
        {
            add
            {
                CheckDisposed();

                #if !PLATFORM_COMPACTFRAMEWORK
                var previous = (EventHandler<RowUpdatingEventArgs>) Events[_updatingEventPH];
                if (previous != null && value.Target is DbCommandBuilder)
                {
                    var handler = (EventHandler<RowUpdatingEventArgs>)FindBuilder(previous);
                    if (handler != null)
                    {
                        Events.RemoveHandler(_updatingEventPH, handler);
                    }
                }
                #endif
                Events.AddHandler(_updatingEventPH, value);
            }
            remove { CheckDisposed(); Events.RemoveHandler(_updatingEventPH, value); }
        }

        #if !PLATFORM_COMPACTFRAMEWORK
        internal static Delegate FindBuilder(MulticastDelegate mcd)
        {
            if (mcd != null)
            {
                var invocationList = mcd.GetInvocationList();
                for (var i = 0; i < invocationList.Length; i++)
                {
                    if (invocationList[i].Target is DbCommandBuilder)
                    {
                        return invocationList[i];
                    }
                }
            }
            return null;
        }
        #endif

        /// <summary>
        /// Row updated event handler
        /// </summary>
        public event EventHandler<RowUpdatedEventArgs> RowUpdated
        {
            add { CheckDisposed(); Events.AddHandler(_updatedEventPH, value); }
            remove { CheckDisposed(); Events.RemoveHandler(_updatedEventPH, value); }
        }

        /// <summary>
        /// Raised by the underlying DbDataAdapter when a row is being updated
        /// </summary>
        /// <param name="value">The event's specifics</param>
        protected override void OnRowUpdating(RowUpdatingEventArgs value)
        {
            var handler = Events[_updatingEventPH] as EventHandler<RowUpdatingEventArgs>;
            handler?.Invoke(this, value);
        }

        /// <summary>
        /// Raised by DbDataAdapter after a row is updated
        /// </summary>
        /// <param name="value">The event's specifics</param>
        protected override void OnRowUpdated(RowUpdatedEventArgs value)
        {
            var handler = base.Events[_updatedEventPH] as EventHandler<RowUpdatedEventArgs>;
            handler?.Invoke(this, value);
        }

        /// <summary>
        /// Gets/sets the select command for this DataAdapter
        /// </summary>
        new public SqliteCommand SelectCommand
        {
            get { CheckDisposed(); return (SqliteCommand) base.SelectCommand; }
            set { CheckDisposed(); base.SelectCommand = value; }
        }

        /// <summary>
        /// Gets/sets the insert command for this DataAdapter
        /// </summary>
        new public SqliteCommand InsertCommand
        {
            get { CheckDisposed(); return (SqliteCommand) base.InsertCommand; }
            set { CheckDisposed(); base.InsertCommand = value; }
        }

        /// <summary>
        /// Gets/sets the update command for this DataAdapter
        /// </summary>
        new public SqliteCommand UpdateCommand
        {
            get { CheckDisposed(); return (SqliteCommand) base.UpdateCommand; }
            set { CheckDisposed(); base.UpdateCommand = value; }
        }

        /// <summary>
        /// Gets/sets the delete command for this DataAdapter
        /// </summary>
        new public SqliteCommand DeleteCommand
        {
            get { CheckDisposed(); return (SqliteCommand) base.DeleteCommand; }
            set { CheckDisposed(); base.DeleteCommand = value; }
        }
    }
}
#endif