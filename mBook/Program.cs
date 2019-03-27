using System;
using System.Windows.Forms;
using EyeXFramework.Forms;

namespace mBook
{
    static class Program
    {
        private static FormsEyeXHost _eyeXHost = new FormsEyeXHost();

        /// <summary>
        /// Gets the singleton EyeX host instance.
        /// </summary>
        public static FormsEyeXHost EyeXHost
        {
            get { return _eyeXHost; }
        }

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            _eyeXHost.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Carrega as configurações
            if (!CConfig.Instance.LoadData())
                return;

            Application.Run(new FMain());

            _eyeXHost.Dispose();
        }
    }
}
