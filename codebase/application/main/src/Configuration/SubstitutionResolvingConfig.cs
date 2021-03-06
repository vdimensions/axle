﻿using System;
using System.Collections.Generic;
using Axle.Text.Expressions.Substitution;
using Axle.Verification;

namespace Axle.Configuration
{
    internal sealed class SubstitutionResolvingConfig : IConfiguration, ISubstitutionProvider
    {
        private readonly ISubstitutionExpression _expression;
        private readonly IConfigSection _originalConfig;
        private readonly SubstitutionResolvingConfig _substSource;
        private readonly HashSet<string> _takenKeys;

        private SubstitutionResolvingConfig(IConfigSection originalConfig, SubstitutionResolvingConfig dataConfig, ISubstitutionExpression expression, HashSet<string> takenKeys)
        {
            _originalConfig = originalConfig is SubstitutionResolvingConfig sc ? sc._originalConfig : originalConfig;
            _substSource = dataConfig ?? this;
            _expression = expression;
            _takenKeys = takenKeys;
        }
        public SubstitutionResolvingConfig(IConfigSection originalConfig, ISubstitutionExpression expression) 
            : this(originalConfig, null, expression, new HashSet<string>(StringComparer.OrdinalIgnoreCase)){ }

        bool ISubstitutionProvider.TrySubstitute(string token, out string value)
        {
            Verifier.IsNotNull(Verifier.VerifyArgument(token, nameof(token)));
            if (_substSource[token] is IConfigSetting cv)
            {
                value = cv.Value;
                return true;
            }
            value = null;
            return false;
        }

        public IConfigSetting this[string key]
        {
            get
            {
                key.VerifyArgument(nameof(key)).IsNotNull();
                var originalResult = _originalConfig[key];
                if (_takenKeys.Contains(key))
                {
                    // prevent recursive lookup
                    return originalResult;
                }
                switch (originalResult)
                {
                    case IConfigSection cs:
                    {
                        return GetSafeSubstitutionProvider(key, cs);
                    }
                    case IConfigSetting cv:
                    {
                        var substitutedValue = _expression.Replace(cv.Value, GetSafeSubstitutionProvider(key, _originalConfig));
                        return ConfigSetting.Create(substitutedValue);
                    }
                    default:
                    {
                        return originalResult;
                    }
                }
            }
        }

        private SubstitutionResolvingConfig GetSafeSubstitutionProvider(string key, IConfigSection section)
        {
            return new SubstitutionResolvingConfig(section, _substSource, _expression, new HashSet<string>(_takenKeys, _takenKeys.Comparer) {key});
        }

        public IEnumerable<string> Keys => _originalConfig.Keys;

        public string Name => _originalConfig.Name;
        public string Value
        {
            get
            {
                var val = _originalConfig.Value;
                return val == null ? null : _expression.Replace(val, this);
            }
        }
    }
}