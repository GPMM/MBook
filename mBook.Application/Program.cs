using System;
using System.Windows.Forms;
using EyeXFramework.Forms;
using MBook.Domain.Entities;
using MBook.Infrastructure.Context;
using MBook.Infrastructure.Repositories;

namespace MBook
{
    static class Program
    {
        private static FormsEyeXHost _eyeXHost = new FormsEyeXHost();

        #region "Repositories"
        public static XmlRepository<Book> bookRepository;
        public static XmlRepository<Effect> effectRepository;
        #endregion


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
            // define os repositorios
            bookRepository = new XmlRepository<Book>(XMLContext.Instance);
            effectRepository = new XmlRepository<Effect>(XMLContext.Instance);
            // inicia o Eye Tracker
            _eyeXHost.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string erro;
            
            // Carrega as configurações
            if (!XMLContext.Instance.LoadData(out erro))
            {
                MessageBox.Show(erro, "Houve um erro ao tentar carregar o XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.Run(new FMain());
           
            _eyeXHost.Dispose();
        }
    }
}
