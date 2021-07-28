using network;
using services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IServices service = new ServerProxy("127.0.0.1", 1337);
            ClientCtrl ctrl = new ClientCtrl(service);
            Application.Run(new Form1(ctrl));
        }
    }
}
