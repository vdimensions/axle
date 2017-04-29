using System;


namespace Axle.Environment
{
    public static class RuntimeExtensions
    {
        public static bool IsMono(this IRuntime runtime)
        {
            return Type.GetType("Mono.Runtime") != null;
        }
    }
}