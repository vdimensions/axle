using System.Collections.Generic;

namespace Axle.Data.Versioning.Configuration
{
    public class DbVersioningConfig
    {
        public List<DbChangesetConfig> Migrations { get; set; }
    }
}