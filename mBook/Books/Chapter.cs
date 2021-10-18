using System;
using System.Collections;
using System.Windows.Forms;
using System.Xml;

namespace mBook.Books
{
    public class CChapter
    {
        #region Attributes

        protected int m_iChapterId;
        protected string m_sId;
        protected string m_sNome;
        protected string m_sConteudo;
        protected Hashtable m_htPage;

        #endregion // Attributes

        #region Properties

        public int ChapterNumberId
        {
            get { return m_iChapterId; }
        }

        public string ChapterNameId
        {
            get { return m_sId; }
        }

        public string ChapterName
        {
            get { return m_sNome; }
        }

        public Hashtable Pages
        {
            get { return m_htPage; }
        }


        #endregion // Properties

        #region Constructors

        public CChapter(int iChapterId, XmlNode oChapterNode)
        {
            m_iChapterId = iChapterId;
            m_sId = oChapterNode.Attributes["id"] != null ? oChapterNode.Attributes["id"].Value : "";
            m_sNome = oChapterNode.Attributes["name"] != null ? oChapterNode.Attributes["name"].Value : "";
            m_sConteudo = oChapterNode.Attributes["src"] != null ? oChapterNode.Attributes["src"].Value : "";
            m_htPage = new Hashtable();
            LoadSourceBook(m_sConteudo);
        }

        #endregion // Constructors    

        #region Public Methods

        public bool ConstainsPages(int iPageId)
        {
            return m_htPage.ContainsKey(iPageId);
        }

        public CPage GetPage(int iPageId)
        {
            return m_htPage[iPageId] as CPage;
        }

        #endregion // Public Methods 

        #region Private Methods

        private bool LoadSourceBook(string sConteudo)
        {
            // Carrega o XML com as configurações do programa.
            string sErrorMsg;
            XmlNode oMainNode = CUtil.LoadXmlFile(CGenDef.BooksDir + sConteudo, CGenDef.AppName, out sErrorMsg);
            if (oMainNode == null)
            {
                MessageBox.Show(sErrorMsg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Interpreta o XML
            try
            {
                // Carrega os Books da Biblioteca
                XmlNode oStructureNode = oMainNode["structure"];
                LoadStructure(oStructureNode);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao ler arquivo do livros:\n" + e.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private bool LoadStructure(XmlNode oBookNode)
        {
            if (oBookNode == null)
                return false;

            try
            {
                int iPageId = 1;

                foreach (XmlNode oNode in oBookNode)
                {
                    if (oNode.Name == "page")
                    {
                        CPage oPage = new CPage(iPageId, oNode);
                        m_htPage.Add(iPageId, oPage);
                        iPageId++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao carregar páginas.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        #endregion
    }
}

