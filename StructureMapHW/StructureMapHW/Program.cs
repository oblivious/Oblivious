using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace StructureMapHW
{
    class Program
    {
        private static void Main(string[] args)
        {
            IContainer container = ConfigureDependencies();

            IAppEngine appEngine = container.GetInstance<IAppEngine>();
            appEngine.Run();
        }

        private static IContainer ConfigureDependencies()
        {
            return new Container(x =>
            {
                x.For<IAppEngine>().Use<AppEngine>();
                x.For<IGreeter>().Use<EnglishGreeter>();
                x.For<IOutputDisplay>().Use<ConsoleOutputDisplay>();
            });
        }
    }

    public class AppEngine : IAppEngine
    {
        private readonly IGreeter greeter;
        private readonly IOutputDisplay outputDisplay;

        public AppEngine(IGreeter greeter, IOutputDisplay outputDisplay)
        {
            this.greeter = greeter;
            this.outputDisplay = outputDisplay;
        }

        public void Run()
        {
            outputDisplay.Show(greeter.GetGreeting());
        }
    }

    public interface IAppEngine
    {
        void Run();
    }

    public interface IGreeter
    {
        string GetGreeting();
    }

    public class EnglishGreeter : IGreeter
    {
        public string GetGreeting()
        {
            return "Hello";
        }
    }

    public class FrenchGreeter : IGreeter
    {
        public string GetGreeting()
        {
            return "Bonjour";
        }
    }

    public interface IOutputDisplay
    {
        void Show(string message);
    }

    public class ConsoleOutputDisplay : IOutputDisplay
    {
        public void Show(string message)
        {
            Console.WriteLine(message);
        }
    }
}
