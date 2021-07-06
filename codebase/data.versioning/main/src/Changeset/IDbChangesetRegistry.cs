using System.Collections.Generic;

namespace Axle.Data.Versioning.Changeset
{
    public interface IDbChangesetRegistry
    {
        IDbChangesetRegistry Register(IEnumerable<IDbChangeset> scriptSources);
    }
}
