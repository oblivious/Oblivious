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
using EDTS.DirectTopUpServices.BusinessEntities;

namespace TelcelMexicoTester
{
	public partial class Form1 : Form
	{
		private TelcelMexico gateway;
		private const string IP_ADDRESS = "IP:";
		private const string TOTAL_TIME = "TOTAL TIME:";
		private const string COLUMN_TIME = "TotalTime";
		private const string LINE_NUMBER = "ln_";

		private static Form1 instance = null;

		public static Form1 GetInstance()
		{
			if (instance == null)
				instance = new Form1();
			return instance;
		}

		public Form1()
		{
			InitializeComponent();
			gateway = new TelcelMexico();
		}

		private void sendLocalServiceStartupButton_Click(object sender, EventArgs e)
		{
			try
			{
				SendOperatorTopUpPhoneAccountRequestStatus status = gateway.SendLocalServiceStartup(null, null);
				this.WriteLine("Status: " + (SendOperatorTopUpPhoneAccountRequestStatusType)status.StatusID);
				this.WriteLine("Description: " + status.StatusDescription);
			}
			catch (Exception ex)
			{
				this.WriteLine("Exception: " + ex.ToString());
			}
		}

		private void sendCommsTestButton_Click(object sender, EventArgs e)
		{
			try
			{
				SendOperatorTopUpPhoneAccountRequestStatus status = gateway.SendCommsTest(null, null);
				this.WriteLine("Status: " + (SendOperatorTopUpPhoneAccountRequestStatusType)status.StatusID);
				this.WriteLine("Description: " + status.StatusDescription);
			}
			catch (Exception ex)
			{
				this.WriteLine("Exception: " + ex.ToString());
			}
		}

		private void SocketSendReceiveButton_Click(object sender, EventArgs e)
		{
			try
			{
				byte[] messageToSend = Encoding.ASCII.GetBytes(this.textBox1.Text);
				this.WriteLine("Sending message: " + this.textBox1.Text);
				byte[] response = gateway.SocketSendReceive(messageToSend);
				this.WriteLine("Response received: " + Encoding.ASCII.GetString(response));
			}
			catch (Exception ex)
			{
				this.WriteLine("Exception: " + ex.ToString());
			}
		}

		public void WriteLine(String line)
		{
			if (line.StartsWith(IP_ADDRESS))
			{
				this.Text = line;
			}
			else if (line.StartsWith(TOTAL_TIME))
			{
				this.tConsole.Text = this.tConsole.Text + "\n" + line;
			}
			else
			{
				this.tConsole.Text = this.tConsole.Text + "\n" + line;
			}

			StringBuilder sb = new StringBuilder(string.Empty);
			for (int i = 0; i < tConsole.Lines.Length; i++)
			{

				if (!tConsole.Lines[i].StartsWith(LINE_NUMBER))
				{
					sb.Append(LINE_NUMBER + i.ToString() + ": " + tConsole.Lines[i] + "\n");
				}
				else
				{
					sb.Append(tConsole.Lines[i] + "\n");
				}
			}
			this.tConsole.Text = sb.ToString();
		}
	}
}
