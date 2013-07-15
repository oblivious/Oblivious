using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace SocketExceptionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                throw new SocketException(10004);
            }
            catch (Exception e)
            {
                Console.WriteLine("Message: {0}", e.Message);
                //Console.WriteLine("Socket Error Code: {0}", e.SocketErrorCode);
                Console.WriteLine("Stack Trace: {0}", e.StackTrace);
                Console.WriteLine("ToString(): {0}", e.ToString());
                //Console.WriteLine(e.NativeErrorCode);
            }
        }
    }
}
