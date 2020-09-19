using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Axle.Data.DataSources;
using Axle.Data.Versioning.Migrations;
using Axle.Extensions.Uri;
using Axle.Modularity;
using Axle.Resources;
using Axle.Verification;

namespace Axle.Data.Versioning.Changeset
{
    [Module]
    internal sealed class DbChangesetModule : IDbChangesetRegistry, IEnumerable<DbChangelog>
    {
        private ConcurrentBag<DbChangelog> _dbChangelogs;

        public DbChangesetModule()
        {
            _dbChangelogs = new ConcurrentBag<DbChangelog>();
        }

        private IDbChangesetRegistry DoRegister(IEnumerable<IDbChangeset> migrationChangesets)
        {
            var changelogs = migrationChangesets
                .SelectMany(x => x.Changelog.Select(
                    c =>
                    {
                        var fileName = c.Segments.LastOrDefault();
                        var location = c.Resolve("../");
                        var dataSourceName = x.DataSource;
                        var scriptsResourceManager = new DefaultResourceManager();
                        scriptsResourceManager.Bundles.Configure(dataSourceName).Register(location);
                        var resource =
                            scriptsResourceManager.Load(dataSourceName, fileName, CultureInfo.InvariantCulture);
                        return new DbChangelog(dataSourceName, resource, x.MigrationEngine);
                    }));
            foreach (var changelog in changelogs)
            {
                _dbChangelogs.Add(changelog);
            }
            return this;
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        IDbChangesetRegistry IDbChangesetRegistry.Register(IEnumerable<IDbChangeset> migrationChangesets)
        {
            migrationChangesets.VerifyArgument(nameof(migrationChangesets)).IsNotNull();
            return DoRegister(migrationChangesets);
        }

        public IEnumerator<DbChangelog> GetEnumerator()
        {
            return _dbChangelogs.GetEnumerator();
            
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}