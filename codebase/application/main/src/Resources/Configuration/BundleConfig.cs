using System;
using System.Collections.Generic;

namespace Axle.Resources.Configuration
{
    internal sealed class BundleConfig
    {
        public string Name { get; set; }
        public IList<Uri> Locations { get; set; } = new List<Uri>();
        public IList<Type> Extractors { get; set; } = new List<Type>();
    }
}