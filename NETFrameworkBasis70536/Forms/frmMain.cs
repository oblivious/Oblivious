// This source file is distributed under the licence
// Creative Commons Attribution-Share Alike 2.5 Generic - http://creativecommons.org/licenses/by-sa/2.5/
// (C) 2007 - Fabian Sosa Escalada
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Security.Policy;
using System.Security;
using System.ServiceProcess;
using System.Configuration;
using System.Collections.Specialized;
using System.Configuration.Install;
using System.Collections;
using System.Diagnostics;
using System.Management;
using System.Security.Permissions;
using System.Net;
using System.Security.Principal;
using System.Security.AccessControl;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Reflection;
using System.Reflection.Emit;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace fabianse70536.Forms
{
    public partial class frmFabianseMain : Form
    {
        public frmFabianseMain()
        {
            InitializeComponent();
        }

        private void btnGraphicsClass_Click(object sender, EventArgs e)
        {
            Graphics g = tabGraphicsClass.CreateGraphics();

            // line
            Pen p = new Pen(Color.Red, 3);
            g.DrawLine(p, 110, 110, 300, 300);

            // pie
            p.Brush = Brushes.Coral;
            g.DrawPie(p, 10, 10, 200, 200, -30, 50);

            int x = 100;
            int y = 100;

            Point[] a = new Point[] {
                new Point(x + 10, y + 10)
                , new Point(x + 300, y + 15)
                , new Point(x + 250, y + 250)
                , new Point(x + 350, y + 300)
                , new Point(x + 150, y + 350)
            };

            // polygon
            p.DashPattern = new float[] { 1, 1, 2, 1, 3, 1, 4, 1 };
            p.DashStyle = DashStyle.DashDotDot;
            //p.DashStyle = DashStyle.DashDot;
            g.DrawPolygon(p, a);

            x = 500;
            y = 150;

            // polygon filled
            Point[] a2 = new Point[] {
                new Point(x + 10, y + 10)
                , new Point(x + 300, y + 15)
                , new Point(x + 250, y + 250)
                , new Point(x + 350, y + 300)
                , new Point(x + 150, y + 350)
            };
            Brush b = new SolidBrush(Color.Red);
            g.FillPolygon(b, a2);


            // gradient brush
            //b = new LinearGradientBrush(new Point(200, 200), new Point(300, 300), Color.Magenta, Color.SlateBlue);
            b = new LinearGradientBrush(new Point(200, 200), new Point(300, 300), Color.Magenta, Color.SlateBlue);
            g.FillRectangle(b, 800, 200, 100, 100);

            // ends
            p = new Pen(Color.RoyalBlue, 15);
            p.StartCap = LineCap.DiamondAnchor;
            p.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(p, 300, 300, 400, 400);


        }

        private void btnGraphicsFromImage_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap("fullmoon.jpg");
            Graphics g = Graphics.FromImage(b);
            g.DrawLine(Pens.Aquamarine, 10, 10, 200, 200);
            
            Graphics tab = tabGraphicsClass.CreateGraphics();
            tab.DrawImage(b, 100, 100);
        }

        private void btnImageClass_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine( Application.StartupPath , "fullmoon.jpg");
            // method 1 of loading
            Image i = Image.FromFile(filename);
            pictureBoxImage1.BackgroundImage = i;

            // method 2 of loading
            Bitmap b = new Bitmap(filename);
            pictureBoxImage1.BackgroundImage = i;
        }

        private void btnCreateAnImage_Click(object sender, EventArgs e)
        {
            // create a bitmap
            Bitmap bm = new Bitmap(500, 500);

            // draw with the bitmap itself
            bm.SetPixel(150, 150, Color.Red);
            bm.SetPixel(151, 151, Color.Red);

            // draw through graphics
            Graphics g = Graphics.FromImage(bm);
            g.DrawRectangle(new Pen(Color.Green), 100, 100, 100, 100);

            // show results
            pictureBoxImage1.BackgroundImage = bm;

            // save
            string filename = Path.Combine( Application.StartupPath , "mysavedbitmap.jpg");
            bm.Save(filename, ImageFormat.Jpeg);
        }

        private void btnIconClass_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(500, 500);
            Graphics g = Graphics.FromImage(bm);
            pictureBoxImage1.BackgroundImage = bm;
            g.DrawIcon(SystemIcons.Hand, 100, 100);
            g.DrawIcon(SystemIcons.Shield, 200, 200);
        }

        private void btnFontClass_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(500, 500);
            Graphics g = Graphics.FromImage(bm);
            pictureBoxImage1.BackgroundImage = bm;

            // simple way to create a font
            Font f = new Font("Times New Roman", 14, FontStyle.Italic);

            g.DrawString("create font with new", f, Brushes.Blue, 200, 10);

            // creating font from string
            FontConverter converter = new FontConverter();
            Font f2 = (Font)converter.ConvertFromString("Arial; 14pt");

            g.DrawString("create font from string", f2, Brushes.Red, 200, 60);

            // create from font family
            FontFamily ff = new FontFamily("Arial");
            Font f3 = new Font(ff, 14);

            g.DrawString("create font from font family", f3, Brushes.Green, 200, 110);

        }

        private void btnFontSample1_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(500, 500);
            Graphics g = Graphics.FromImage(bm);
            pictureBoxFont.BackgroundImage = bm;

            // rectangle
            int x = int.Parse(txtRectanglePointX.Text);
            int y = int.Parse(txtRectanglePointY.Text);
            int w = int.Parse(txtRectangleWidth.Text);
            int h = int.Parse(txtRectangleHeight.Text);

            Rectangle r = new Rectangle( new Point(x,y), new Size(w,h) );

            // StringFormat
            StringFormat sf = new StringFormat();
            
            // alignment
            if (rbAlignmentCenter.Checked)
            {
                sf.Alignment = StringAlignment.Center;
            } 
            else
                if (rbAlignmentNear.Checked)
                {
                    sf.Alignment = StringAlignment.Near;
                }
                else
                    if (rbAlignmentFar.Checked)
                    {
                        sf.Alignment = StringAlignment.Far;
                    }
            
            //Line alignment
            if (rbLineAlignmentCenter.Checked)
            {
                sf.LineAlignment = StringAlignment.Center;
            }
            else
                if (rbLineAlignmentNear.Checked)
                {
                    sf.LineAlignment = StringAlignment.Near;
                }
                else
                    if (rbLineAlignmentFar.Checked)
                    {
                        sf.LineAlignment = StringAlignment.Far;
                    } 

            // format flags
            sf.FormatFlags = sf.FormatFlags & 0;
            if (cbFFDirectionRightToLeft.Checked) sf.FormatFlags = sf.FormatFlags | StringFormatFlags.DirectionRightToLeft;
            if (cbFFDirectionVertical.Checked) sf.FormatFlags = sf.FormatFlags | StringFormatFlags.DirectionVertical;
            if (cbFFDisplayFormatControl.Checked) sf.FormatFlags = sf.FormatFlags | StringFormatFlags.DisplayFormatControl;
            if (cbFFFitBlackBox.Checked) sf.FormatFlags = sf.FormatFlags | StringFormatFlags.FitBlackBox;
            if (cbFFLineLimit.Checked) sf.FormatFlags = sf.FormatFlags | StringFormatFlags.LineLimit;
            if (cbFFMeasureTrailingSpaces.Checked) sf.FormatFlags = sf.FormatFlags | StringFormatFlags.MeasureTrailingSpaces;
            if (cbFFNoClip.Checked) sf.FormatFlags = sf.FormatFlags | StringFormatFlags.NoClip;
            if (cbFFNoFontFallback.Checked) sf.FormatFlags = sf.FormatFlags | StringFormatFlags.NoFontFallback;
            if (cbFFNoWrap.Checked) sf.FormatFlags = sf.FormatFlags | StringFormatFlags.NoWrap;
            
            // trimming
            if (rbTrimmingCharacter.Checked)
            {
                sf.Trimming = StringTrimming.Character;
            }
            else
                if (rbTrimmingEllipsisCharacter.Checked)
                {
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                }
                else
                    if (rbTrimmingEllipsisPath.Checked)
                    {
                        sf.Trimming = StringTrimming.EllipsisPath;
                    }
                    else
                        if (rbTrimmingEllipsisWord.Checked)
                        {
                            sf.Trimming = StringTrimming.EllipsisWord;
                        }
                        else
                            if (rbTrimmingNone.Checked)
                            {
                                sf.Trimming = StringTrimming.None;
                            }
                            else
                                if (rbTrimmingWord.Checked)
                                {
                                    sf.Trimming = StringTrimming.Word;
                                }

            // simple way to create a font
            Font f = new Font("Times New Roman", 14, FontStyle.Italic);

            // draw the rectangle
            if (cbFontDrawRectangle.Checked)
            {
                g.DrawRectangle(Pens.BlueViolet, r);
            }
            
            // draw the string
            g.DrawString(txtFontText.Text, f, Brushes.Blue, (RectangleF)r, sf);

        }

        private void mySimpleThreadMethod()
        {
            Console.WriteLine("Thread.CurrentThread.ManagedThreadId = " + Thread.CurrentThread.ManagedThreadId.ToString());
            Console.WriteLine("Thread.CurrentThread.Priority = " + Thread.CurrentThread.Priority);
        }

        private void btnSimpleThread_Click(object sender, EventArgs e)
        {
            // create a ThreadStart delegate passing a method as constructor's parameter
            ThreadStart ts1 = new ThreadStart(mySimpleThreadMethod);
            
            // create the thread and pass as a parameter the ThreadStart object
            Thread thread1 = new Thread(ts1);

            // start the thread
            thread1.Start();

            // sleep main thread a second until thread1 finishes
            Thread.Sleep(1000);

            // Do we have to create the thread again after it finished? try commenting out next line.
            thread1 = new Thread(ts1);
            // change priority
            thread1.Priority = ThreadPriority.AboveNormal;
            // start it again
            thread1.Start();

            MessageBox.Show("Check the Visual Studio output window.");
        }

        private void myParameterizedThreadMethod(object o)
        {
            int a= (int) o;
            Console.WriteLine("M.ThreadId= {0} ; parameter = {1}", Thread.CurrentThread.ManagedThreadId, a);
        }

        private void ParameterizedTHreadStart_Click(object sender, EventArgs e)
        {
            ParameterizedThreadStart pts1 = new ParameterizedThreadStart(myParameterizedThreadMethod);
            Thread thread1 = new Thread(pts1);
            thread1.Start(99);
        }

        private void btnExecutionContextClass_Click(object sender, EventArgs e)
        {
            // changing execution context flow
            // Method 1
            ExecutionContext.SuppressFlow();
            // new threads doesn't get the execution context
            ThreadStart ts1 = new ThreadStart(mySimpleThreadMethod);
            Thread thread1 = new Thread(ts1);
            thread1.Start();

            // restore the flow for subsequent threads
            ExecutionContext.RestoreFlow();

            // Method 2
            AsyncFlowControl afc = ExecutionContext.SuppressFlow();
            ThreadStart ts2 = new ThreadStart(mySimpleThreadMethod);
            Thread thread2 = new Thread(ts2);
            thread2.Start();
            // restore the flow for subsequent threads
            afc.Undo();
        }

        private void myCallback(object o)
        {
            ExecutionContext ec = ExecutionContext.Capture();
            Console.WriteLine("In callback \"myCallback\" with execution context hashcode {0}", ec.GetHashCode());
            Console.WriteLine("Run in custom ExecutionContext. State Object: "+ (string)o);
        }

        private void btnContextCallback_Click(object sender, EventArgs e)
        {
            // get current context
            ExecutionContext ec = ExecutionContext.Capture();
            Console.WriteLine("In main method with execution context hashcode {0}", ec.GetHashCode());
            // prepare to execute a callback with an specified context
            ExecutionContext.Run(ec, new ContextCallback(myCallback), "state object!");
        }

        // field
        int countForInterlocked = 0;

        private void simpleOperationForInterlockedClass()
        {
            Random r = new Random();
            for (int t = 0; t < 200; t++)
            {
                Interlocked.Increment(ref countForInterlocked);
                Thread.Sleep(r.Next(5));
            }
        }

        private void simpleIncrement()
        {
            Random r = new Random();
            for (int t = 0; t < 200; t++)
            {
                countForInterlocked = countForInterlocked + 1;
                Thread.Sleep( r.Next(5) );
            }
        }

        private void btnInterlockedClass_Click(object sender, EventArgs e)
        {
            // reinit the counter
            countForInterlocked = 0;

            Thread[] threads = new Thread[10];
            // call increment method without taking care of threads
            ThreadStart ts1 = new ThreadStart(simpleIncrement);
            for (int t = 0; t < 10; t++)
            {
                threads[t] = new Thread(ts1);
                threads[t].Start();
            }

            // wait for each thread to finish
            for (int t = 0; t < 10; t++)
            {
                threads[t].Join();
            }

            MessageBox.Show("Counter result = " + countForInterlocked);

            // reinit the counter
            countForInterlocked = 0;

            // use Interlocked class to increment as an atomic operation
            ThreadStart ts2 = new ThreadStart(simpleOperationForInterlockedClass);
            for (int t = 0; t < 10; t++)
            {
                threads[t] = new Thread(ts2);
                threads[t].Start();
            }

            // wait for each thread to finish
            for (int t = 0; t < 10; t++)
            {
                threads[t].Join();
            }

            MessageBox.Show("Counter result = " + countForInterlocked);
        }

        private void simpleIncrementWithMonitor(object o)
        {
            int i = (int)o;
            Monitor.Enter(this);
            Console.WriteLine("Thread #{0} entering simpleIncrementWithMonitor", i);
            try
            {
                Random r = new Random();
                for (int t = 0; t < 200; t++)
                {
                    countForInterlocked = countForInterlocked + 1;
                    Thread.Sleep(r.Next(5));
                }
            }
            finally
            {
                Console.WriteLine("Thread #{0} exiting simpleIncrementWithMonitor", i);
                Monitor.Exit(this);
            }
        }


        private void btnMonitorClass_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Start: {0}", DateTime.Now);
            // reinit the counter
            countForInterlocked = 0;

            Thread[] threads = new Thread[10];
            // call increment method which uses Monitor
            var ts1 = new ParameterizedThreadStart(simpleIncrementWithMonitor);
            for (int t = 0; t < 10; t++)
            {
                threads[t] = new Thread(ts1);
                threads[t].Start(t);
            }

            // wait for each thread to finish
            for (int t = 0; t < 10; t++)
            {
                threads[t].Join();
            }
            Console.WriteLine("Stop: {0}", DateTime.Now);

            MessageBox.Show("Counter result = " + countForInterlocked);

        }

        ReaderWriterLock rrl;

        private void simpleIncrementWithReaderWriterLock()
        {
            try
            {
                rrl.AcquireWriterLock(100000);
                try
                {
                    Random r = new Random();
                    for (int t = 0; t < 200; t++)
                    {
                        countForInterlocked = countForInterlocked + 1;
                        Thread.Sleep(r.Next(5));
                    }
                }
                finally
                {
                    rrl.ReleaseWriterLock();
                }
            }
            catch(ApplicationException e)
            {
                MessageBox.Show("Failed to acquiere the reader lock. " + e.Message);
            }
        }

        private void btnReaderWriterLockClass_Click(object sender, EventArgs e)
        {
            // create an R.R. object
            rrl = new ReaderWriterLock();
            // reinit the counter
            countForInterlocked = 0;

            Thread[] threads = new Thread[10];
            // call increment method which uses ReaderWriterLock
            ThreadStart ts1 = new ThreadStart(simpleIncrementWithReaderWriterLock);
            for (int t = 0; t < 10; t++)
            {
                threads[t] = new Thread(ts1);
                threads[t].Start();
            }

            // wait for each thread to finish
            for (int t = 0; t < 10; t++)
            {
                threads[t].Join();
            }

            MessageBox.Show("Counter result = " + countForInterlocked);

        }

        private void btnMutex_Click(object sender, EventArgs e)
        {
            // Method 1
            Console.WriteLine("Creating Mutex: Mutex m = new Mutex();");
            Mutex m = new Mutex();
            if (m.WaitOne(400, false))
            {
                try
                {
                    Console.WriteLine("The OS gods have seen fit to bestow upon this humble .Net application access to the Mutex " + 
                        "that we just created, all hail the OS gods!");
                    Console.WriteLine("This line of code runs threadsafe due to Mutex class.");
                }
                finally
                {
                    Console.WriteLine("We release this mutex back to the ether from whence it came so that it might bestow its " +
                        "light upon us again in the future. Farewell, gentle mutex, you served us well!");
                    m.ReleaseMutex();
                }
            }
            else
            {
                MessageBox.Show("Mutex.WaitOne has failed.");
            }

            // Method 2 - using a named Mutex
            Console.WriteLine("Declaring a reference to a Mutex: Mutex m2 = null;");
            Mutex m2 = null;
            try
            {
                Console.WriteLine("Trying m2 = Mutex.OpenExisting(\"NamedMutex\")");
                m2 = Mutex.OpenExisting("NamedMutex");
                Console.WriteLine("Successfully opened named mutex \"NamedMutex\"");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Console.WriteLine("Failed to open named mutex \"NamedMutex\"");
                // Mutex doesn't previously exists
            }
            if (m2 == null)
            {
                Console.WriteLine("Creating named mutex \"NamedMutex\" as it did not previously exist");
                m2 = new Mutex(false, "NamedMutex");
            }

            if (m2.WaitOne(400, false))
            {
                try
                {
                    Console.WriteLine("The OS gods have seen fit to bestow upon this humble .Net application access to the named " + 
                        "Mutex \"NamedMutex\", all hail the OS gods!");
                    Console.WriteLine("This line of code runs threadsafe due to Mutex class / named.");
                }
                finally
                {
                    Console.WriteLine("We release this mutex back to the ether from whence it came so that it might bestow its " +
                        "light upon us again in the future. Farewell, gentle mutex, you served us well!");
                    m2.ReleaseMutex();
                }
            }
            else
            {
                MessageBox.Show("Mutex.WaitOne has failed.");
            }

        }

        private void btnSemaphore_Click(object sender, EventArgs e)
        {
            Semaphore s = null;
            try
            {
                s = Semaphore.OpenExisting("NamedSemaphore");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                // Semaphore doesn't previously exists
            }
            if (s == null)
            {
                s = new Semaphore(1,10, "NamedSemaphore");
            }
            
            if (s.WaitOne(400,false))
            {
                try
                {
                    Console.WriteLine("This line of code runs threadsafe");
                }
                finally
                {
                    s.Release();
                }
            }
            else
            {
                MessageBox.Show("WaitOne has failed.");
            }

        }

        private void btnIAsyncResult_Click(object sender, EventArgs e)
        {
            byte[] bytebuf = new byte[1024];

            string filename = Application.ExecutablePath;

            FileStream fs = new FileStream(filename, FileMode.Open
                , FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous);

            IAsyncResult result = fs.BeginRead(bytebuf, 0, bytebuf.Length, null, null);

            // do other processing
            for (int t = 0; t < 100; t++)
            {
                Thread.Sleep(1);
            }

            // think: why we use the try/catch here?            
            int readbytes=0;
            try
            {
                // we can also use result isComplete before blocking for completion
                readbytes = fs.EndRead(result);
            }
            catch (IOException i)
            {
                MessageBox.Show("An error ocurred during read operation. "+i.Message);
            }

            MessageBox.Show("Total bytes read:" + readbytes.ToString());
            fs.Close();
        }

        private void myFileRead_CallBack(IAsyncResult result)
        {
            FileStream stream = (FileStream)result.AsyncState;

            int readbytes = stream.EndRead(result);
            Console.WriteLine("Callback fired. Total bytes read:" + readbytes.ToString());
            stream.Close();
        }

        private void btnAsyncCallback_Click(object sender, EventArgs e)
        {
            byte[] bytebuf = new byte[1024];

            string filename = Application.ExecutablePath;

            FileStream fs = new FileStream(filename, FileMode.Open
                , FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous);

            IAsyncResult result = fs.BeginRead(bytebuf, 0, bytebuf.Length
                , new AsyncCallback(myFileRead_CallBack)
                , fs);

            // do other processing
            for (int t = 0; t < 100; t++)
            {
                Thread.Sleep(1);
            }

            Console.WriteLine("Main function finished.");

        }

        private void myPooledMethod(object o)
        {
            Console.WriteLine("myPooledMethod fired.");
            Thread.Sleep(200);
            Console.WriteLine("myPooledMethod finished.");
        }

        private void btnThreadPool_Click(object sender, EventArgs e)
        {
            WaitCallback wc = new WaitCallback(myPooledMethod);
            if (!ThreadPool.QueueUserWorkItem(wc))
            {
                MessageBox.Show("ThreadPool.QueueUserWorkItem failed to enqueue item");
            }

            // show some information
            int minWorkerThreads;
            int minCompletionPortThreads;
            int maxWorkerThreads;
            int maxCompletionPortThreads;
            ThreadPool.GetMinThreads(out minWorkerThreads, out minCompletionPortThreads);
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);
            MessageBox.Show("ThreadPool.Get{Max | Min}Threads"
                    + "\nworkerThreads= {" + minWorkerThreads+  " | " + maxWorkerThreads + " }"
                    + "\ncompletionPortThreads= {" + minCompletionPortThreads +" | " + maxCompletionPortThreads +" }");
        }

        private void myMutexAndThreadMethod(object o, bool timedOut)
        {
            Console.WriteLine("myMutexAndThreadMethod fired. State = " + (string)o
                +"\n\n" + (timedOut ? "called due to time out!" : "called due to mutex signaled"));
        }

        private void btnMutexAndThreadPool_Click(object sender, EventArgs e)
        {
            // with parameter in true the calling thread initially owns the the mutex
            Mutex m = new Mutex(true);

            ThreadPool.RegisterWaitForSingleObject(m
                        , new WaitOrTimerCallback(myMutexAndThreadMethod)
                        , "this is my state", Timeout.Infinite, true);

            // Signal the mutex to fire the thread
            m.ReleaseMutex();
        }

        private void myMethod(object o)
        {
            Console.WriteLine("myMethod fired!\nRunning on thread with id = \"{0}\" and value received \"{1}\"",
                Thread.CurrentThread.ManagedThreadId, (string)o);
        }

        private void RunOtherThreadToSyncWith(object state)
        {
            int id = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine("In the other thread, id: {0}", id);

            SynchronizationContext sc = (SynchronizationContext)state;

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);

                Console.WriteLine("\"Post\" to myMethod by other thread of \"line {0}\"", i);
                sc.Post(myMethod, String.Format("line {0} from thread {1}", i, id));
            }

        }

        private void btnSynchronizationContext_Click(object sender, EventArgs e)
        {
            // fork or not depending on context. 
            SynchronizationContext sc = SynchronizationContext.Current;

            int id = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine("\nIn the main thread, id: {0}", id);

            Thread thread = new Thread(RunOtherThreadToSyncWith);
            thread.Start(sc);

            Thread.Sleep(200);

            // Send
            Console.WriteLine("Before  sc.Send line. ManagedThreadId = " + id);
            sc.Send(myMethod, "some state");
            Console.WriteLine("After  sc.Send line. ManagedThreadId = " + id);

            // Post
            Console.WriteLine("Before  sc.Post line. ManagedThreadId = " + id);
            sc.Post(myMethod, "some state");
            Console.WriteLine("After  sc.Post line. ManagedThreadId = " + id);

        }


        private void myTimerCallbackMethod(object myState)
        {
            Console.WriteLine("myTimerCallbackMethod fired! myState={0}", ((MyState)myState).Time); 
        }

        private void btnTimerCallbackClass_Click(object sender, EventArgs e)
        {
            MyState myState = new MyState
            {
                Time = DateTime.Now
            };
            Console.WriteLine("Create timer.");
            System.Threading.Timer t =
                new System.Threading.Timer(new TimerCallback(myTimerCallbackMethod), myState, 500, 500);

            for (int i = 0; i < 10; i++)
            {
                myState.Time = DateTime.Now;
                Thread.Sleep(500);
            }
            t.Dispose();
            Console.WriteLine("Timer disposed.");
        }

        private class MyState
        {
            public DateTime Time { get; set; }
        }

        private void showAppDomainProperties(AppDomain ad)
        {
            StringBuilder sbAppDomainText = new StringBuilder(txtAppDomain.Text);

            sbAppDomainText.AppendLine("AppDomain.CurrentDomain");
            sbAppDomainText.AppendLine("-----------------------");
            sbAppDomainText.AppendLine("FriendlyName = " +ad.FriendlyName);
            sbAppDomainText.AppendLine("Id = " + ad.Id);
            sbAppDomainText.AppendLine("ShadowCopyFiles = " + ad.ShadowCopyFiles);
            sbAppDomainText.AppendLine("RelativeSearchPath = " + ad.RelativeSearchPath);
            sbAppDomainText.AppendLine("BaseDirectory = " + ad.BaseDirectory);
            sbAppDomainText.AppendLine();

            txtAppDomain.Text = sbAppDomainText.ToString();
        }

        private void btnAppDomainClass_Click(object sender, EventArgs e)
        {
            StringBuilder sbAppDomainText;
            showAppDomainProperties(AppDomain.CurrentDomain);

            sbAppDomainText = new StringBuilder(txtAppDomain.Text);

            sbAppDomainText.Append("\r\n");

            txtAppDomain.Text = sbAppDomainText.ToString();

            AppDomain ad = AppDomain.CreateDomain("myAppDomain");
            showAppDomainProperties(ad);

            // load assemblies
            // ad.ExecuteAssembly("myAssembly.exe");

            // unload
            AppDomain.Unload(ad);
        }

        private void btnEvidenceClass_Click(object sender, EventArgs e)
        {
            // Method 1 - evidence for a particular ExecuteAssembly call
            AppDomain ad = AppDomain.CreateDomain("myAppDomain");

            object[] hostEvidence = { new Zone(SecurityZone.MyComputer) };
            Evidence localEvidence = new Evidence(hostEvidence, null);
            // load assembly - enter a valid path of a valid assembly you want to execute
            ad.ExecuteAssembly(@"c:\temp\70536\fabianse70536.exe", localEvidence);

            // unload
            AppDomain.Unload(ad);

            // Method 2 - evidence for the entire AppDomain
            object[] ihostEvidence = { new Zone(SecurityZone.Internet) };
            Evidence iEvidence = new Evidence(ihostEvidence, null);
            AppDomain ad2 = AppDomain.CreateDomain("myAppDomain", iEvidence);

            // load assembly - enter a valid path!
            // THIS WILL FAIL DUE TO LACK OF EXECUTE PERMISSION FOR Internet Zone!!!
            try
            {
                ad2.ExecuteAssembly(@"c:\temp\70536\fabianse70536.exe");
            }
            catch (SecurityException es)
            {
                MessageBox.Show("SecurityException: Insufficient privileges to run assembly. \n"+es.Message);
            }

            // unload
            AppDomain.Unload(ad2);


        }

        private void btnAppDomainSetupClass_Click(object sender, EventArgs e)
        {
            AppDomainSetup appDomainSetup = new AppDomainSetup();
            // ensure that folder exists in your filesystem
            appDomainSetup.ApplicationBase = @"c:\temp\70536";
            appDomainSetup.DisallowBindingRedirects = false;
            appDomainSetup.DisallowCodeDownload = true;
            appDomainSetup.DisallowPublisherPolicy = true;
            appDomainSetup.ShadowCopyFiles = "true";
            appDomainSetup.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            AppDomain ad = AppDomain.CreateDomain("myAppDomain", null, appDomainSetup);
            ad.ExecuteAssembly(@"c:\temp\70536\fabianse70536.exe");
            AppDomain.Unload(ad);


        }

        private void btnServiceControllerClass_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController(textBoxServiceName.Text);
            sc.Stop();
        }

        private void btnServiceControllerClassStart_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController(textBoxServiceName.Text);
            //catch the possible exceptions
            try
            {
                sc.Start();
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show("Win32Exception when calling ServiceController.Start() :"
                    + Environment.NewLine + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("InvalidOperationException when calling ServiceController.Start() :"
                    + Environment.NewLine + ex.Message);
            }
        }

        private void btnConfigurationManagerClass_Click(object sender, EventArgs e)
        {
            // Method 1
            Configuration c1 = 
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Method 2
            ExeConfigurationFileMap ec = new ExeConfigurationFileMap();
            ec.ExeConfigFilename = Path.GetFileName(Application.ExecutablePath) + @".config";
            Configuration c2 =
                ConfigurationManager.OpenMappedExeConfiguration(ec, ConfigurationUserLevel.None);

            // Method 3
            Configuration c3 = ConfigurationManager.OpenMachineConfiguration();

            // Method 4
            ExeConfigurationFileMap ec2 = new ExeConfigurationFileMap();
            ec2.ExeConfigFilename = Path.GetFileName(Application.ExecutablePath) + @".config";
            Configuration c4 =
                ConfigurationManager.OpenMappedMachineConfiguration(ec2);

            // reading app settings
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            string s = appSettings["myAppSetting"];
            MessageBox.Show("myAppSetting = " + s);


            // take a look at WebConfigurationManager for ASP.NET
            //TODO: show the use of .Add and .Save
        }

        private void btnConnectionStringSettingsClass_Click(object sender, EventArgs e)
        {
            //browse connection strings
            ConnectionStringSettingsCollection csc = ConfigurationManager.ConnectionStrings;
            txtConfiguration.Text = "";
                
            foreach (ConnectionStringSettings cs in csc)
            {
                txtConfiguration.Text += Environment.NewLine;
                txtConfiguration.Text += "Name = " + cs.Name;
                txtConfiguration.Text += Environment.NewLine;
                txtConfiguration.Text += "------------------------------------";
                txtConfiguration.Text += Environment.NewLine;
                txtConfiguration.Text += "ProviderName = " + cs.ConnectionString;
                txtConfiguration.Text += Environment.NewLine;
                txtConfiguration.Text += cs.ConnectionString;
                txtConfiguration.Text += Environment.NewLine;
                txtConfiguration.Text += Environment.NewLine;

            }
        }

        private void btnsettingsFile_Click(object sender, EventArgs e)
        {
            Settings1 s = new Settings1();
            MessageBox.Show("ParticipantCount = " + s.ParticipantCount);
        }

        private void btnAssemblyInstallerClass_Click(object sender, EventArgs e)
        {
            IDictionary savedState = new Hashtable();
            try
            {
                AssemblyInstaller ai =
                    new AssemblyInstaller(Application.ExecutablePath, null);

                ai.UseNewContext = true;
                //ai.CommandLine = ;

                ai.Install(savedState);
                ai.Commit(savedState);
            }
            catch(ApplicationException ae)
            {
                MessageBox.Show(ae.Message);
            }

        
        }

        private void btnConfigurationGetSection_Click(object sender, EventArgs e)
        {
            Configuration c = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // if the configuration was already created in the .exe.config file, trying to add it again will throw an ArgumentException
            MyConfigurationSection section = c.Sections["MySection"] as MyConfigurationSection;
            if (section == null)
            {
                section = new MyConfigurationSection();
                c.Sections.Add("MySection", section);
            }
            section.Name = "name 2";
            section.Code = 33010;

            c.Save();

            // read
            MyConfigurationSection mc = (MyConfigurationSection)c.Sections["MySection"];
            MessageBox.Show("Name = " + mc.Name );


            ConfigurationSectionGroupCollection groupCollection = c.SectionGroups;
            foreach(ConfigurationSectionGroup sg in groupCollection)
            {
                Console.WriteLine("SectionGroup.Name={0}, SectionGroup.SectionGroupName={1}", sg.Name, sg.SectionGroupName);
            }
        }

        private void btnEventLogClass_Click(object sender, EventArgs e)
        {
            EventLog el = new EventLog("myLogName");
            el.Source = Path.GetFileName(Application.ExecutablePath);
            el.WriteEntry("I'm using WriteEntry. ", EventLogEntryType.Information);

            txtInstrumentation.Text = "";
            // browse
            foreach (EventLogEntry entry in el.Entries)
            {
                txtInstrumentation.Text += "Message = " + entry.Message;
                txtInstrumentation.Text += Environment.NewLine;
                txtInstrumentation.Text += "------------------------------------";
                txtInstrumentation.Text += Environment.NewLine;
            }

            // to clear/delete an event log
            // el.Clear();
            // EventLog.Delete("myLogName");

            //TODO: use EventLog.CreateEventSource
        }

        private void btnDebuggerClass_Click(object sender, EventArgs e)
        {
            Debugger.Break(); // only if not in Release build
        }

        private void btnDefaultTraceListenerClass_Click(object sender, EventArgs e)
        {
            Debug.Listeners.Clear();
            DefaultTraceListener dtl = new DefaultTraceListener();
            Debug.Listeners.Add(dtl);
            Debugger.Log(0, "Learning!", "this is a Debugger.Log message sent to the DefaultTraceListener");
            MessageBox.Show("you better check the output window in Visual Studio... something is happening there...");
        }

        private void btnDebugClass_Click(object sender, EventArgs e)
        {
            int fabianIntelligence = 126;
            const int minimumIntelligence = 130;
            Debug.Assert(fabianIntelligence >= minimumIntelligence
                , "IQ evaluation method failed."
                , "There must be an error in the IQ test, please verify it or hardcode the results in a politically correct way.");
        }

        private void btnDebugFail_Click(object sender, EventArgs e)
        {
            Debug.Fail("Not enough coffe to continue coding.");
            MessageBox.Show("this is shown after the fail message!");
        }

        private void btnDebugWrite_Click(object sender, EventArgs e)
        {
            Debug.Write(new Point());
            Debug.WriteLine("<-- :O ");
            Debug.WriteLineIf(1 < 2, "Natural numbers are still dependable enough. But make that backup anyway.");
            Debug.Print("This is a print message.");

            MessageBox.Show("you better check the output window in Visual Studio... something is happening there...");
            // see Debug.Print / Debug.Flush / etc.
        }

        [DebuggerDisplay("Autor = {author}, Title = {name}, Pages = {pageCount}" )]
        class MyBookClass 
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string author;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string name;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int pageCount;

            public MyBookClass(string _autor, string _name, int _pageCount)
            {
                author = _autor;
                name = _name;
                pageCount = _pageCount;
            }

            public string Author
            {
                get { return author; }
                set { author = value; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public int PageCount
            {
                get { return pageCount; }
                set { pageCount = value; }
            }


        }

        private void btnDebuggingAttributes_Click(object sender, EventArgs e)
        {
            MyBookClass b = new MyBookClass("fabianse", "Moving Planets with C# 10.0", 3);
            Debugger.Break();
        }

        [DebuggerHidden]
        private void btnDebuggerHiddenAttribute_Click(object sender, EventArgs e)
        {
            // try to put a breakpoint here and reach it while debugging
            int t = 1;
            t = t + 1;
            // after you fail, try thinking what will happend if you uncomment next line
            // so you can fail a second time, but in a brand new fashion
            Debugger.Break();

            // see remaining attributes
        }

        private void btnTraceSourceClass_Click(object sender, EventArgs e)
        {
            TraceSource ts = new TraceSource("MyTraceSource");
            // What happens if we uncomment this lines
            ts.Switch.ShouldTrace(TraceEventType.Information);
            ts.Switch.Level = SourceLevels.Information;
            ts.TraceInformation("TraceInformation output");
        }

        private void btnTextWriterTraceListenerClass_Click(object sender, EventArgs e)
        {
            Trace.Listeners.Clear();
            TextWriterTraceListener tw = new TextWriterTraceListener("TextWriterTraceListener.log");
            Trace.Listeners.Add(tw);
            Trace.AutoFlush = true;
            Trace.WriteLine("testing TextWriterTraceListener.");
        }

        private void btnProcessClass_Click(object sender, EventArgs e)
        {
            Process p = Process.GetCurrentProcess();
            Console.WriteLine("Current process name = ", p.ProcessName);
            Process[] processes = Process.GetProcesses();
            // GetProcesses can throw several exceptions!
            foreach (Process pr in processes)
            {
                Console.WriteLine("Process name = " + pr.ProcessName);
            }

            try
            {
                Process myProcess = Process.GetProcessById(9999999);
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("GetProcessById throwed an ArgumentException: "+ae.Message);
            }
        }

        private void btnCounterCreationDataClass_Click(object sender, EventArgs e)
        {
            CounterCreationData ccd = new CounterCreationData(
                "MyCounterName", "My Counter Help", PerformanceCounterType.SampleCounter);
            PerformanceCounterCategory.Create("myCategoryName", "My category help"
                , PerformanceCounterCategoryType.SingleInstance, "My counter name", "My counter help");

            //TODO: complete here or in a different project
        }

        private void btnPerformanceCounterClass_Click(object sender, EventArgs e)
        {
            //TODO: complete here or in a different project
            //TODO: pc.Increment / .Decrement / RawValue
        }

        private void btnProcessStartInfoClass_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "firefox.exe";
            psi.Arguments = "www.asp.net";
            // if we specify user and password what other thing we have to set?
            //psi.UserName =
            //psi.Password =
            //psi.UseShellExecute = 

            try
            {
                Process.Start(psi);
            }
            catch (Win32Exception we)
            {
                MessageBox.Show("Couldn't start firefox.exe. We get this exception: " + we.Message);
            }
        }

        private void btnStackTraceClass_Click(object sender, EventArgs e)
        {
            StackTrace st = new StackTrace();
            MessageBox.Show("Trace = \n"+st);
        }

        private void btnWMIClasses_Click(object sender, EventArgs e)
        {
            ConnectionOptions co = new ConnectionOptions();
            //co.Username = txtWMIUser.Text;
            //co.Password = txtWMIPassword.Text;

            ManagementScope ms = new ManagementScope(@"\\.", co);
            ObjectQuery q = new ObjectQuery(txtWMIQuery.Text);

            ManagementObjectSearcher mos = new ManagementObjectSearcher(ms, q);
            ManagementObjectCollection moc = mos.Get();

            foreach (ManagementObject mo in moc)
            {
                txtInstrumentation.Text += Environment.NewLine + "--- --- --- --- --- --- --- --- --- --- ";
                txtInstrumentation.Text += Environment.NewLine + mo.ClassPath;
                txtInstrumentation.Text += Environment.NewLine + mo.Path;
            }
        }

        ManagementEventWatcher watcher;

        private void btnManagementEventWatcherClass_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ManagementEventWatcher on thread " + Thread.CurrentThread.ManagedThreadId);
            // Create event query to be notified within 1 second of 
            // a change in a service
            WqlEventQuery query =
                new WqlEventQuery("__InstanceCreationEvent",
                new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");

            watcher = new ManagementEventWatcher();
            watcher.Query = query;
            watcher.Options.Timeout = new TimeSpan(0, 0, 25);
            watcher.EventArrived += watcher_EventArrived;

            MessageBox.Show("Close these and open a notepad to trigger an event." + 
                "\n This thread will now sleep for 25 seconds before stopping listening for events.");
            //ManagementBaseObject mbo = watcher.WaitForNextEvent();
            watcher.Start();

            ThreadPool.QueueUserWorkItem(stopTheWatcher);
        }

        private void stopTheWatcher(object context)
        {
            Console.WriteLine("stopTheWatcher on thread " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(25000);
            watcher.Stop();
        }

        private void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine("watcher_EventArrived on thread " + Thread.CurrentThread.ManagedThreadId);
            ManagementBaseObject mbo = e.NewEvent;
            txtInstrumentation.Text += Environment.NewLine + "--- --- --- --- --- --- --- --- --- --- ";
            txtInstrumentation.Text += Environment.NewLine
                 + "Name = " + ((ManagementBaseObject)mbo["TargetInstance"])["Name"]
                 + " ; ExecutablePath = " + ((ManagementBaseObject)mbo["TargetInstance"])["ExecutablePath"];
        }

        [FileIOPermission(SecurityAction.Demand , Write=@"c:\windows\system")]
        private void MyMethod1DeclarativeCAS()
        {
            MessageBox.Show("FileIOPermission is granted for method MyMethod1DeclarativeCAS.");
        }

        [WebPermission(SecurityAction.Demand , ConnectPattern="www.msn.com")]
        private void MyMethod2DeclarativeCAS()
        {
            MessageBox.Show("WebPermission is granted for method MyMethod2DeclarativeCAS.");
        }

        private void btnMethodDeclarativeCAS_Click(object sender, EventArgs e)
        {
            MyMethod1DeclarativeCAS();
            MyMethod2DeclarativeCAS();

            // Imperative 
            try
            {
                FileIOPermission fiop = new FileIOPermission(FileIOPermissionAccess.Write, @"c:\windows");
                fiop.Demand(); // checks the caller
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                WebPermission wp = new WebPermission(NetworkAccess.Connect, "www.msn.com");
                wp.Demand(); // checks the caller
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // check the assembly
            FileIOPermission fp = new FileIOPermission(FileIOPermissionAccess.Write, @"c:\windows");
            if (SecurityManager.IsGranted(fp))
            {
                MessageBox.Show("SecurityManager.IsGranted returned TRUE for FileIOPermission.");
            }
            else
            {
                MessageBox.Show("SecurityManager.IsGranted returned FALSE for FileIOPermission.");
            }


        }

        private void btnAssertCAS_Click(object sender, EventArgs e)
        {
            FileIOPermission fiop = new FileIOPermission(FileIOPermissionAccess.Write, @"c:\windows");
            // remember SecurityPermissionFlag.Assertion
            
            fiop.Assert(); 
            // what happens in subsequent calls?
            // what happens when we call Assert multiple times in a method?

        }

        private void btnPermissionSetClass_Click(object sender, EventArgs e)
        {
            FileIOPermission fiop = new FileIOPermission(FileIOPermissionAccess.Write, @"c:\windows");
            WebPermission wp = new WebPermission(NetworkAccess.Connect, "www.msn.com");

            PermissionSet ps = new PermissionSet(PermissionState.None);
            ps.AddPermission(fiop);
            ps.AddPermission(wp);

            ps.Assert();
        }

        private void btnWindowsIdentityClass_Click(object sender, EventArgs e)
        {
            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            txtSecurity.Clear();
            txtSecurity.Text += Environment.NewLine + "WindowsIdentity.GetCurrent()";
            txtSecurity.Text += Environment.NewLine + "----------------------------";
            txtSecurity.Text += Environment.NewLine + ".Name = " + wi.Name;
            txtSecurity.Text += Environment.NewLine + ".Authentication type = " + wi.AuthenticationType;
            txtSecurity.Text += Environment.NewLine + ".IsAnonymous = " + wi.IsAnonymous;
            txtSecurity.Text += Environment.NewLine + ".IsAuthenticated = " + wi.IsAuthenticated;
            txtSecurity.Text += Environment.NewLine + ".IsGuest = " + wi.IsGuest;
            txtSecurity.Text += Environment.NewLine + ".IsSystem = " + wi.IsSystem;

        }

        private IEnumerable<T> EnumerateEnumeration<T>()
        {
            if (!typeof(T).IsSubclassOf(typeof(Enum))) throw new Exception("T must be an Enum");
            int[] Values = (int[])Enum.GetValues(typeof(T));
            foreach (int i in Values)
            {
                yield return (T)Enum.ToObject(typeof(T), i);
            }
        }

        private void btnWindowsPrincipalClass_Click(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            WindowsPrincipal wp = (WindowsPrincipal)Thread.CurrentPrincipal;

            txtSecurity.Clear();
            txtSecurity.Text += Environment.NewLine + "WindowsPrincipal / WindowsBuiltInRole";
            txtSecurity.Text += Environment.NewLine + "-------------------------------------";

            foreach (WindowsBuiltInRole br in EnumerateEnumeration<WindowsBuiltInRole>())
            {
                txtSecurity.Text += Environment.NewLine + br + " = " + wp.IsInRole(br);
            }
        }

        private void btnPrincipalPermissionClass_Click(object sender, EventArgs e)
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal wPrincipal = new WindowsPrincipal(id);
            // several overloads - can we use next line in an french windows installation?
            //bool status = wPrincipal.IsInRole(@"BUILTIN\Administrators"); 
            bool status = wPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
            try
            {
                string role = txtSecurityGroupname.Text; //e.g. @"BUILTIN\Administrators"
                // regarding next line, what can we do if we don't need to check for user name?
                PrincipalPermission MyPermission = new PrincipalPermission(id.Name, role);
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                MyPermission.Demand();
                MessageBox.Show("PrincipalPermission MyPermission.Demand() successful");
            }
            catch (SecurityException ew)
            {
                MessageBox.Show("We found this error: " + ew.Message);
            }
        }

        private void btnGenericIdentityClass_Click(object sender, EventArgs e)
        {
            GenericIdentity user1 = new GenericIdentity("fabianse", "IRIS");
            GenericPrincipal principal1 = new GenericPrincipal(user1, new string[] { "Coffee Drinkers", "Tennis Players", "Developers" });
            
            // test the role - why we put this outside of the try/catch?
            if (!principal1.IsInRole("French Speakers"))
                MessageBox.Show("The user don't speaks the language of love.");

            try
            {
                // add imperative demand
                Thread.CurrentPrincipal = principal1;
                PrincipalPermission pp = new PrincipalPermission("fabianse", "Coffee Drinkers");
                pp.Demand();
                MessageBox.Show("but it appears to be in the role of Cofee Drinkers");
            }
            catch (SecurityException ex)
            {
                MessageBox.Show("You should check the code because we found this error on permission.Demand(): " + Environment.NewLine + ex.Message);
            }
        }

        private void btnAuthorizationRuleCollectionClass_Click(object sender, EventArgs e)
        {
            txtSecurity.Clear();
            txtSecurity.Text += Environment.NewLine + "RegistrySecurity: for Registry.CurrentUser";
            RegistrySecurity rs = Registry.CurrentUser.GetAccessControl();
            AuthorizationRuleCollection arc = rs.GetAccessRules(true, true, typeof(NTAccount));
            foreach (RegistryAccessRule rar in arc)
            {
                txtSecurity.Text += Environment.NewLine + "------ ------ ------ ------ ------ ------ ";
                txtSecurity.Text += Environment.NewLine + ".IdentityReference=" + rar.IdentityReference;
                txtSecurity.Text += Environment.NewLine + ".AccessControlType=" + rar.AccessControlType;
                txtSecurity.Text += Environment.NewLine + ".RegistryRights=" + rar.RegistryRights;
                txtSecurity.Text += Environment.NewLine + ".IsInherited=" + rar.IsInherited;
            }

            // Note:
            // to add an access rule
            // 1) rs.AddAccessRule(RegistryAccessRule Rule)
            // 2) Registry.CurrentUser.SetAccessControl(rs);
            // to remove
            // use RemoveAccessRule instead.

            // now the same but for DirectorySecurity. Note we still use the same AuthorizationRuleCollection variable
            txtSecurity.Text += Environment.NewLine + Environment.NewLine + "------ ------ ------ ------ ------ ------ ";
            txtSecurity.Text += Environment.NewLine + "DirectorySecurity: for C:\\";

            DirectorySecurity ds = new DirectorySecurity(@"C:\", AccessControlSections.Access);
            arc = ds.GetAccessRules(true, true, typeof(NTAccount));
            foreach (FileSystemAccessRule far in arc)
            {
                txtSecurity.Text += Environment.NewLine + "------ ------ ------ ------ ------ ------ ";
                txtSecurity.Text += Environment.NewLine + ".IdentityReference=" + far.IdentityReference;
                txtSecurity.Text += Environment.NewLine + ".AccessControlType=" + far.AccessControlType;
                txtSecurity.Text += Environment.NewLine + ".FileSystemRights=" + far.FileSystemRights;
                txtSecurity.Text += Environment.NewLine + ".IsInherited=" + far.IsInherited;
            }

        }

        private void btnRijndaelManagedClass_Click(object sender, EventArgs e)
        {
            RijndaelManaged rm = new RijndaelManaged();
            byte[] salt = Encoding.UTF8.GetBytes("encode this");
            // let's generate Key and IV as a funtion of the user provided password
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes("MyPassword", salt);
            rm.Key = key.GetBytes(rm.KeySize / 8);
            rm.IV = key.GetBytes(rm.BlockSize / 8);

            // encrypt the file
            FileStream fsOriginal = File.Open("SimpleTextFile.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fsOriginal);

            FileStream fsEncrypted = File.Create("SimpleTextFile_Encrypted.txt");
            CryptoStream cs = new CryptoStream(fsEncrypted, rm.CreateEncryptor(), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(sr.ReadToEnd());
            sw.Close();
            cs.Close();
            fsOriginal.Close();

            // decrypt file into another file
            fsEncrypted = File.Open("SimpleTextFile_Encrypted.txt", FileMode.Open);
            cs = new CryptoStream(fsEncrypted, rm.CreateDecryptor(), CryptoStreamMode.Read);
            sr = new StreamReader(cs);

            FileStream fsDecrypted = File.Create("SimpleTextFile_Decrypted.txt");
            sw = new StreamWriter(fsDecrypted);
            string dec = sr.ReadToEnd();
            sw.Write(dec);
            sw.Close();
            cs.Close();
            fsEncrypted.Close();

            // show file content on the textbox
            txtEncryption.Text = dec;
        }

        private void btnRSACryptoServiceProviderClass_Click(object sender, EventArgs e)
        {
            txtEncryption.Clear();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            // RSA keys are called parameters
            // export to parameters
            RSAParameters param = rsa.ExportParameters(false);
            
            // or export to xml
            string s = rsa.ToXmlString(true);
            txtEncryption.Text = s.Replace("><", ">\r\n<"); //put a new line for visibility in the form

            string original = "encrypted string";
            // encrypt a message
            byte[] message = Encoding.ASCII.GetBytes(original);
            byte[] encrypted = rsa.Encrypt(message, false);

            // decrypt that message
            byte[] decrypted = rsa.Decrypt(encrypted, false);


            txtEncryption.Text += Environment.NewLine + Environment.NewLine + "Original = " + original;
            txtEncryption.Text += Environment.NewLine + "Encrypted = " + Encoding.ASCII.GetString(encrypted);
            txtEncryption.Text += Environment.NewLine + "Decrypted = " + Encoding.ASCII.GetString(decrypted);
        }

        private void btnCspParametersClass_Click(object sender, EventArgs e)
        {
            // CSP = Crypto Service Provider
            CspParameters cspp = new CspParameters();
            // what happens if we run the app several times changing or not the key container name?
            cspp.KeyContainerName = "MyKeyContainerName";

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            string s = rsa.ToXmlString(true);
            txtEncryption.Text += Environment.NewLine + "----------------------------";
            txtEncryption.Text += Environment.NewLine + s.Replace("><", ">\r\n<");

        }

        private void btnMD5CryptoServiceProviderClass_Click(object sender, EventArgs e)
        {
            txtEncryption.Clear();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            string original = "calculate hash for this string";
            // encrypt a message
            byte[] message = Encoding.Unicode.GetBytes(original);
            byte[] hash = md5.ComputeHash(message);
            txtEncryption.Text += Environment.NewLine + Environment.NewLine + "Original = " + original;
            txtEncryption.Text += Environment.NewLine + "Hash = " + Encoding.Unicode.GetString(hash);

        }

        private void btnHMACSHA1Class_Click(object sender, EventArgs e)
        {
            // keyed hash algorithm - a hash algorithm that requires a key
            txtEncryption.Clear();
            byte[] salt = Encoding.UTF8.GetBytes("encode this");
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes("MyPassword", salt);
            byte[] bytepk = key.GetBytes(16);

            HMACSHA1 alg = new HMACSHA1(bytepk);

            string original = "calculate hash for this string";
            // encrypt a message
            byte[] message = Encoding.UTF8.GetBytes(original);
            byte[] hash = alg.ComputeHash(message);
            txtEncryption.Text += Environment.NewLine + Environment.NewLine + "Original = " + original;
            txtEncryption.Text += Environment.NewLine + "Hash = " + Encoding.UTF8.GetString(hash);
            
        }

        private void btnDSACryptoServiceProviderClass_Click(object sender, EventArgs e)
        {
            //TODO: DSACryptoServiceProviderClass
            //MSDN: http://msdn.microsoft.com/en-us/library/system.security.cryptography.dsacryptoserviceprovider.aspx
            
        }

        private void btnAssemblyClass_Click(object sender, EventArgs e)
        {
            Assembly a = Assembly.GetExecutingAssembly();

            txtReflection.Clear();
            txtReflection.Text += Environment.NewLine + "FullName = " + a.FullName;
            txtReflection.Text += Environment.NewLine + "EntryPoint.Name = " + a.EntryPoint.Name;
            txtReflection.Text += Environment.NewLine + "EntryPoint.GlobalAssemblyCache = " + a.GlobalAssemblyCache;
            txtReflection.Text += Environment.NewLine + "EntryPoint.Location = " + a.Location;

            txtReflection.Text += Environment.NewLine + Environment.NewLine + "GetExportedTypes : ";
            foreach (Type t in a.GetExportedTypes())
            {
                txtReflection.Text += Environment.NewLine + t.Name;
            }

            txtReflection.Text += Environment.NewLine + Environment.NewLine + "GetLoadedModules : ";
            foreach (Module t in a.GetLoadedModules())
            {
                txtReflection.Text += Environment.NewLine + t.Name;
            }

            txtReflection.Text += Environment.NewLine + Environment.NewLine + "GetCustomAttributes : ";
            foreach (object t in a.GetCustomAttributes(false))
            {
                txtReflection.Text += Environment.NewLine + t.GetType().Name;
            }

            object objAssemblyFileVersion = a.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
            string version = ((AssemblyFileVersionAttribute[])objAssemblyFileVersion)[0].Version;
            txtReflection.Text += Environment.NewLine + Environment.NewLine + "AssemblyFileVersionAttribute = " + version;


        }

        private void btnTypeClass_Click(object sender, EventArgs e)
        {
            // that string that is always so intrigant
            Type t = typeof(String);
            txtReflection.Clear();
            txtReflection.Text += Environment.NewLine + "Type t = typeof(String);";
            txtReflection.Text += Environment.NewLine + "IsByRef = " + t.IsByRef;
            txtReflection.Text += Environment.NewLine + "IsValueType = " + t.IsValueType;
            txtReflection.Text += Environment.NewLine + "FullName = " + t.FullName;
            txtReflection.Text += Environment.NewLine + "Namespace = " + t.Namespace;
            txtReflection.Text += Environment.NewLine + "IsClass = " + t.IsClass;
            txtReflection.Text += Environment.NewLine + "IsPublic = " + t.IsPublic;
            txtReflection.Text += Environment.NewLine + "IsAbstract = " + t.IsAbstract;

        }

        public void btnMethodBodyClass_Click(object sender, EventArgs e)
        {
            txtReflection.Clear();

            Type t = this.GetType();
            // exercise: what happens if we change next string with other method name. Can also be explored?
            string s = "btnMethodBodyClass_Click";
            txtReflection.Text += Environment.NewLine + "Exploring methodbody of btnMethodBodyClass_Click";

            MethodInfo i = t.GetMethod(s);
            MethodBody m = i.GetMethodBody();
            IList<LocalVariableInfo> lviList = m.LocalVariables;
            foreach (LocalVariableInfo lvi in lviList)
            {
                txtReflection.Text += Environment.NewLine + lvi.LocalType.Name;
            }

            // exercise: in the previous foreach try to output the local variable names instead of their index and type

            byte[] b = m.GetILAsByteArray();
            txtReflection.Text += Environment.NewLine + "lenght of GetILAsByteArray() = " + b.Length;
        }

        private void btnBindingsFlag_Click(object sender, EventArgs e)
        {
            BindingFlags bf = BindingFlags.Public | BindingFlags.NonPublic;

            MemberInfo[] members = this.GetType().GetMembers(bf);

            foreach (MemberInfo m in members)
            {
                txtReflection.Text += Environment.NewLine + "MemberInfo[x].Name = " + m.Name;
            }

            // review all the values of BindingFlags
        }

        private void btnConstructorInfoClass_Click(object sender, EventArgs e)
        {
            AssemblyName an = new AssemblyName("mscorlib.dll");
            Assembly a = Assembly.Load(an);

            Type typeAL = a.GetType("System.Collections.ArrayList");
            // we want a parameterless constructor
            Type[] typeArg = Type.EmptyTypes;
            ConstructorInfo ci = typeAL.GetConstructor(typeArg);

            // invoke the constructor
            object al = ci.Invoke(new object[] { });

            // get the value of Count property
            PropertyInfo pi = al.GetType().GetProperty("Count");
            int myCount = (int)pi.GetValue(al, null);

            txtReflection.Clear();
            txtReflection.Text += Environment.NewLine + "type of dynamically created object is = " + al.GetType().FullName;
            txtReflection.Text += Environment.NewLine + ".Count = " + myCount;

            txtReflection.Text += Environment.NewLine + "invoke Add method.";
            MethodInfo mi = al.GetType().GetMethod("Add");
            mi.Invoke(al, new object[] { "Element 1" });

            myCount = (int)pi.GetValue(al, null);
            txtReflection.Text += Environment.NewLine + ".Count = " + myCount;

            // what do we have to do to invoke static methods.
        }

        private void btnAssemblyBuilderClass_Click(object sender, EventArgs e)
        {
            AssemblyName aName = new AssemblyName("myAssemblyName");
            AssemblyBuilder ab = 
                AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder mb = ab.DefineDynamicModule("myModule", "myAssembly.dll");
            TypeBuilder tb = mb.DefineType("myType", TypeAttributes.Class | TypeAttributes.Public);
            ConstructorBuilder cb = tb.DefineDefaultConstructor(MethodAttributes.Public);

            ILGenerator ilg = cb.GetILGenerator();
            ilg.Emit(OpCodes.Ret);

            MethodBuilder mtb = tb.DefineMethod("myMethod", MethodAttributes.Public, null, Type.EmptyTypes);

            MethodBuilder mtb2 = 
                tb.DefineMethod("stringLength", MethodAttributes.Public, typeof(int), new Type[] { typeof(string) });

            FieldBuilder fb = tb.DefineField("myData", typeof(int), FieldAttributes.Private);
            PropertyBuilder pb = tb.DefineProperty("MyData", System.Reflection.PropertyAttributes.None, typeof(int), Type.EmptyTypes);

        }

        private void btnMailMessageClass_Click(object sender, EventArgs e)
        {
            MailMessage msgMail = new MailMessage(txtMailFrom.Text, txtMailTo.Text);

            msgMail.Subject = "About this Screens...";

            msgMail.IsBodyHtml = chkMailIsHTMLBody.Checked;
            msgMail.Body = txtMailBody.Text;

            //include a text to attach a file
            //msgMail.Attachments.Add(new Attachment(maindirectory + "\\" + filename));

            SmtpClient smtp = new SmtpClient(txtMailHost.Text, int.Parse(txtMailPort.Text));
            smtp.Send(msgMail);

            //TODO: show special case with credentials
        }

        private void btnCultureInfoClass_Click(object sender, EventArgs e)
        {
            txtGlobalization.Clear();
            txtGlobalization.Text += Environment.NewLine + "CultureInfo";
            txtGlobalization.Text += Environment.NewLine + "-----------";

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            txtGlobalization.Text += Environment.NewLine + ".Name = " + ci.Name;
            txtGlobalization.Text += Environment.NewLine + ".DisplayName = " + ci.DisplayName;
            txtGlobalization.Text += Environment.NewLine + ".EnglishName = " + ci.EnglishName;
            txtGlobalization.Text += Environment.NewLine + ".NativeName = " + ci.NativeName;
            txtGlobalization.Text += Environment.NewLine + ".IsNeutralCulture = " + ci.IsNeutralCulture;
            txtGlobalization.Text += Environment.NewLine + ".TwoLetterISOLanguageName = " + ci.TwoLetterISOLanguageName;
            txtGlobalization.Text += Environment.NewLine + ".ThreeLetterISOLanguageName = " + ci.ThreeLetterISOLanguageName;

            txtGlobalization.Text += Environment.NewLine + "1 millon and... a half monetary units = " + (1000000.50).ToString("C");
            txtGlobalization.Text += Environment.NewLine + "DateTime.Now.ToLongDateString = " + DateTime.Now.ToLongDateString();
            txtGlobalization.Text += Environment.NewLine + "DateTime.Now.ToShortDateString = " + DateTime.Now.ToShortDateString();

            // is there any particularity or restriction in where to change CurrentUICulture ?
            txtGlobalization.Text += Environment.NewLine + "Thread.CurrentThread.CurrentUICulture.Name = " + Thread.CurrentThread.CurrentUICulture.Name;
            
        }

        private void btnGetCulturesMethod_Click(object sender, EventArgs e)
        {
            txtGlobalization.Clear();

            CultureInfo ciBkp = Thread.CurrentThread.CurrentCulture;
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.FrameworkCultures))
            {
                txtGlobalization.Text += Environment.NewLine + "--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ";
                txtGlobalization.Text += Environment.NewLine + ".Name = " + ci.Name;
                txtGlobalization.Text += Environment.NewLine + ".DisplayName = " + ci.DisplayName;
                txtGlobalization.Text += Environment.NewLine + ".EnglishName = " + ci.EnglishName;
                txtGlobalization.Text += Environment.NewLine + ".NativeName = " + ci.NativeName;
                txtGlobalization.Text += Environment.NewLine + ".IsNeutralCulture = " + ci.IsNeutralCulture;

                if (!ci.IsNeutralCulture)
                {
                    // why we set Thread.CurrentThread.CurrentCulture here?
                    Thread.CurrentThread.CurrentCulture = ci;
                    txtGlobalization.Text += Environment.NewLine + "1 millon and a half monetary units = " + (1000000.50).ToString("C");
                    txtGlobalization.Text += Environment.NewLine + "DateTime.Now.ToLongDateString = " + DateTime.Now.ToLongDateString();
                    txtGlobalization.Text += Environment.NewLine + "DateTime.Now.ToShortDateString = " + DateTime.Now.ToShortDateString();
                }

            }

            Thread.CurrentThread.CurrentCulture = ciBkp;
        }

        private void btnRegionInfoClass_Click(object sender, EventArgs e)
        {
            txtGlobalization.Clear();

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            RegionInfo ri = new RegionInfo(ci.LCID);
            txtGlobalization.Text += Environment.NewLine + ".Name = " + ri.Name;
            txtGlobalization.Text += Environment.NewLine + ".DisplayName = " + ri.DisplayName;
            txtGlobalization.Text += Environment.NewLine + ".EnglishName = " + ri.EnglishName;
            txtGlobalization.Text += Environment.NewLine + ".NativeName = " + ri.NativeName;
            txtGlobalization.Text += Environment.NewLine + ".CurrencySymbol = " + ri.CurrencySymbol;
            txtGlobalization.Text += Environment.NewLine + ".CurrencyEnglishName = " + ri.CurrencyEnglishName;
            txtGlobalization.Text += Environment.NewLine + ".CurrencyNativeName = " + ri.CurrencyNativeName;
            txtGlobalization.Text += Environment.NewLine + ".IsMetric = " + ri.IsMetric;
            
        }

        private void btnDateTimeFormatInfoClass_Click(object sender, EventArgs e)
        {
            txtGlobalization.Clear();

            CultureInfo ci = new CultureInfo("es-AR");
            txtGlobalization.Text += Environment.NewLine + ".Name = " + ci.Name;
            txtGlobalization.Text += Environment.NewLine + ".DisplayName = " + ci.DisplayName;
            txtGlobalization.Text += Environment.NewLine + ".EnglishName = " + ci.EnglishName;
            txtGlobalization.Text += Environment.NewLine + Environment.NewLine + "DateTimeFormatInfo";
            DateTimeFormatInfo dtfi = ci.DateTimeFormat;
            DayOfWeek fdow = dtfi.FirstDayOfWeek;
            txtGlobalization.Text += Environment.NewLine + ".FirstDayOfWeek = " + dtfi.FirstDayOfWeek;
            txtGlobalization.Text += Environment.NewLine + "DayNames = " ;
            foreach (string day in dtfi.DayNames)
            {
                txtGlobalization.Text += day +"; ";
            }
            txtGlobalization.Text += Environment.NewLine + "MonthNames = ";
            foreach (string month in dtfi.MonthNames)
            {
                txtGlobalization.Text += month + "; ";
            }
            txtGlobalization.Text += Environment.NewLine + ".FullDateTimePattern = " + dtfi.FullDateTimePattern;
            txtGlobalization.Text += Environment.NewLine + ".RFC1123Pattern = " + dtfi.RFC1123Pattern;
            txtGlobalization.Text += Environment.NewLine + ".ShortTimePattern = " + dtfi.ShortTimePattern;
            txtGlobalization.Text += Environment.NewLine + ".AMDesignator = " + dtfi.AMDesignator;

        }

        private void btnCompareInfoClass_Click(object sender, EventArgs e)
        {
            txtGlobalization.Clear();
            string[] result = { "<", "=", ">" };
            string a;
            string b;

            CompareInfo cmpi = new CultureInfo("es-AR").CompareInfo;
            txtGlobalization.Text += Environment.NewLine + ".Name = " + cmpi.Name;
            a = "ñandú";
            b = "ñandu";
            txtGlobalization.Text += Environment.NewLine + a + " " + result[1 + cmpi.Compare(a, b)] + " " + b;
            a = "ñandú";
            b = "Ñandú";
            txtGlobalization.Text += Environment.NewLine + a + " " + result[1 + cmpi.Compare(a, b, CompareOptions.IgnoreCase)] + " " + b;

            cmpi = new CultureInfo("fr-FR").CompareInfo;
            txtGlobalization.Text += Environment.NewLine;
            txtGlobalization.Text += Environment.NewLine + ".Name = " + cmpi.Name;
            a = "côté";
            b = "coté";
            txtGlobalization.Text += Environment.NewLine + a + " " + result[1 + cmpi.Compare(a, b)] + " " + b;



        }

        private void btnCultureAndRegionInfoBuilderClass_Click(object sender, EventArgs e)
        {
            // what did we have to do to avoid the not defined/ArgumentException message?
            CultureAndRegionInfoBuilder b = new CultureAndRegionInfoBuilder("en-US-C#", CultureAndRegionModifiers.None);
            b.RegionNativeName = "sqr(C++)";
            //TODO: expand example
        }

        private void btnIntegerTypes_Click(object sender, EventArgs e)
        {
            txtTypes.Clear();

            Stopwatch w = new Stopwatch();
            w.Start();
            for (Int16 t = 0; t < 10000; t++)
            {
                string s = "";
                s += " ";
            }
            w.Stop();
            txtTypes.Text += Environment.NewLine + "Int16 iterations = " + w.ElapsedTicks;

            w.Reset();
            w.Start();
            for (Int32 t = 0; t < 10000; t++)
            {
                string s = "";
                s += " ";
            }
            w.Stop();
            txtTypes.Text += Environment.NewLine + "Int32 iterations = " + w.ElapsedTicks;

            w.Reset();
            w.Start();
            for (Int64 t = 0; t < 10000; t++)
            {
                string s = "";
                s += " ";
            }
            w.Stop();
            txtTypes.Text += Environment.NewLine + "Int64 iterations = " + w.ElapsedTicks;

        }

        enum MyEnumDays { Sat = 1, Sun, Mon, Tue, Wed, Thu, Fri };
        struct MyStruct { int x; int y; public MyStruct(int a, int b) { x = a; y = b; } }
        class MyObject { public int x; public MyObject(int a) { x = a; } }

        private void btnValueTypes_Click(object sender, EventArgs e)
        {
            txtTypes.Clear();
            Type t;
            t = typeof(Int32); 
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;
            
            t = typeof(String);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;

            t = typeof(Char);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;

            t = typeof(DateTime);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;

            t = typeof(string);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;

            t = typeof(Exception);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;

            t = typeof(Array);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;

            t = typeof(StringBuilder);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;

            t = typeof(MyEnumDays);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;

            // are we saying that this is a reference type?
            t = typeof(System.Enum);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;

            t = typeof(System.ValueType);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;

            t = typeof(MyStruct);
            txtTypes.Text += Environment.NewLine + Environment.NewLine + t.Name + " - " + (t.IsValueType ? "Value" : "Reference") + " - BaseType: " + t.BaseType + " - FullName: " + t.FullName + " - Namespace: " + t.Namespace;
            
            // nullable
            txtTypes.Text += Environment.NewLine;
            int? i;
            // what happen if we uncomment next line
            //txtTypes.Text += Environment.NewLine + "i.HasValue = " + i.HasValue;
            i = null;
            txtTypes.Text += Environment.NewLine + "i = null; ";
            txtTypes.Text += Environment.NewLine + "i.HasValue = " + i.HasValue;
            i = 1;
            txtTypes.Text += Environment.NewLine + "i = 1; ";
            txtTypes.Text += Environment.NewLine + "i.HasValue = " + i.HasValue;

            txtTypes.Text += Environment.NewLine;
            MyObject a = new MyObject(5);
            MyObject b = a;
            b.x = 3;
            txtTypes.Text += Environment.NewLine + "a.x = " + a.x;
            txtTypes.Text += Environment.NewLine + "b.x = " + b.x;

            txtTypes.Text += Environment.NewLine;
            int a2 = 50;
            int b2 = a2;
            b2 = 10;
            txtTypes.Text += Environment.NewLine + "a2 = " + a2;
            txtTypes.Text += Environment.NewLine + "b2 = " + b2;

            txtTypes.Text += Environment.NewLine;
            string a3 = "value 1";
            string b3 = a3;
            b3 = "value 2";
            txtTypes.Text += Environment.NewLine + "a3 = " + a3;
            txtTypes.Text += Environment.NewLine + "b3 = " + b3;


        }

        private void btnStringClass_Click(object sender, EventArgs e)
        {
            txtTypes.Clear();

            string s = "this";
            s = s + " is";
            s = String.Concat(s, " a string!");
            txtTypes.Text += Environment.NewLine + "s = " + s;

            string[] a = { "first", "second", "third" };
            txtTypes.Text += Environment.NewLine + "a = " + String.Join(" ; " , a);
            
            // can we create an array without dimension? try uncommenting next line
            //string[] myVariable = new string[];
            string[] b = new string[3];
            b[0] = "red";
            b[1] = "green";
            b[2] = "blue";
            txtTypes.Text += Environment.NewLine + "b = " + String.Join(" ; ", b);

            string[,] c = new string[2,2];
            c[0, 0] = "upper left";
            c[0, 1] = "upper right";
            c[1, 0] = "bottom left";
            c[1, 1] = "bottom right";
            txtTypes.Text += Environment.NewLine + "c.Length = " + c.Length;
            txtTypes.Text += Environment.NewLine + "c.Rank = " + c.Rank;


        }

        private void btnArrayClass_Click(object sender, EventArgs e)
        {
            txtTypes.Clear();

            string[] a = { "1", "2", "3" };
            string[] b = new String[3];
            b[0] = "1";
            b[1] = "2";
            b[2] = "3";

            int[,] c = { { 1, 2, 3 }, { 4, 5, 6 } };
            // exercise: try uncommenting next line, then compile and run it... if you can.
            // c[3, 3] = 8;
            c[1, 2] = 8;

            int[,] d = new int[2,3];
            d[0, 0] = 1;
            d[0, 1] = 2;
            d[0, 2] = 3;
            d[1, 0] = 4;
            d[1, 1] = 5;
            d[1, 2] = 6;

            // jagged
            int[][] f = new int[2][];
            f[0] = new int[2];
            f[0][0] = 1;
            f[0][1] = 2;
            f[1] = new int[2];
            f[1][0] = 3;
            f[1][1] = 4;
            //TODO: expand the jagged example

            // direct assignment
            int[][] g = f;
            g[0][0] = 9;
            txtTypes.Text += Environment.NewLine + "f[0][0] = " + f[0][0];
            txtTypes.Text += Environment.NewLine + "g[0][0] = " + g[0][0];

            // copy
            txtTypes.Text += Environment.NewLine;
            int[][] h = new int[2][];
            Array.Copy(f, h, 2);
            h[0][0] = 13;
            txtTypes.Text += Environment.NewLine + "f[0][0] = " + f[0][0];
            txtTypes.Text += Environment.NewLine + "h[0][0] = " + h[0][0];

            // copy in second dimension
            txtTypes.Text += Environment.NewLine;
            int[][] i = new int[2][];
            i[0] = new int[2];
            i[1] = new int[2];
            Array.Copy(f[0], i[0], 2);
            Array.Copy(f[1], i[1], 2);
            i[0][0] = 47;
            txtTypes.Text += Environment.NewLine + "f[0][0] = " + f[0][0];
            txtTypes.Text += Environment.NewLine + "i[0][0] = " + i[0][0];

        }

        private void btnFileStreamClass_Click(object sender, EventArgs e)
        {
            txtIO.Clear();

            // direct reading
            txtIO.Text += Environment.NewLine + Environment.NewLine + "direct reading";
            txtIO.Text += Environment.NewLine + "-------------";
            FileStream fs = File.Open("SimpleTextFile.txt", FileMode.Open);
            byte[] b = new byte[fs.Length];
            int count = fs.Read(b, 0, b.Length);
            txtIO.Text += Environment.NewLine + System.Text.Encoding.ASCII.GetString(b);
            fs.Close(); // try commenting this line

            // reader
            txtIO.Text += Environment.NewLine + Environment.NewLine + "Reader";
            txtIO.Text += Environment.NewLine + "-------------";
            FileStream fs2 = File.Open("SimpleTextFile.txt", FileMode.Open);
            StreamReader sr2 = new StreamReader(fs2);
            txtIO.Text += Environment.NewLine + sr2.ReadToEnd();
            sr2.Close(); // what happens with the underlying stream?

            // OpenText
            txtIO.Text += Environment.NewLine + Environment.NewLine + "OpenText";
            txtIO.Text += Environment.NewLine + "-------------";
            StreamReader sr3 = File.OpenText("SimpleTextFile.txt");
            txtIO.Text += Environment.NewLine + sr3.ReadToEnd();
            sr3.Close();

            // Open by StreamReader
            txtIO.Text += Environment.NewLine + Environment.NewLine + "Open by StreamReader";
            txtIO.Text += Environment.NewLine + "------------------------";
            StreamReader sr4 = new StreamReader("SimpleTextFile.txt");
            txtIO.Text += Environment.NewLine + sr4.ReadToEnd();
            sr4.Close();


            // open an existing file
            FileStream fs3 = File.Open("SimpleTextFile.txt", FileMode.Open);
            fs3.Close();
            FileStream fs4 = new FileStream("SimpleTextFile.txt", FileMode.Open, FileAccess.Read);
            fs4.Close();
            FileStream fs5 = new FileStream("SimpleTextFile.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            fs5.Close();

            // open or create
            FileStream fs6 = File.Open("SimpleTextFile.txt", FileMode.OpenOrCreate);
            fs6.Close();
            FileStream fs7 = new FileStream("SimpleTextFile.txt", FileMode.OpenOrCreate, FileAccess.Read);
            fs7.Close();

            // create - direct writing
            if (File.Exists("temp.txt"))  File.Delete("temp.txt");
            FileStream fs8 = File.Create("temp.txt");
            string s = "this will be written to the file." + Environment.NewLine + "In multiple lines.";
            byte[] b2 = System.Text.Encoding.ASCII.GetBytes(s);
            fs8.Write(b2,0,b2.Length);
            fs8.Close();

            // create - StreamWriter writer
            if (File.Exists("temp2.txt")) File.Delete("temp2.txt");
            FileStream fs9 = File.Create("temp2.txt");
            StreamWriter sw = new StreamWriter(fs9);
            sw.Write("this will be written to the file." + Environment.NewLine + "In multiple lines.");
            sw.Close();

            // CreateText 
            StreamWriter sw2 = File.CreateText("temp4.txt");
            sw2.WriteLine("first line");
            sw2.WriteLine("second line");
            sw2.Close();

            // Create by StreamWriter
            StreamWriter sw3 = new StreamWriter("temp5.txt");
            sw3.WriteLine("first line");
            sw3.WriteLine("second line");
            sw3.Close();

            // AppendAllText
            if (File.Exists("temp6.txt")) File.Delete("temp6.txt");
            File.AppendAllText("temp6.txt", "write this istring into the file");

            // AppendText
            if (File.Exists("temp7.txt")) File.Delete("temp7.txt");
            StreamWriter sw4 = File.AppendText("temp7.txt");
            sw4.Write("write this istring into the file");
            sw4.Close();

            // create
            //FileStream fs10 = File.Create("temp_encrypted.txt", 1024, FileOptions.Encrypted); // create or overwrite
            //StreamWriter sw5 = new StreamWriter(fs10);
            //sw5.Write("write this istring into the file");
            //sw5.Close(); // now go and see this file in the filesystem folder /bin/debug if debugguing. Check its properties.

            try
            {
                FileStream fs11 = new FileStream("temp3.txt", FileMode.CreateNew); // create only if not exists
                fs11.Close();
            }
            catch (System.IO.IOException ioe)
            {
                MessageBox.Show("error trying to create with FileMode.CreateNew. File must not previously exist. Message: " +  ioe.Message);
            }

            FileStream fs12 = new FileStream("temp3.txt", FileMode.Create); // create or overwrite
            fs12.Close();
            FileStream fs13 = new FileStream("temp3.txt", FileMode.Create, FileAccess.ReadWrite); // create or overwrite
            fs13.Close();

        }

        private void btnMemoryStreamClass_Click(object sender, EventArgs e)
        {
            txtIO.Clear();
            txtIO.Text += "MemoryStream";
            txtIO.Text += Environment.NewLine + "----------------";

            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.Write("all this information is written in a MemoryStream");
            sw.Flush();

            txtIO.Text += Environment.NewLine + ".CanRead " + ms.CanRead;
            txtIO.Text += Environment.NewLine + ".CanSeek " + ms.CanSeek;
            txtIO.Text += Environment.NewLine + ".CanTimeout " + ms.CanTimeout;
            txtIO.Text += Environment.NewLine + ".CanWrite " + ms.CanWrite;
            txtIO.Text += Environment.NewLine + ".Capacity " + ms.Capacity;
            txtIO.Text += Environment.NewLine + ".Length " + ms.Length;
            txtIO.Text += Environment.NewLine + ".Position " + ms.Position;
            if (ms.CanTimeout)
            {
                txtIO.Text += Environment.NewLine + ".ReadTimeout " + ms.ReadTimeout;
                txtIO.Text += Environment.NewLine + ".WriteTimeout " + ms.WriteTimeout;
            }

            // excercise: try moving this line just after the sw.Flush() and run the code
            sw.Close(); 
            
        }

        private void btnFileClass_Click(object sender, EventArgs e)
        {
            txtIO.Text = "File.ReadAllLines";
            string[] lines = File.ReadAllLines("SimpleTextFile.txt");
            txtIO.Text += String.Join(Environment.NewLine, lines);

            string s = File.ReadAllText("SimpleTextFile.txt");
            txtIO.Text += Environment.NewLine + Environment.NewLine + "File.ReadAllText";
            txtIO.Text += Environment.NewLine + s;

            txtIO.Text += Environment.NewLine + Environment.NewLine + "FileInfo ";
            FileInfo fi = new FileInfo("SimpleTextFile.txt");
            txtIO.Text += Environment.NewLine + ".Attributes = " + fi.Attributes;
            txtIO.Text += Environment.NewLine + ".CreationTime = " + fi.CreationTime;
            txtIO.Text += Environment.NewLine + ".CreationTimeUtc = " + fi.CreationTimeUtc;
            txtIO.Text += Environment.NewLine + ".DirectoryName = " + fi.DirectoryName;
            txtIO.Text += Environment.NewLine + ".Exists = " + fi.Exists;
            txtIO.Text += Environment.NewLine + ".Extension = " + fi.Extension;
            txtIO.Text += Environment.NewLine + ".FullName = " + fi.FullName;
            txtIO.Text += Environment.NewLine + ".IsReadOnly = " + fi.IsReadOnly;
            txtIO.Text += Environment.NewLine + ".LastAccessTime = " + fi.LastAccessTime;
            txtIO.Text += Environment.NewLine + ".Length = " + fi.Length;
            txtIO.Text += Environment.NewLine + ".Name = " + fi.Name;

        }

        private void btnDirectoryClass_Click(object sender, EventArgs e)
        {
            txtIO.Text = "Directory";

            if (Directory.Exists(@".\created Dir"))
            {
                try
                {
                    Directory.Delete(@".\created Dir", true);
                }
                catch (IOException ioe)
                {
                    MessageBox.Show(@"The directory \created Dir could not be deleted. Ensure the application has enough permission and the directory isn't locked. \n" +ioe.Message);
                }
            }
            // exercise: does CreateDirectory fails if the folder already exists?
            Directory.CreateDirectory(@".\created Dir\child 1\child 11");
            Directory.CreateDirectory(@".\created Dir\child 2");

            string[] s = Directory.GetDirectories(@".\created Dir");
            txtIO.Text += Environment.NewLine + "Directories 1st level = " + String.Join(" ; ",s);

            s = Directory.GetDirectories(@".\created Dir", "*", SearchOption.AllDirectories);
            txtIO.Text += Environment.NewLine + "Directories recursive = " + String.Join(" ; ",s);

            txtIO.Text += Environment.NewLine + Environment.NewLine + "DirectoryInfo ";
            DirectoryInfo di = new DirectoryInfo(@".\created Dir");
            txtIO.Text += Environment.NewLine + ".Attributes = " + di.Attributes;
            txtIO.Text += Environment.NewLine + ".CreationTime = " + di.CreationTime;
            txtIO.Text += Environment.NewLine + ".CreationTimeUtc = " + di.CreationTimeUtc;
            txtIO.Text += Environment.NewLine + ".Exists = " + di.Exists;
            txtIO.Text += Environment.NewLine + ".FullName = " + di.FullName;
            txtIO.Text += Environment.NewLine + ".LastAccessTime = " + di.LastAccessTime;
            txtIO.Text += Environment.NewLine + ".LastWriteTime = " + di.LastWriteTime;
            txtIO.Text += Environment.NewLine + ".Name = " + di.Name;
            txtIO.Text += Environment.NewLine + ".Parent.Name = " + di.Parent.Name;
            txtIO.Text += Environment.NewLine + ".Root.Name = " + di.Root.Name;

            txtIO.Text += Environment.NewLine + Environment.NewLine + "Directory.GetLogicalDrives ";
            s = Directory.GetLogicalDrives();
            txtIO.Text += Environment.NewLine + String.Join(" ; ", s);

            txtIO.Text += Environment.NewLine + Environment.NewLine + "DriveInfo of " + s[0];
            DriveInfo driveInfo = new DriveInfo(s[0]);
            txtIO.Text += Environment.NewLine + ".AvailableFreeSpace = " + driveInfo.AvailableFreeSpace;
            txtIO.Text += Environment.NewLine + ".DriveFormat = " + driveInfo.DriveFormat;
            txtIO.Text += Environment.NewLine + ".DriveType = " + driveInfo.DriveType;
            txtIO.Text += Environment.NewLine + ".IsReady = " + driveInfo.IsReady;
            txtIO.Text += Environment.NewLine + ".Name = " + driveInfo.Name;
            txtIO.Text += Environment.NewLine + ".RootDirectory = " + driveInfo.RootDirectory;
            txtIO.Text += Environment.NewLine + ".TotalFreeSpace = " + driveInfo.TotalFreeSpace;
            txtIO.Text += Environment.NewLine + ".TotalSize = " + driveInfo.TotalSize;
            txtIO.Text += Environment.NewLine + ".VolumeLabel = " + driveInfo.VolumeLabel;

            // move a dir
            Directory.Move(@".\created Dir\child 1\child 11", @".\created Dir\child 2\child 11");

        }

        static void myFileSystemEventHandler(object sender, FileSystemEventArgs e)
        {
            Console.Write(Environment.NewLine + Environment.NewLine + "------------------------------------------");
            Console.Write(Environment.NewLine + Environment.NewLine + "New change in the filesystem ");
            Console.Write(Environment.NewLine + ".ChangeType " + e.ChangeType);
            Console.Write(Environment.NewLine + ".FullPath " + e.FullPath);
            Console.Write(Environment.NewLine + ".Name " + e.Name);
        }

        static void myErrorEventHanlder(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.GetException());
        }

        private void btnFileSystemWatcherClass_Click(object sender, EventArgs e)
        {
            txtIO.Clear();
            FileSystemWatcher fsw = new FileSystemWatcher(@"C:\");
            fsw.Created += new FileSystemEventHandler(myFileSystemEventHandler);
            fsw.Deleted += new FileSystemEventHandler(myFileSystemEventHandler);
            // when too many errors occurs this event will be fired
            fsw.Error += new ErrorEventHandler(myErrorEventHanlder);

            fsw.EnableRaisingEvents = true;
            MessageBox.Show(@"Create or delete a file in C:\ and see notifications in the Console ( debug output window ).");
        }

        private void btnPathClass_Click(object sender, EventArgs e)
        {
            txtIO.Clear();
            string filename = Path.GetFullPath("SimpleTextFile.txt");
            txtIO.Text += Environment.NewLine + "Path for file:";
            txtIO.Text += Environment.NewLine + ".GetFullPath = " + Path.GetFullPath("SimpleTextFile.txt");
            txtIO.Text += Environment.NewLine + ".GetFileName = " + Path.GetFileName(filename);
            txtIO.Text += Environment.NewLine + ".GetExtension = " + Path.GetExtension(filename);
            txtIO.Text += Environment.NewLine + ".GetFileNameWithoutExtension = " + Path.GetFileNameWithoutExtension(filename);
            txtIO.Text += Environment.NewLine + ".GetRandomFileName = " + Path.GetRandomFileName();
            txtIO.Text += Environment.NewLine + ".GetTempPath = " + Path.GetTempPath();
            txtIO.Text += Environment.NewLine + ".HasExtension = " + Path.HasExtension(filename);
            txtIO.Text += Environment.NewLine + ".Combine(\"c:\\dir1\\\",@\"dir2\\file.txt\") = " + Path.Combine(@"c:\dir1\", @"dir2\file.txt");
            txtIO.Text += Environment.NewLine + ".ChangeExtension(filename, \"doc\") = " + Path.ChangeExtension(filename, "doc");

            //TODO: expand the Path.Combine with different examples and results
        }

        private void btnBufferedStreamClass_Click(object sender, EventArgs e)
        {
            txtIO.Clear();
            Stopwatch chronometer = new Stopwatch();

            FileStream fs2 = File.Create("Stream without buffer.txt");
            StreamWriter sw2 = new StreamWriter(fs2);
            chronometer.Reset();
            chronometer.Start();
            for (int t = 0; t < 10000; t++)
            {
                sw2.Write(":)" + new String('A',100));
            }
            sw2.Close();
            chronometer.Stop();
            txtIO.Text += Environment.NewLine + "non buffered writes ElapsedTicks = " + chronometer.ElapsedTicks;

            
            FileStream fs = File.Create("Stream with buffer.txt");
            BufferedStream bs = new BufferedStream(fs);
            StreamWriter sw = new StreamWriter(bs);
            chronometer.Reset();
            chronometer.Start();
            for (int t = 0; t < 10000; t++)
            {
                sw.Write(":)" + new String('A', 100));
            }
            sw.Close();
            chronometer.Stop();
            txtIO.Text += Environment.NewLine + "buffered writes ElapsedTicks = " + chronometer.ElapsedTicks;


        }

        private void btnStringReaderClass_Click(object sender, EventArgs e)
        {
            txtIO.Clear();

            string s = File.ReadAllText("SimpleTextFile.txt");
            StringReader sr = new StringReader(s);
            txtIO.Text = sr.ReadToEnd();

            StringWriter sw = new StringWriter();
            sw.WriteLine("line 1");
            sw.WriteLine("line 2");
            sw.WriteLine("line 3");
            txtIO.Text += Environment.NewLine + Environment.NewLine + sw.ToString();

            // Additionally: use the StringWriter to write strings to a stream
        }

        private void btnBinaryReaderClass_Click(object sender, EventArgs e)
        {
            FileStream fs = File.Create("BinaryWriter.txt"); // it is not text, it's binary.
            BinaryWriter br = new BinaryWriter(fs);
            string m = "Actual date"; 
            br.Write(m);
            DateTime now = DateTime.Now;
            br.Write(now.Ticks);
            br.Close();
            txtIO.Text += Environment.NewLine + "Values to write";
            txtIO.Text += Environment.NewLine + "s = " + m;
            txtIO.Text += Environment.NewLine + "dt = " + now;

            BinaryReader reader = new BinaryReader(File.Open("BinaryWriter.txt", FileMode.Open));
            string s = reader.ReadString();
            DateTime dt = new DateTime(reader.ReadInt64());
            txtIO.Text += Environment.NewLine + "Readed values";
            txtIO.Text += Environment.NewLine + "s = " + s;
            txtIO.Text += Environment.NewLine + "dt = " + dt;

        }

        private void btnArrayListClass_Click(object sender, EventArgs e)
        {
            txtCollections.Clear();
            ArrayList al = new ArrayList();
            al.Add("first element");
            al.Add(22);
            al.Add(new int());
            string[] a = { "a", "b", "c" };
            al.AddRange( a );
            al.Insert(0, "inserting before first!");
            al.Add("a"); // let's add a duplicate

            ArrayList secondArray = new ArrayList();
            secondArray.AddRange(new string[] { "<foreign element 1>", "<foreign element 2>" });

            al.InsertRange(4, secondArray);

            secondArray[1] = "<modified! foreign element 2>";
            try
            {
                secondArray[2] = ":O";
            }
            catch (ArgumentOutOfRangeException)
            { 
                // argument our of range!
            }

            txtCollections.Text += "---------------------------";
            txtCollections.Text += Environment.NewLine + "ArrayList:";
            txtCollections.Text += Environment.NewLine + ".Count = " + al.Count;
            txtCollections.Text += Environment.NewLine + ".Capacity = " + al.Capacity;
            txtCollections.Text += Environment.NewLine + ".IsFixedSize = " + al.IsFixedSize;
            txtCollections.Text += Environment.NewLine + ".IsReadOnly = " + al.IsReadOnly;
            txtCollections.Text += Environment.NewLine + ".IsSynchronized = " + al.IsSynchronized;
            txtCollections.Text += Environment.NewLine + ".Contains(\"a\") = " + al.Contains("a");
            txtCollections.Text += Environment.NewLine + ".IndexOf(\"a\") = " + al.IndexOf("a");
            txtCollections.Text += Environment.NewLine + ".LastIndexOf(\"a\") = " + al.LastIndexOf("a");
            txtCollections.Text += Environment.NewLine + ".ToString() = " + al.ToString();
            txtCollections.Text += Environment.NewLine + ".ToArray().Length = " + al.ToArray().Length;

            // iterate
            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "foreach:";
            foreach (object o in al)
            {
                txtCollections.Text += Environment.NewLine + o + " : " + o.GetType().Name;
            }

            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "GetEnumerator:";
            IEnumerator ie = al.GetEnumerator();
            ie.Reset();
            while (ie.MoveNext())
            {
                txtCollections.Text += Environment.NewLine + ie.Current + " : " + ie.Current.GetType().Name;
            }

            // lock
            lock (al.SyncRoot)
            {
                foreach (Object item in al)
                {
                    // do something
                }
            }

            // get a new synchronized version 
            ArrayList synchronized = ArrayList.Synchronized(al);
            txtCollections.Text += Environment.NewLine + Environment.NewLine + "ArrayList synchronized = ArrayList.Synchronized(al);";
            txtCollections.Text += Environment.NewLine + "synchronized.IsSynchronized = " + synchronized.IsSynchronized;


            //TODO: demonstrate sort in other method
            //TODO: ICollection, IList
            //TODO: show the behaviour of Capacity property, its default value and increments/decrements
            //TODO: Sort an arraylist with objects that implement IComparable and sort by passing a custom IComparer to the sort method
            //TODO: use TrimToSize / BinarySearch / Reverse
        }

        private void btnQueueClass_Click(object sender, EventArgs e)
        {
            txtCollections.Clear();
            Queue q = new Queue();
            q.Enqueue("1");
            q.Enqueue("2");
            q.Enqueue(3);
            q.Enqueue("4");
            IEnumerator ie = q.GetEnumerator();

            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "GetEnumerator:";
            while (ie.MoveNext())
            {
                txtCollections.Text += Environment.NewLine + ie.Current + " : " + ie.Current.GetType().Name;
            }

            // try uncommenting next line
            // q[0] = "Uno";

            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "Dequeue:";
            while (q.Count > 0)
            {
                if (q.Peek().GetType() == typeof(Int32))
                {
                    // do something
                    int t = (int)q.Peek();
                }
                object o = q.Dequeue();
                txtCollections.Text += Environment.NewLine + o + " : " + o.GetType().Name;
            }


        }

        private void btnStackClass_Click(object sender, EventArgs e)
        {
            txtCollections.Clear();
            Stack s = new Stack();
            s.Push("1");
            s.Push("2");
            s.Push(3);
            s.Push("4");
            IEnumerator ie = s.GetEnumerator();

            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "GetEnumerator:";
            while (ie.MoveNext())
            {
                txtCollections.Text += Environment.NewLine + ie.Current + " : " + ie.Current.GetType().Name;
            }

            // try uncommenting next line
            //s[0] = "Uno";

            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "Pop:";
            while (s.Count > 0)
            {
                if (s.Peek().GetType() == typeof(Int32))
                {
                    // do something
                    int t = (int)s.Peek();
                }
                object o = s.Pop();
                txtCollections.Text += Environment.NewLine + o + " : " + o.GetType().Name;
            }


        }

        private void btnHashtableClass_Click(object sender, EventArgs e)
        {
            txtCollections.Clear();
            Hashtable ht = new Hashtable();
            ht.Add(1, 100);
            ht.Add("welcome message", "hello world!");
            try
            {
                ht.Add("welcome message", "hola mundo!");
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("Can't add duplicate key. Item has already been added. "  + ae.Message);
            }

            // what happens here?
            ht["Antofagasta"] = "99";
            ht["Antofagasta"] = "-1";



            txtCollections.Text += "---------------------------";
            txtCollections.Text += Environment.NewLine + "Hashtable:";
            txtCollections.Text += Environment.NewLine + ".Count = " + ht.Count;
            txtCollections.Text += Environment.NewLine + ".IsFixedSize = " + ht.IsFixedSize;
            txtCollections.Text += Environment.NewLine + ".IsReadOnly = " + ht.IsReadOnly;
            txtCollections.Text += Environment.NewLine + ".IsSynchronized = " + ht.IsSynchronized;
            txtCollections.Text += Environment.NewLine + ".Contains(\"welcome message\") = " + ht.Contains("welcome message");
            txtCollections.Text += Environment.NewLine + "ht[\"welcome message\"] = " + ht["welcome message"];
            txtCollections.Text += Environment.NewLine + ".ContainsKey(\"welcome message\") = " + ht.ContainsKey("welcome message");
            txtCollections.Text += Environment.NewLine + ".ContainsValue(\"hola mundo!\") = " + ht.ContainsKey("hola mundo!");
            txtCollections.Text += Environment.NewLine + ".ToString() = " + ht.ToString();
            txtCollections.Text += Environment.NewLine + ".Keys.Count = " + ht.Keys.Count;
            txtCollections.Text += Environment.NewLine + ".Values.Count = " + ht.Values.Count;

            // iterate
            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "foreach:";
            foreach (object o in ht)
            {
                txtCollections.Text += Environment.NewLine + o + " : " + o.GetType().Name;
            }

            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "foreach: DictionaryEntry";
            foreach (DictionaryEntry de in ht)
            {
                txtCollections.Text += Environment.NewLine +"Key=value : "+ de.Key + "  = " + de.Value;
            }


            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "GetEnumerator:";
            IEnumerator ie = ht.GetEnumerator();
            ie.Reset();
            while (ie.MoveNext())
            {
                txtCollections.Text += Environment.NewLine + "Key=value : " + ((DictionaryEntry)ie.Current).Key + "  = " + ((DictionaryEntry)ie.Current).Value;
            }

            
            // can we add duplicated values to hashtable?
            // can we access ht by index? 

            //TODO: show other dictionaries like StringDictionary/ ListDictionary/ HybridDictionary/ NameValueCollection
        }

        private void btnSortedListClass_Click(object sender, EventArgs e)
        {
            txtCollections.Clear();
            SortedList sl = new SortedList(StringComparer.CurrentCulture);
            sl["Alice"] = 1;
            sl["Laura"] = 2;
            sl["Brian"] = 3;

            txtCollections.Text += "---------------------------";
            txtCollections.Text += Environment.NewLine + "SortedList:";

            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "foreach: ";
            foreach (DictionaryEntry de in sl)
            {
                txtCollections.Text += Environment.NewLine + "Key=value : " + de.Key + "  = " + de.Value;
            }

            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "access by index / GetKey / GetByIndex: ";
            for (int t = 0; t < sl.Count; t++)
            {
                txtCollections.Text += Environment.NewLine + "Key=value : " + sl.GetKey(t) + "  = " + sl.GetByIndex(t);

            }
        }

        private void btnBitArrayClass_Click(object sender, EventArgs e)
        {
            txtCollections.Clear();
            BitArray ba = new BitArray(3);
            txtCollections.Text += Environment.NewLine + "length = " + ba.Length +" ; count = " + ba.Count;
            txtCollections.Text += Environment.NewLine;
            foreach (bool value in ba)
            {
                txtCollections.Text += value + " ";
            }

            BitArray ba2 = new BitArray(new byte[3] { 1, 2, 3 });
            txtCollections.Text += Environment.NewLine;
            txtCollections.Text += Environment.NewLine + "length = " + ba2.Length + " ; count = " + ba2.Count;
            int t = 0;
            foreach (bool value in ba2)
            {
                if (t % 8 == 0) txtCollections.Text += Environment.NewLine;
                t++;
                txtCollections.Text +=  value + " ";
            }


        }

        private void btnBitVector32Class_Click(object sender, EventArgs e)
        {
            BitVector32 bitStruct = new BitVector32();
            bitStruct[0] = true;
            bitStruct[31] = true;
            // exercise: what's the difference between BitArray and BitVector32 ?

        }


        private void btnGenericDictionaryClass_Click(object sender, EventArgs e)
        {
            txtCollections.Clear();
            Dictionary<DateTime, string> d = new Dictionary<DateTime, string>();
            DateTime NowTime = DateTime.Now;
            d.Add(NowTime, "now");
            d.Add(NowTime.AddDays(1), "tomorrow");
            try
            {
                // can we add a duplicate key / value?
                d.Add(NowTime.AddDays(1), "tomorrow");
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("Can't add duplicate key / value. Item has already been added. " + ae.Message);
            }

            // exercise: what happens here?
            d[NowTime] = "now value modified!";


            txtCollections.Text += "---------------------------";
            txtCollections.Text += Environment.NewLine + "Hashtable:";
            txtCollections.Text += Environment.NewLine + ".Count = " + d.Count;
            txtCollections.Text += Environment.NewLine + "d[NowTime] = " + d[NowTime];
            txtCollections.Text += Environment.NewLine + ".ContainsKey(NowTime.AddDays(1)) = " + d.ContainsKey(NowTime.AddDays(1));
            txtCollections.Text += Environment.NewLine + ".ToString() = " + d.ToString();
            txtCollections.Text += Environment.NewLine + ".Keys.Count = " + d.Keys.Count;
            txtCollections.Text += Environment.NewLine + ".Values.Count = " + d.Values.Count;

            // iterate
            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "foreach:";
            foreach (object o in d)
            {
                txtCollections.Text += Environment.NewLine + o + " : " + o.GetType().Name;
            }

            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "foreach: KeyValuePair<DateTime,string>";
            foreach (KeyValuePair<DateTime,string> de in d)
            {
                txtCollections.Text += Environment.NewLine + "Key=value : " + de.Key + "  = " + de.Value;
            }


            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "GetEnumerator:";
            IEnumerator ie = d.GetEnumerator();
            txtCollections.Text += Environment.NewLine + "Enumerator class name:" + ie.ToString();
            ie.Reset();
            while (ie.MoveNext())
            {
                txtCollections.Text += Environment.NewLine + "Key=value : " + ((KeyValuePair<DateTime, string>)ie.Current).Key + "  = " + ((KeyValuePair<DateTime, string>)ie.Current).Value;
            }
            
            // browse Keys
            txtCollections.Text += Environment.NewLine + Environment.NewLine + "---------------------------";
            txtCollections.Text += Environment.NewLine + "d.Keys class name:" + d.Keys.GetType().ToString();
            txtCollections.Text += Environment.NewLine + "foreach: DateTime dt in d.Keys";
            foreach (DateTime dt in d.Keys)
            {
                txtCollections.Text += Environment.NewLine + dt;
            }

            //TODO: browse Values
        }


        private void btnGenericSortedListClass_Click(object sender, EventArgs e)
        {
            txtCollections.Clear();
            SortedList<string,string> gsl = new SortedList<string,string>();
            gsl.Add("my key", "my value");
            gsl.Add("zebra", "black and white");
            gsl.Add("aligator", "green and brown");
            foreach (KeyValuePair<string, string> kv in gsl)
            {
                txtCollections.Text += Environment.NewLine + kv.Key + " = " + kv.Value;
            }
            txtCollections.Text += Environment.NewLine + Environment.NewLine + "Values (CSV)= " + String.Join(",", gsl.Values.ToArray<string>());

            // excercise: can we add duplicated values and/or keys?
            txtCollections.Text += Environment.NewLine + "look at the code behind the button.";
        }

        private void btnGenericListClass_Click(object sender, EventArgs e)
        {
            txtCollections.Clear();
            List<string> list = new List<string>();
            list.Add("z");
            list.Add("a");
            list.Add("m");
            txtCollections.Text += Environment.NewLine + "List content:";
            foreach (string s in list)
            {
                txtCollections.Text += Environment.NewLine + s;
            }

            txtCollections.Text += Environment.NewLine + Environment.NewLine + "List content after sort:";
            list.Sort();
            foreach (string s in list)
            {
                txtCollections.Text += Environment.NewLine + s;
            }

            // exercise: create a custom class and a List<MyClass> and used in the same code on this example.

            //TODO: show SortedList<> and other <>
        }


        private void btnGZipStreamClass_Click(object sender, EventArgs e)
        {
            //TODO: GZipStream Class
        }

        private void btnIsolatedStorageFileClass_Click(object sender, EventArgs e)
        {
            //TODO: IsolatedStorageFile & IsolatedStorageFileStream Class
        }

        private void btnRegexIsMatch_Click(object sender, EventArgs e)
        {
            txtRegEx.Clear();
            if (Regex.IsMatch(txtRegExInput.Text, txtRegExExpression.Text))
            {
                txtRegEx.Text += Environment.NewLine + "Input text matches regular expression.";
            }
            else 
            {
                txtRegEx.Text += Environment.NewLine + "Input text DOES NOT match regular expression.";
            }
        }

        private void btnEncodingClass_Click(object sender, EventArgs e)
        {
            txtEncoding.Clear();
            string s = "Salut tout le monde! à â æ è é ê ë ç";
            byte[] encodedBytes = null;
            string bytesInStringFormat = "";
            string encodingName = "";
            Encoding encoding = null;

            txtEncoding.Text += Environment.NewLine + s;
            // get french canadian encoding
            encodingName = "IBM863";
            encoding = Encoding.GetEncoding(encodingName);
            encodedBytes = encoding.GetBytes(s);
            bytesInStringFormat = BitConverter.ToString(encodedBytes);
            txtEncoding.Text += Environment.NewLine + bytesInStringFormat + " in page " + encodingName ;

            // Unicode utf-8 
            encodingName = "utf-8";
            encoding = Encoding.GetEncoding(encodingName);
            encodedBytes = encoding.GetBytes(s);
            bytesInStringFormat = BitConverter.ToString(encodedBytes);
            txtEncoding.Text += Environment.NewLine + bytesInStringFormat + " in page " + encodingName;

            //TODO: more samples on convert http://msdn.microsoft.com/en-us/library/system.text.encoding.aspx
            //TODO: specify code page when writing to stream/network/file/chat application
        }

        private void btnEncodingInfoClass_Click(object sender, EventArgs e)
        {
            txtEncoding.Clear();
            txtEncoding.Text += Environment.NewLine + "List of code pages: display name; name; code page #";

            EncodingInfo[] eiArray = Encoding.GetEncodings();
            foreach(EncodingInfo ei in eiArray)
            {
                txtEncoding.Text += Environment.NewLine + ei.DisplayName + "; " + ei.Name + "; " + ei.CodePage;
            }
        }

        private void btnBinaryFormatterClass_Click(object sender, EventArgs e)
        {
            txtSerialization.Clear();
            string fileName = "BinaryFormatterOutput.bin";
            string strToSerialize = "this string will be serialized to file";
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, strToSerialize);
            fs.Close();

            txtSerialization.Text += Environment.NewLine + "A file was created at:";
            txtSerialization.Text += Environment.NewLine + fs.Name;
            txtSerialization.Text += Environment.NewLine + "we serialized the following string into it using BinaryFormatter: ";
            txtSerialization.Text += Environment.NewLine + strToSerialize;
            txtSerialization.Text += Environment.NewLine + "open it with notepad or a binary editor to see the content.";

            //TODO: serialize and deserialize complex object graphs
            //TODO: implement custom actions after Deserializing
        }

        [Serializable]
        public class MySimplePersonSerializableClass
        {
            string name;
            DateTime bornDate;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public DateTime BornDate
            {
                get { return bornDate; }
                set { bornDate = value; }
            }
        }

        private void btnXmlSerializerClass_Click(object sender, EventArgs e)
        {
            txtSerialization.Clear();
            MySimplePersonSerializableClass p = new MySimplePersonSerializableClass();
            p.Name = "Noami";
            p.BornDate = DateTime.Now.AddYears(-55);
            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(MySimplePersonSerializableClass));
            serializer.Serialize(sw, p);
            txtSerialization.Text += Environment.NewLine + "The object was serialized to string using XmlSerializer. Result:";
            txtSerialization.Text += Environment.NewLine + sw.ToString();

            // exercise: remove the public keyword of the class MySimplePersonSerializableClass, and run this code again.
            // exercise 2: as a different exercise, remove the [Serializable] attribute from that class and see what happens.

        }

        private void btnISerializableInterface_Click(object sender, EventArgs e)
        {
            //TODO: implement ISerializable
        }
    }
}
