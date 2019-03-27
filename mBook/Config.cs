using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using mBook.Books;

namespace mBook
{
    class CConfig
    {
        #region Attributes

        // Singleton
        private static CConfig m_oInstance = null;

        // Lista de livros
        private Hashtable m_htBooks;

        // configuração da TreeView
        private XmlNode m_oTreeViewConfigNode;

        #endregion // Attributes

        #region Properties

        public static CConfig Instance
        {
            get
            {
                if (m_oInstance == null)
                    m_oInstance = new CConfig();
                return m_oInstance;
            }
        }

        public Hashtable Books
        {
            get { return m_htBooks; }
        }

        #endregion // Properties

        #region Constructor

        private CConfig()
        {
            // Dados lidos do arquivo de configuração
            m_htBooks = new Hashtable();

        }

        #endregion // Constructor

        #region Static Config

        private bool LoadStaticConfig(string sConfigFileName)
        {
            // Carrega o XML com as configurações do programa.
            string sErrorMsg;
            XmlNode oMainNode = CUtil.LoadXmlFile(sConfigFileName, CGenDef.AppName + "Config", out sErrorMsg);
            if (oMainNode == null)
            {
                MessageBox.Show(sErrorMsg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Interpreta o XML
            try
            {
                // Carrega os Books
                LoadBookData(oMainNode["books"]);
                //CEffects.Instance.LoadData(oMainNode["effects"]);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao ler arquivo de configuração:\n" + e.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        #endregion

        #region Public Methods

        public bool LoadData()
        {
            // =================== Lê o arquivo de configuração ===================
            string sConfigFileName = CGenDef.ConfigFileName;
            if (!LoadStaticConfig(sConfigFileName))
                return false;

            return true;
        }

        public CBook GetBook(int iBookId)
        {
            if(m_htBooks.ContainsKey(iBookId))
                return m_htBooks[iBookId] as CBook;

            return null;
        }

        public CBook GetBook(string sBookName)
        {
            foreach (CBook oBook in m_htBooks.Values)
            {
                if(oBook.Name==sBookName)
                    return oBook;
            }

            return null;
        }
        
        #endregion

        #region Private Methods

        private bool LoadBookData(XmlNode oBooksNode)
        {
            string sErrorMsg = "Não existem livros cadastrados.";
            if (oBooksNode == null)
            {
                MessageBox.Show(sErrorMsg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                int iBookId = 1;

                foreach (XmlNode oNode in oBooksNode)
                {
                    if (oNode.Name == "book")
                    {
                        CBook oBook = new CBook(iBookId, oNode);
                        m_htBooks.Add(iBookId, oBook);
                        iBookId++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao carregar livros.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        
        #endregion
    }
}
