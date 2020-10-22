using System.Collections.Generic;

namespace Axle.Web.AspNetCore.Cors
{
    public sealed class AspNetCoreCorsConfig
    {
        public sealed class CorsFields
        {
            public List<string> Headers { get; set; } = new List<string>();
            public List<string> Origins { get; set; } = new List<string>();
            public List<string> Methods { get; set; } = new List<string>();
            public bool Credentials { get; set; }
        }
        
        public CorsFields Allowed { get; set; }
        
    }
}