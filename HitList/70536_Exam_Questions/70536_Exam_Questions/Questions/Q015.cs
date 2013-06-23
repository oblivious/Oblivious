using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace _70536_Exam_Questions.Questions
{
    class Q015
    {
        public static void Run()
        {
            Console.WriteLine("\nQ015 Start:\n");

            Console.WriteLine("Ready to sort one or more text lines...");

            // Start the sort.exe process with redirect input.
            // Use the sort command to sort the input text.
            var myProcess = new Process
            {
                StartInfo =
                {
                    FileName = "sort.exe",
                    UseShellExecute = false,
                    RedirectStandardInput = true
                }
            };

            myProcess.Start();

            var myStreamWriter = myProcess.StandardInput;

            // Prompt the user for input text lines to sort.
            // Write each line to the StandardInput stream of
            // the sort command.
            string inputText;
            int numLines = 0;
            do
            {
                Console.WriteLine("Enter a line of text (or press the enter key to stop):");

                inputText = Console.ReadLine();
                if (!String.IsNullOrEmpty(inputText))
                {
                    numLines++;
                    myStreamWriter.WriteLine(inputText);
                }
            } while (!String.IsNullOrEmpty(inputText));

            myStreamWriter.WriteLine();

            // Write a report header to the console.
            if (numLines > 0)
            {
                Console.WriteLine(" {0} sorted text line(s) ", numLines);
                Console.WriteLine("------------------------");
            }
            else
            {
                Console.WriteLine(" No input was sorted");
            }

            // End the input stream to the sort command.
            // When the stream closes, the sort command
            // writes the sorted text lines to the console.
            myStreamWriter.Close();

            // Wait for the sort process to write the sorted text lines.
            myProcess.WaitForExit();
            myProcess.Close();

            Console.WriteLine("\nQ015 End...\n");
        }
    }
}
