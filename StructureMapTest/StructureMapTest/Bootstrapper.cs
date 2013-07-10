using StructureMap;

namespace StructureMapTest
{
    public interface IBootstrapper
    {
        void BootstrapStructureMap();
    }

    internal class Bootstrapper : IBootstrapper
    {
        private static bool _hasStarted;

        public void BootstrapStructureMap()
        {
            ObjectFactory.Initialize(cfg =>
            {
                cfg.AddRegistry<MyRegistry>();
            });
        }

        public static void Restart()
        {
            if (_hasStarted)
            {
                ObjectFactory.ResetDefaults();
            }
            else
            {
                Bootstrap();
                _hasStarted = true;
            }
        }

        public static void Bootstrap()
        {
            new Bootstrapper().BootstrapStructureMap();
        }
    }
}
