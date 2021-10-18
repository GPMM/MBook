using System;
using System.Collections;
using System.Windows.Forms;
using System.Xml;

namespace mBook.Books
{
    public class CBook : IComparable
    {
        #region Attributes

        // Identificador do Livro
        protected int m_iBookId;

        // Identificador do Livro
        protected string m_sId;

        // Titulo atribuído ao Livro
        protected string m_sNome;

        // Editora do Livro
        protected string m_sEditora;

        // Edicao do Livro
        protected string m_sEdicao;

        // Idioma do Livro
        protected string m_sIdioma;

        // Ilustrador do Livro
        protected string m_sIlustrador;

        // ISBN do Livro
        protected string m_sISBN;

        // Autor do Livro
        protected string m_sAutor;

        // Description do Livro
        protected string m_sDescription;

        // Capa do Livro
        protected string m_sCapaUrl;

        // Conteudo do Livro
        protected string m_sConteudo;

        // Lista de capítulos do livro
        protected Hashtable m_htChapter;

        //Lista de paginas do livro
        protected Hashtable m_htPage;

        #endregion // Attributes

        #region Properties

        public int Id
        {
            get { return m_iBookId; }
            set { m_iBookId = value; }
        }

        public string NameId
        {
            get { return m_sId; }
            set { m_sId = value; }
        }

        public string Name
        {
            get { return m_sNome; }
            set { m_sNome = value; }
        }

        public string Editora
        {
            get { return m_sEditora; }
            set { m_sEditora = value; }
        }

        public string ISBN
        {
            get { return m_sISBN; }
            set { m_sISBN = value; }
        }

        public string Author
        {
            get { return m_sAutor; }
            set { m_sAutor = value; }
        }

        public string Description
        {
            get { return m_sDescription; }
            set { m_sDescription = value; }
        }

        public Hashtable Pages
        {
            get { return m_htPage; }
        }

        public Hashtable Chapter
        {
            get { return m_htChapter; }
        }

        #endregion // Properties

        #region Constructor

        public CBook(int iBookId, XmlNode oBookNode)
        {
            m_iBookId = iBookId;
            m_sId = oBookNode.Attributes["id"] != null ? oBookNode.Attributes["id"].Value : "";
            m_sNome = oBookNode.Attributes["title"]!=null ? oBookNode.Attributes["title"].Value : "";
            m_sAutor = oBookNode.Attributes["author"].Value != null ? oBookNode.Attributes["author"].Value : "";
            m_sIlustrador = oBookNode.Attributes["illustrator"].Value != null ? oBookNode.Attributes["illustrator"].Value : "";
            m_sEditora = oBookNode.Attributes["publisher"] !=null ? oBookNode.Attributes["publisher"].Value : "";
            m_sEdicao = oBookNode.Attributes["edition"] != null ? oBookNode.Attributes["edition"].Value : "";
            m_sISBN = oBookNode.Attributes["isbn"].Value != null ? oBookNode.Attributes["isbn"].Value : "";
            m_sIdioma = oBookNode.Attributes["language"] != null ? oBookNode.Attributes["language"].Value : "";
            m_sCapaUrl = oBookNode.Attributes["cover-url"] != null ? oBookNode.Attributes["cover-url"].Value : "";
            m_sConteudo = oBookNode.Attributes["src"] != null ? oBookNode.Attributes["src"].Value : null;
            m_sDescription = oBookNode.Attributes["description"].Value != null ? oBookNode.Attributes["description"].Value : "";
            
            m_htChapter = new Hashtable();
            m_htPage = new Hashtable();
            LoadChapterData(oBookNode);
            if(m_htChapter.Count==0)
                LoadSourceBook(m_sConteudo);

        }

        #endregion // Constructor

        #region Public Methods

        public bool ContainsChapter(int iChapterId)
        {
            return m_htChapter.ContainsKey(iChapterId);
        }

        public CChapter GetChapter(int iChapterId)
        {
            return m_htChapter[iChapterId] as CChapter;
        }

        public bool ConstainsPages(int iPageId)
        {
            return m_htPage.ContainsKey(iPageId);
        }

        public CPage GetPage(int iPageId)
        {
            return m_htPage[iPageId] as CPage;
        }

        // Permite que os livros sejam ordenados pelo seu nome
        public int CompareTo(object obj)
        {
            CBook oBook = (CBook)obj;
            return m_sNome.CompareTo(oBook.m_sNome);
        }

        #endregion // Public Methods

        #region Private Methods

        private bool LoadChapterData(XmlNode oBookNode)
        {
            if (oBookNode == null)
                return false;

            try
            {
                int iChapterId = 1;

                foreach (XmlNode oNode in oBookNode)
                {
                    if (oNode.Name == "chapter")
                    {
                        CChapter oChapter = new CChapter(iChapterId, oNode);
                        m_htChapter.Add(iChapterId, oChapter);
                        iChapterId++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao carregar capítulos.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

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