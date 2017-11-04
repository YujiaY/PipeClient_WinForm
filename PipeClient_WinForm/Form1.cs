using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Pipes;
using System.IO;


namespace PipeClient_WinForm
{
    public partial class Form1 : Form
    {
        NamedPipeClientStream pipeClient;

        public Form1()
        {
            InitializeComponent();
            lbInfo.Text = "\nClick Connect to set up connection";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Connect to the pipe or wait until the pipe is available.

            pipeClient = new NamedPipeClientStream(".", "testpipe", PipeDirection.In);

            pipeClient.Connect();

            lbInfo.Text += "\nConnected to pipe.";
            lbInfo.Text += $"There are currently" + pipeClient.NumberOfServerInstances.ToString()
                + "pipe server instances open.";
            lbInfo.Text += "\nSwitch over to the server program to input text there.";


            // Now read the text stream that comes in via the named pipe
            using (StreamReader sr = new StreamReader(pipeClient))
            {
                // Display the text to the console
                string temp;
                if ((temp = sr.ReadLine()) != null)
                {
                    tbReceive.Text = "Received from server:";
                    tbReceive.Text += temp;
                }
            }


        }

        private void btnDisc_Click(object sender, EventArgs e)
        {
            if (pipeClient != null)
            {
                pipeClient.Close();
            }

            lbInfo.Text = "Connection Closed";
        }


    }
}
