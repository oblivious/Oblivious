using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace PortQryBatch
{
    public class OpInfo
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public string OpName { get; set; }
        public string Result { get; set; }
        public int ThreadNum { get; set; }

        public OpInfo(string[] initInfo)
        {
            this.IP = initInfo[1];
            this.Port = initInfo[2];
            this.OpName = initInfo[0];
        }
    }

    public class PortQryBatch
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please include the input file name in the arguments.");
                Console.WriteLine("Usage: PortQryBatch <file>");
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
                return;
            }

            string currentLine;
            string[] lineInfo;
            List<OpInfo> operatorsInfo = new List<OpInfo>();
            OpInfo currentOperator;

            Console.WriteLine("Reading from file: " + args[0]);
            //Open the file
            using (StreamReader reader = new StreamReader(args[0]))
            {
                //Read a line from the file
                while (((currentLine = reader.ReadLine()) != null) && !String.IsNullOrEmpty(currentLine.Trim()))
                {
                    //Split the line into values.
                    lineInfo = currentLine.Split(',');
                    if (lineInfo.Length != 3)
                    {
                        Console.WriteLine("Invalid line format found, expected \"<IP>,<Port>,<Operator Name>\"");
                        return;
                    }
                    currentOperator = new OpInfo(lineInfo);
                    operatorsInfo.Add(currentOperator);
                }
            }
            Console.WriteLine(operatorsInfo.Count + " operators loaded.");

            Console.WriteLine("Starting threads...");
            Thread[] threads = new Thread[operatorsInfo.Count];
            int count = 0;
            foreach (OpInfo operatorEntry in operatorsInfo)
            {
                try
                {
                    threads[count] = new Thread(PortQryBatch.ThreadProc);
                    operatorsInfo[count].ThreadNum = count;
                    threads[count].Start(operatorsInfo[count]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return;
                }
                count++;
            }
            foreach (Thread currentThread in threads)
            {
                currentThread.Join();
            }
            Console.WriteLine("Run completed.");
            Console.ReadLine();
            return;
        }

        static void ThreadProc(Object stateInfo)
        {
            OpInfo opInfo = (OpInfo)stateInfo;

            //Console.WriteLine("Started thread for operator " + opInfo.OpName);

            Process p;
            string result, output = "";
            int queryIndex, endIndex;
            try
            {
                p = new Process();
                p.StartInfo.FileName = "PortQry.exe";
                p.StartInfo.Arguments = "-n " + opInfo.IP + " -e " + opInfo.Port + " -nr";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                
                queryIndex = output.IndexOf(": ");
                queryIndex += 2;
                endIndex = output.IndexOf("\n", queryIndex);
                result = output.Substring(queryIndex, endIndex - queryIndex).Trim();
                Console.WriteLine((opInfo.IP).PadRight(20) + (opInfo.Port).PadRight(8) + (opInfo.OpName).PadRight(20) + result);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while querying operator " + opInfo.OpName);
                Console.WriteLine(e.ToString());
            }
        }
    }
}
