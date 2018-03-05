using System.Collections.Generic;


namespace Axle.Configuration
{
    public interface IAddRemoveElementCollection<TAdd, TRemove>
    {
        IEnumerable<TAdd> AddedElements { get; }
        IEnumerable<TRemove> RemovedElements { get; }
    }
}