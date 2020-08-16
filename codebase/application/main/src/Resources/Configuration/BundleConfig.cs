using System;
using System.Collections.Generic;

namespace Axle.Resources.Configuration
{
    internal class BundleConfig
    {
        public IList<Uri> Locations { get; set; } = new List<Uri>();
        public IList<Type> Extractors { get; set; } = new List<Type>();
    }
}