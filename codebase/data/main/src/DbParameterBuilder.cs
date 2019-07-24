using System.Data;
using System.Linq;
using Axle.Builder;
using Axle.Verification;

namespace Axle.Data
{
    internal sealed class DbParameterBuilder : IDbParameterBuilder, IDbParameterValueBuilder, IDbParameterOptionalPropertiesBuilder, IFluentBuilder<IDataParameter>
    {
        private readonly IDbServiceProvider _provider;

        private string _parameterName;
        private ParameterDirection _parameterDirection;
        private DbType? _parameterType;
        private int? _parameterSize;
        private object _parameterValue;

        public DbParameterBuilder(IDbServiceProvider provider)
        {
            _provider = provider;
        }

        public IDbParameterValueBuilder CreateParameter(string name, ParameterDirection direction)
        {
            _parameterName = name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            _parameterDirection = direction;
            return this;
        }

        public IDbParameterOptionalPropertiesBuilder SetSize(int size)
        {
            _parameterSize = size;
            return this;
        }

        public IDbParameterOptionalPropertiesBuilder SetType(DbType type)
        {
            _parameterType = type;
            return this;
        }

        public IDbParameterOptionalPropertiesBuilder SetValue(DbType type, object value)
        {
            _parameterValue = value;
            return SetType(type);
        }
        public IDbParameterOptionalPropertiesBuilder SetValue(object value)
        {
            _parameterValue = value;
            return this;
        }

        public IDataParameter Build()
        {
            return _provider.CreateParameter(_parameterName, _parameterType, _parameterSize, _parameterDirection, _parameterValue);
        }

        public IDataParameter[] BuildMany(int count) => Enumerable.Range(0, count).Select(x => Build()).ToArray();
    }
}