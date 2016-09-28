using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using SourceRconLib;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace FactorioRcon
{
    public partial class Form1 : Form
    {
        Rcon sr;

        public Form1()
        {
            InitializeComponent();
            IPBox.Text = Properties.Settings.Default.IP;
            PortBox.Text = Properties.Settings.Default.Port;
            PasswordBox.Text = Properties.Settings.Default.Password;
            WhitelistText.Text = Properties.Settings.Default.Whitelist;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        void sr_Errors(MessageCode code, string data)
        {

            MethodInvoker m = () => 
            {
                OutputBox.SelectionColor = Color.Red;
                OutputBox.SelectedText = "} " + code.ToString() + "\n" + (data == null ? "" : "} " + data + "\n");
            };
            this.Invoke(m);
        }

        void sr_ServerOutput(MessageCode code, string data)
        {

            if (data.Contains("Players "))
            {
                int count1 = 0;
                int count2 = 1;
                //PlayersTextBox.Text = "";

                //this.PlayersTextBox.Text = "";
                //MessageBox.Show(data);
                string[] players = data.Split(' ');
                foreach (string i in players)
                {
                    
                        //MessageBox.Show(i);
                        //System.Console.Write("{0} ", i);
                    string testerv3 = (i);
                        MethodInvoker m = () => { if (data != null) PlayersTextBox.AppendText(testerv3); };
                        this.Invoke(m);
                    
                }

                if (WhitelistBox.Checked)
                {
                    //MessageBox.Show("Whitelist Enabled");
                    //whitelistWorker.RunWorkerAsync();
                    WhitelistText.Invoke(
                    (MethodInvoker)
                    delegate
                    {
                        foreach (string i in players)
                        {
                          //  count1 = count1 + 1;
                          //  count2 = count2 + 1;

                         //   string testing = players[count1] + players[count2];
                           // if (testing.Contains(i + "(online)"))
                            //{
                              //  MessageBox.Show("Yes");
                            //}

                            if (i.Contains("Players "))
                            {
                                //MessageBox.Show(i + " Is Safe ");
                            }
                             else if (i.Contains("(online)"))
                            //else if (WhitelistText.Text.Contains(i + "(online)"))
                            {
                                //MessageBox.Show(i + " Is Safe ");
                            }
                            else if (WhitelistText.Text.Contains(i))
                             //else if (WhitelistText.Contains(i))
                            {
                           // MessageBox.Show(i + " Is Safe");
                            }
                            else if (i.Contains("(online)"))
                            {
                            //MessageBox.Show(i + " Is Safe");
                            }
                            else if (i.Contains(i + "(online)"))
                            {
                                MessageBox.Show("Senpai tell me how you got here");
                            }
                            else
                            {
                                sr.ServerCommand("/kick " + i + " You are not whitelisted on this server!");
                            }


                        }
                    }
                   );

                    //

                }
            }
            else if (data.Contains("Player "))
            {
                //MessageBox.Show("I interuppted your console write");
            }
            else {
                MethodInvoker m = () => { if (data != null) OutputBox.AppendText(data); };
                this.Invoke(m);
            }
        }

        void sr_ConnectionSuccess(bool info)
        {
            if (info)
            {
                MethodInvoker m = () =>
                    {
                        SendInputButton.Enabled = true;
                        OutputBox.Enabled = true;
                        InputBox.Enabled = true;

                        ConnectButton.Text = "Disconnect";
                        ConnectButton.Click += Disconnect_Click;
                        ConnectButton.Click -= button2_Click;
                    };
                this.Invoke(m);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(IPBox.Text);
            int port = int.Parse(PortBox.Text);

            IPEndPoint ipe = new IPEndPoint(ip, port);

            sr = new Rcon();
            sr.ConnectionSuccess += new BoolInfo(sr_ConnectionSuccess);
            sr.ServerOutput += new RconOutput(sr_ServerOutput);
            sr.Errors += new RconOutput(sr_Errors);
            
            sr.Connect(ipe, PasswordBox.Text);
            while (!sr.Connected)
            {
                Thread.Sleep(10);
            }
            sr.ServerCommand(Properties.Settings.Default.Players);
            sr.ServerCommand(Properties.Settings.Default.Players);
            playerBackGround.RunWorkerAsync();
            //PlayerJob();
            // Thread playerThread = new Thread(PlayerJob);
            //  playerThread.Start();
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("You have been disconnected, but as we do not support reconnect please reopen the program.");
            sr.Disconnect();
            
            playerBackGround.CancelAsync();
            
            ConnectButton.Text = "Connect";
            ConnectButton.Click += button2_Click;
            ConnectButton.Click -= Disconnect_Click;
        }

        private void SendInputButton_Click(object sender, EventArgs e)
        {
            sr.ServerCommand(InputBox.Text);
            OutputBox.SelectionColor = Color.Blue;
            OutputBox.SelectedText = "] " + InputBox.Text + "\n";
            InputBox.Clear();
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                SendInputButton.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sr.Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            sr = new Rcon();
            sr.ConnectionSuccess += new BoolInfo(sr_ConnectionSuccess);
            sr.ServerOutput += new RconOutput(sr_ServerOutput);
            sr.Errors += new RconOutput(sr_Errors);
        }

        private void PlayersButton_Click(object sender, EventArgs e)
        {
            
            sr.ServerCommand(Properties.Settings.Default.Players);

        }

        private void EvolutionButton_Click(object sender, EventArgs e)
        {
            sr.ServerCommand(Properties.Settings.Default.Evolution);

        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            sr.ServerCommand(Properties.Settings.Default.Help);
        }
        private void AdminCheck_Click(object sender, EventArgs e)
        {
            sr.ServerCommand(Properties.Settings.Default.Admins);
        }
        private void CheckBans_Click(object sender, EventArgs e)
        {
            sr.ServerCommand(Properties.Settings.Default.Bans);
            
        }

        private void SaveMapButton_Click(object sender, EventArgs e)
        {
            sr.ServerCommand(Properties.Settings.Default.SaveMap);
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IP = IPBox.Text;
            Properties.Settings.Default.Port = PortBox.Text;
            Properties.Settings.Default.Password = PasswordBox.Text;
            Properties.Settings.Default.Whitelist = WhitelistText.Text;
            Properties.Settings.Default.Save();
           
        }
        
        private void playerBackGround_DoWork(object sender, DoWorkEventArgs e)
        {
            while(!playerBackGround.CancellationPending)
            {
            	for (int i = 1; i <= 100; i++) {
            		Thread.Sleep(i*100);
            		if (playerBackGround.CancellationPending) return;
            	}         	               
            	
                if(sr.Connected)
                {
	                sr.ServerCommand(Properties.Settings.Default.Players);
	                //Reset ChatBox
	                PlayersTextBox.Invoke(
	                    (MethodInvoker)
	                    delegate
	                   {
	                       int count = Regex.Matches(PlayersTextBox.Text, "(online)").Count;
	                       string pphole = "Players (" + count + ")";
	                       tabPage3.Text = pphole;
	                       PlayersTextBox.Text = "";
	                   }
	                   );
                }
                    
            }
            
            
        }

        private void WhitelistBox_CheckedChanged(object sender, EventArgs e)
        {
            if (WhitelistBox.Checked)
            {
                MessageBox.Show("Whitelist Enabled");
            }
            else
            {
                MessageBox.Show("Whitelist Disabled");
            }
        }


    }
}
