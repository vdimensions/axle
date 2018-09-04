using System.Data;
using System.Data.Common;

using Axle.Verification;


namespace Axle.Data.Common
{
    public sealed class DbRecord : DbRecordDecorator
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        public static implicit operator DbRecord(DataRow dataRow)
        {
            return new DbRecord(new DataRowAdapter(dataRow.VerifyArgument(nameof(dataRow)).IsNotNull()));
        }
        #endif
        public static implicit operator DbRecord(DbDataRecord dataRecord)
        {
            return new DbRecord(new DataRecordAdapter(dataRecord.VerifyArgument(nameof(dataRecord)).IsNotNull().Value));
        }
        public static implicit operator DbRecord(DbDataReader dataReader)
        {
            return new DbRecord(new DataRecordAdapter(dataReader.VerifyArgument(nameof(dataReader)).IsNotNull().Value));
        }

        public DbRecord(IDbRecord target) : base(target) { }
    }
}