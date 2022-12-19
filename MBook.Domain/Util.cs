using System;
using System.IO;
using System.Xml;

namespace MBook.Domain
{
    /// <summary>
    /// classe de utilidades
    /// </summary>
    public static class Util
    {
        #region Load XML File

        /// <summary>
        /// Carrega um arquivo XML
        /// </summary>
        /// <param name="sFileName">Nome do arquivo</param>
        /// <param name="sMainNode">Nome do nó principal do XML para verificação</param>
        /// <param name="sErrorMsg">Mensagem de erro</param>
        /// <returns>Nó principal do XML</returns>
        public static XmlNode LoadXmlFile(string sFileName, string sMainNode, out string sErrorMsg)
        {
            sErrorMsg = null;
            XmlDocument xdoc = new XmlDocument();

            if (!File.Exists(sFileName))
            {
                sErrorMsg = "Arquivo " + sFileName + " não encontrado.";
                return null;
            }

            try
            {
                // Abre arquivo no modo somente leitura para evitar problemas quando rodando
                // em contas sem privilégios
                FileInfo oFileInfo = new FileInfo(sFileName);
                FileStream oFile = oFileInfo.Open(FileMode.Open, FileAccess.Read);

                int iLength = Convert.ToInt32(oFile.Length);
                byte[] pBuffer = new byte[iLength];

                oFile.Read(pBuffer, 0, iLength);
                MemoryStream pStream = new MemoryStream(pBuffer, 0, pBuffer.Length);
                oFile.Close();
                xdoc.Load(pStream);
            }
            catch (Exception e)
            {
                sErrorMsg = "Erro ao carregar arquivo " + sFileName + "\n\n" + e.Message;
                return null;
            }

            // test XML root node
            if (xdoc.DocumentElement.Name != sMainNode)
            {
                sErrorMsg = "Arquivo " + sFileName + " inválido.";
                return null;
            }

            sErrorMsg = string.Empty;
            return xdoc.DocumentElement;
        }

        #endregion         

        /// <summary>
        /// retorna o path de um diretório dentro da aplicação, se null retorna o diretório base
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDirectoryPath(string name=null)
        {
            string path = null;
            string basePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
            if (string.IsNullOrEmpty(name))
            {
                path = basePath;
            }
            else
            {
                path = basePath + "\\" + name;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            return path;
        }
    }
}
