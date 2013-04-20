using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace AttachmentRepairer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Drag and drop the file you wish to repair onto this application.");
                Console.WriteLine("Exiting, press any key to continue...");
                Console.ReadKey();
                return;
            }
            string fileName = args[0];
            Console.WriteLine("Filename: " + fileName);
            string encodedAttachmentName = string.Empty;
            string attachmentName = string.Empty;
            try
            {
                byte[] data;
                Console.WriteLine("Retrieving encoded attachment name from file.");
                //Get part of the attachment name from the file name (encoded in base 64).
                int nameStart = fileName.IndexOf("utf-8") + 6;
                encodedAttachmentName += fileName.Substring(nameStart);
                Console.WriteLine("Segment: \"{0}\"", encodedAttachmentName);
                using (StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
                {
                    string line = string.Empty;
                    string secondSegment = string.Empty;
                    while (!line.Contains("Content-Transfer-Encoding") && line != null)
                    {
                        line = reader.ReadLine();
                        line = line.Replace("\\", "");
                        if (!String.IsNullOrWhiteSpace(line) && line.Contains("utf-8?B?") && line.Contains("?="))
                        {
                            //Get the name segment and decode from base 64 string (first decode).
                            string nameSegment = Regex.Match(line, @"utf-8\?B\?([^\?]*)\?=").Groups[1].Value.Trim();
                            Console.WriteLine("Segment: \"{0}\"", nameSegment);
                            nameSegment = Encoding.UTF8.GetString(Convert.FromBase64String(nameSegment));
                            secondSegment += nameSegment;
                        }
                    }
                    Console.WriteLine("Second segment before: " + secondSegment);
                    //Following the first decode the second segment of the name may still contain delimiters
                    //Replace any whitespace characters
                    secondSegment = Regex.Replace(secondSegment, @"\s", "");
                    //Replace delimiters
                    secondSegment = secondSegment.Replace("?=", "").Replace("=?", "").Replace("utf-8?B?", "");
                    Console.WriteLine("Second segment after: " + secondSegment);
                    encodedAttachmentName += secondSegment;
                    //Once all of the encoded attachment name has been extracted convert it to text
                    attachmentName = Encoding.UTF8.GetString(Convert.FromBase64String(encodedAttachmentName));
                    Console.WriteLine("Attachment name decoded.");
                    //Read to the start of the data.
                    reader.ReadLine();
                    reader.ReadLine();

                    string encodedData = reader.ReadToEnd();

                    data = Convert.FromBase64String(encodedData);
                    reader.Close();
                }
                using (BinaryWriter writer = new BinaryWriter(new FileStream(attachmentName, FileMode.Create)))
                {
                    writer.Write(data);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("The file \"{0}\" was not found, please provide a valid filename.");
            }
            catch(Exception e)
            {
                Console.WriteLine("An unexpected exception occurred. Details as follows: " + e.ToString());
            }
            Console.WriteLine("Decoded attachment name: " + attachmentName);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
