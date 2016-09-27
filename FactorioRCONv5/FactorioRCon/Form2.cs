using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FactorioRcon
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerAsync();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                      
            //progressBar1.Increment(1);
            if (progressBar1.Value == 15)
            {
                label1.Text = ("Loading Moar Hype!");
                //timer1.Stop();
                //Form1 newForm = new Form1();
                //newForm.Show();
                //Close();
                //this.Hide();
            }
                else if (progressBar1.Value == 45)
                {
                    label1.Text = ("Loading Bouncing Betties!");
                }
                else if (progressBar1.Value == 65)
                {
                    label1.Text = ("Loading Dick Gifs!");
                }
                else if (progressBar1.Value == 100)
                {
                    timer1.Stop();
                    Close();
                }
                
            }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            progressBar1.Invoke(
    (MethodInvoker)
    delegate
    {
        while (true) {
            Thread.Sleep(10);
            progressBar1.Increment(1);
        }
        
    }
   );
            
        }
    }
    }
