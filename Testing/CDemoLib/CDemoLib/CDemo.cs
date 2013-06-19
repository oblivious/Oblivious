using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CDemoLib
{
    public partial class CDemo : Component
    {
        public readonly int InstanceID;
        private static int NextInstanceID = 0;
        private static long ClassInstanceCount = 0;

        public CDemo()
        {
            InitializeComponent();
            InstanceID = NextInstanceID++;
            ClassInstanceCount++;
        }

        ~CDemo()
        {
            ClassInstanceCount--;
        }

        public CDemo(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public static long InstanceCount
        {
            get { return ClassInstanceCount; }
        }
    }
}
