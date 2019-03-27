using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace mBook.Books
{
    public class CBook : IComparable
    {
        #region Attributes

        // Identificador do Livro
        protected int m_iBookId;

        // Nome atribuído ao Livro
        protected string m_sName;

        // Editora do Livro
        protected string m_sEditora;

        // ISBN do Livro
        protected string m_sISBN;

        // Autor do Livro
        protected string m_sAutor;

        // Description do Livro
        protected string m_sDescription;

        // Lista de paginas do livro
        protected Hashtable m_htPages;

        #endregion // Attributes

        #region Properties

        public int Id
        {
            get { return m_iBookId; }
            set { m_iBookId = value; }
        }

        public string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
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
            get { return m_htPages; }
        }

        #endregion // Properties

        #region Constructor

        public CBook(int iBookId, XmlNode oBookNode)
        {
            m_iBookId = iBookId;
            m_sName = oBookNode.Attributes["Name"]!=null ? oBookNode.Attributes["Name"].Value : "";
            m_sEditora = oBookNode.Attributes["Editora"]!=null ? oBookNode.Attributes["Editora"].Value : "";
            m_sISBN = oBookNode.Attributes["ISBN"].Value!=null ? oBookNode.Attributes["ISBN"].Value : "";
            m_sAutor = oBookNode.Attributes["Autor"].Value != null ? oBookNode.Attributes["Autor"].Value : "";
            m_sDescription = oBookNode.Attributes["Descricao"].Value != null ? oBookNode.Attributes["Descricao"].Value : "";
            m_htPages = new Hashtable();

            XmlNode oPageNode = oBookNode["pages"];
            LoadPageData(oPageNode);
        }

        #endregion // Constructor

        #region Public Methods
       
        public bool ConstainsPages(int iPageId)
        {
            return m_htPages.ContainsKey(iPageId);
        }

        public CPage GetPage(int iPageId)
        {
            return m_htPages[iPageId] as CPage;
        }

        // Permite que os livros sejam ordenados pelo seu nome
        public int CompareTo(object obj)
        {
            CBook oBook = (CBook)obj;
            return m_sName.CompareTo(oBook.m_sName);
        }

        #endregion // Public Methods

        #region Private Methods

        private bool LoadPageData(XmlNode oPagesNode)
        {
            string sErrorMsg = "Não existem páginas cadastradas.";
            if (oPagesNode == null)
            {
                MessageBox.Show(sErrorMsg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                int iPageId = 1;

                foreach (XmlNode oNode in oPagesNode)
                {
                    if (oNode.Name == "page")
                    {
                        CPage oPage = new CPage(iPageId, oNode);
                        m_htPages.Add(iPageId, oPage);
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