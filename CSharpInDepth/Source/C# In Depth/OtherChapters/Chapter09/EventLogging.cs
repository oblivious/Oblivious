﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Chapter09
{
    [Description("Listing 9.05")]
    class EventLogging
    {
        static void Log(string title, object sender, EventArgs e)
        {
            Console.WriteLine("Event: {0}", title);
            Console.WriteLine("  Sender: {0}", sender);
            Console.WriteLine("  Arguments: {0}", e.GetType());
            foreach (PropertyDescriptor prop in
                     TypeDescriptor.GetProperties(e))
            {
                string name = prop.DisplayName;
                object value = prop.GetValue(e);
                Console.WriteLine("    {0}={1}", name, value);
            }
        }

        static void Main()
        {
            Button button = new Button();
            button.Text = "Click me";
            button.Click += (src, e) => Log("Click", src, e);
            button.KeyPress += (src, e) => Log("KeyPress", src, e);
            button.MouseClick += (src, e) => Log("MouseClick", src, e);

            Form form = new Form();
            form.AutoSize = true;
            form.Controls.Add(button);
            Application.Run(form);
        }
    }
}
