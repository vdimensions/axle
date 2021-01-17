using System.Collections.Generic;

namespace Axle.Application.Unity
{
    public static class UnityProfiles
    {
        public static IEnumerable<string> Detect()
        {
            if (UnityEngine.Application.isEditor)
            {
                yield return UnityProfiles.Editor;
            }
            if (UnityEngine.Application.isMobilePlatform)
            {
                yield return UnityProfiles.Mobile;
            }
            if (UnityEngine.Application.isConsolePlatform)
            {
                yield return UnityProfiles.Console;
            }
            yield return UnityEngine.Application.platform.ToString();
        }
        
        public const string Editor = "Editor";
        public const string Console = "Console";
        public const string Mobile = "Mobile";
        public const string Desktop = "Desktop";
    }
}