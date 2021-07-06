using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if NETFRAMEWORK || NETSTANDARD1_1_OR_NEWER
[assembly: Guid("FDA82935-6451-468E-A3DC-DA5E681C652F")]
#endif

[assembly: ComVisible(false)]
// TODO: we should avoid internal expositions like this
[assembly: InternalsVisibleTo("Axle.Web.AspNetCore")]
