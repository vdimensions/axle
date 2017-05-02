using System;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Reflection
{
    [Serializable]
    public partial class MethodToken
    {
        public MethodToken(MethodInfo info) : base(info)
        {
            memberType = info.ReturnType;
        }
    }
}