using System;
using System.Globalization;
using System.IO;
using Axle.Reflection.Extensions.Type;
using Axle.Resources;
using UnityEngine;

namespace Axle.Application.Unity.Resources
{
    using Object = UnityEngine.Object;

    public sealed class UnityAssetResourceInfo : ResourceInfo
    {
        public static UnityAssetResourceInfo Create(string name, CultureInfo culture, Object resourceObject)
        {
            switch (resourceObject)
            {
                case TextAsset ta:
                    var tr = new TextResourceInfo(ta.name, culture, ta.text);
                    return new UnityAssetResourceInfo(name, culture, resourceObject, tr);
                default:
                    return new UnityAssetResourceInfo(name, culture, resourceObject);
            }
        }
        
        private readonly ResourceInfo _origin;
        private readonly Object _object;

        public UnityAssetResourceInfo(string name, CultureInfo culture, Object value, ResourceInfo origin) : base(name, culture, "application/x-unity-game-object")
        {
            _object = value;
            _origin = origin;
        }
        public UnityAssetResourceInfo(string name, CultureInfo culture, Object value) : this(name, culture, value, null) { }

        public override Stream Open() => _origin?.Open();

        public override bool TryResolve(Type targetType, out object result)
        {
            if (targetType.IsInstanceOfType(_object))
            {
                result = _object;
                return true;
            }

            if (targetType.ExtendsOrImplements<Component>() && _object is GameObject go)
            {
                result = go.GetComponent(targetType);
                return result != null;
            }

            return base.TryResolve(targetType, out result);
        }
    }
}