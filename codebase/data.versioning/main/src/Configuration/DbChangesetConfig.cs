using System;
using System.Collections.Generic;
using Axle.Data.Versioning.Changeset;

namespace Axle.Data.Versioning.Configuration
{
    public class DbChangesetConfig : IDbChangeset
    {
        /// <inheritdoc />
        public string DataSource { get; set; }
        /// <inheritdoc />
        public Type MigrationEngine { get; set; }
        public List<Uri> Changelog { get; set; }
        
        IEnumerable<Uri> IDbChangeset.Changelog => Changelog;
    }
}