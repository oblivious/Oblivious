using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDomainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain d = AppDomain.CreateDomain("New Domain");


            d.ExecuteAssembly(@"D:\Oblivious\VS8\Chapter 8\CreatingDomainsAndLoadingAssemblies\ShowWinIni\bin\Debug\ShowWinIni.exe");

            Console.WriteLine("\n\nBy Name: ");
            d.ExecuteAssemblyByName(@"ShowWinIni");
        }
    }
}
