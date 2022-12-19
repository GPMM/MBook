using MBook.Domain.ValueObjects;
using System;
using System.Collections;
using System.Xml;

namespace MBook.Domain.Entities
{
    public class Chapter
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

        public Chapter(int iChapterId, XmlNode oChapterNode, out string erro)
        {
            erro = null;
            m_iChapterId = iChapterId;
            m_sId = oChapterNode.Attributes["id"] != null ? oChapterNode.Attributes["id"].Value : "";
            m_sNome = oChapterNode.Attributes["name"] != null ? oChapterNode.Attributes["name"].Value : "";
            m_sConteudo = oChapterNode.Attributes["src"] != null ? oChapterNode.Attributes["src"].Value : "";
            m_htPage = new Hashtable();
            LoadSourceBook(m_sConteudo, out erro);
        }

        #endregion // Constructors    

        #region Public Methods

        public bool ConstainsPages(int iPageId)
        {
            return m_htPage.ContainsKey(iPageId);
        }

        public Page GetPage(int iPageId)
        {
            return m_htPage[iPageId] as Page;
        }

        #endregion // Public Methods 

        #region Private Methods

        private bool LoadSourceBook(string sConteudo, out string erro)
        {
            erro = null;
            // Carrega o XML com as configurações do programa.
            XmlNode oMainNode = Util.LoadXmlFile(GenDef.BooksDir + sConteudo, GenDef.AppName, out erro);
            if (oMainNode == null)
            {
                
                return false;
            }

            // Interpreta o XML
            try
            {
                // Carrega os Books da Biblioteca
                XmlNode oStructureNode = oMainNode["structure"];
                LoadStructure(oStructureNode, out erro);
            }
            catch (Exception e)
            {
                erro = "Erro ao ler arquivo do livros:\n" + e.Message;
                return false;
            }

            return true;
        }
        private bool LoadStructure(XmlNode oBookNode, out string erro)
        {
            erro=null;
            if (oBookNode == null)
                return false;

            try
            {
                int iPageId = 1;

                foreach (XmlNode oNode in oBookNode)
                {
                    if (oNode.Name == "page")
                    {
                        Page oPage = new Page(iPageId, oNode, out erro);
                        m_htPage.Add(iPageId, oPage);
                        iPageId++;
                    }
                }
            }
            catch(Exception ex)
            {
                erro = $"Erro ao carregar páginas.\nMais detalhes: {ex.Message}";
                return false;
            }

            return true;
        }

        #endregion
    }
}

