using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Text;


namespace Axle.Data.SqlClient.Conversion
{
    #if !NETSTANDARD
    [System.Serializable]
    #endif
    internal sealed class SqlXmlConverter : SqlDbTypeConverter<string, SqlXml>
    {
        public SqlXmlConverter() : base(DbType.Xml, SqlDbType.Xml) { }

        protected override SqlXml GetNotNullValue(string value)
        {
            var b = Encoding.UTF8.GetBytes(value);
            var stream = new MemoryStream();
            stream.Write(b, 0, b.Length);
            stream.Flush();
            return new SqlXml(stream);
        }
        protected override string GetNotNullValue(SqlXml value) => value.Value;

        protected override bool IsNull(SqlXml value) => value.IsNull;

        protected override string SourceNullEquivalent => null;
        protected override SqlXml DestinationNullEquivalent => SqlXml.Null;
    }
}