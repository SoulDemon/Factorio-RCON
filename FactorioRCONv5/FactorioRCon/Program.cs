using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FactorioRcon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MessageBox.Show("Please report all encountered bugs to JetFox in IRC or Discord!", "Notice!");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form2());
            Application.Run(new Form1());
        }
    }
}
