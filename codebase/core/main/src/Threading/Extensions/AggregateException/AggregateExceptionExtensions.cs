using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Axle.Threading.Extensions.AggregateException
{
    using AggregateException = System.AggregateException;

    public static class AggregateExceptionExtensions
    {
        private static IEnumerable<Exception> Resolve(AggregateException e, ICollection<Exception> result)
        {
            for (var i = 0; i < e.InnerExceptions.Count; i++)
            {
                var innerException = e.InnerExceptions[i];
                switch (innerException)
                {
                    case TaskCanceledException _:
                        continue;
                    case AggregateException aggEx:
                        foreach (var exception in Resolve(aggEx, result))
                        {
                            result.Add(exception);
                        }

                        break;
                    default:
                        result.Add(innerException);
                        break;
                }
            }

            return result;
        }

        public static IEnumerable<Exception> Resolve(this AggregateException e) => Resolve(e, new LinkedList<Exception>());

        public static Exception ResolveFirst(this AggregateException e)
        {
            var exceptions = new List<Exception>();
            Resolve(e, exceptions);
            return exceptions.Count > 0 ? exceptions[0] : null;
        }
    }
}
