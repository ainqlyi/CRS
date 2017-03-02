using System;
using System.Windows.Forms;
using log4net;

namespace RestfulGeoCoder
{
    static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GeoCoderForm());
        }
    }
}