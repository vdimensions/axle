using System;
using System.Collections.Generic;
using Axle.Data.Versioning.Changeset;

namespace Axle.Data.Versioning.Configuration
{
    public class DbChangesetConfig : IDbChangeset
    {
        public string DataSource { get; set; }
        public List<Uri> Changelog { get; set; }
        public Type MigrationEngine { get; set; }
        
        IEnumerable<Uri> IDbChangeset.Changelog => Changelog;
    }
}