using System;
using System.Collections.Generic;

namespace Axle.Data.Versioning.Changeset
{
    /// <summary>
    /// An interface representing a database changeset.
    /// </summary>
    public interface IDbChangeset
    {
        /// <summary>
        /// The connection string for the datasource that will be a subject of versioning. 
        /// </summary>
        string DataSource { get; }
        /// <summary>
        /// A collection of <see cref="Uri"/> pointing to the locations of the changelogs that form the current
        /// <see cref="IDbChangeset"/> instance. 
        /// </summary>
        /// <remarks>
        /// When the migration engine executes the changeset, the changelog items will be ordered by their filename. 
        /// </remarks>
        IEnumerable<Uri> Changelog { get; }
        /// <summary>
        /// Gets the <see cref="Type"/> of the database migration engine that will perform the current changeset.
        /// </summary>
        Type MigrationEngine { get; }
    }
}