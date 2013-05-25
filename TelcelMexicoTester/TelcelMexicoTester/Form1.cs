using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EDTS.DirectTopUpServices.OperatorGateWay;
using EDTS.DirectTopUpServices.IOperatorGateWay;

namespace TelcelMexicoTester
{
	public partial class Form1 : Form
	{
		TelcelMexico gateway;

		public Form1()
		{
			InitializeComponent();
			gateway = new TelcelMexico();
		}

		private void sendLocalServiceStartupButton_Click(object sender, EventArgs e)
		{
			
		}
	}
}
