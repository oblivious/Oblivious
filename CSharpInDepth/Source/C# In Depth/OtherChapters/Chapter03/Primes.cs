using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Chapter03
{
    [Description("Listing 3.13")]
    class Primes
    {
        static void Main()
        {
            List<int> candidates = new List<int>();         
            for (int i=2; i <= 100; i++)                    
            {                                               
                candidates.Add(i);                          
            }                                               

            for (int factor=2; factor <= 10; factor++)      
            {                                               
                candidates.RemoveAll (delegate(int x)       
                    { return x>factor && x%factor==0; }     
                );                                          
            }

            candidates.ForEach (delegate(int prime)
                { Console.WriteLine(prime); }      
            );                                            
        }
    }
}
