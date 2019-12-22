using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Generator.PlugIn;

namespace Generator.Gui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var loader = new Loader();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            loader.Scan();

            Application.Run(new Form1(loader.PlugIns));
        }
    }
}
