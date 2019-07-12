namespace Axle.Modularity
{
    public abstract class ModuleExporter
    {
        internal ModuleExporter() { }

        public abstract ModuleExporter Export<T>(T instance, string name);
        public abstract ModuleExporter Export<T>(T instance);
        public abstract ModuleExporter Export<T>(string name);
        public abstract ModuleExporter Export<T>();
    }
}