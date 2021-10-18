using System.Collections;
using System.Windows.Forms;
using System.Xml;

namespace mBook.Books
{
    public class CPage
    {
        #region Attributes

        protected int m_iPageId;
        protected string m_sId;
        protected string m_sEffects;
        protected string m_sBackground;
        protected Hashtable m_htLines;
        protected Hashtable m_htAnchors;

        #endregion // Attributes

        #region Properties

        public int PageId
        {
            get { return m_iPageId; }
        }

        public string Effects
        {
            get { return m_sEffects; }
            set { m_sEffects = value; }
        }

        public string Background
        {
            get { return m_sBackground; }
            set { m_sBackground = value; }
        }

        public Hashtable Lines
        {
            get { return m_htLines; }
        }

        public Hashtable Anchor
        {
            get { return m_htAnchors; }
        }

        #endregion // Properties

        #region Constructors

        public CPage(int iPageId, XmlNode oPageNode)
        {
            m_iPageId = iPageId;
            m_sId = oPageNode.Attributes["id"] != null ? oPageNode.Attributes["id"].Value : "";
            m_sEffects = oPageNode.Attributes["Effect"]!=null ? oPageNode.Attributes["Effect"].Value : "";
            m_sBackground = oPageNode.Attributes["background-url"] != null ? oPageNode.Attributes["background-url"].Value : "";
            m_htLines = new Hashtable();
            m_htAnchors = new Hashtable();

            //XmlNode oLineNode = oPageNode["lines"];
            LoadLineData(oPageNode);
        }

        #endregion // Constructors    

        #region Public Methods

        public bool ContainsLine(int iLineId)
        {
            return m_htLines.ContainsKey(iLineId);
        }

        public CLine GetLine(int iLineId)
        {
            return m_htLines[iLineId] as CLine;
        }

        #endregion // Public Methods 

        #region Private Methods

        private bool LoadLineData(XmlNode oLinesNode)
        {
            string sErrorMsg = "Não existem linhas cadastradas.";
            if (oLinesNode == null)
            {
                MessageBox.Show(sErrorMsg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                int iLineId = 1;

                foreach (XmlNode oNode in oLinesNode)
                {
                    if (oNode.Name == "line")
                    {
                        CLine oLine = new CLine(iLineId, oNode);
                        m_htLines.Add(iLineId, oLine);
                        iLineId++;

                        foreach (DictionaryEntry entry in oLine.Anchor)
                        {
                            if (!m_htAnchors.ContainsKey(entry.Key))
                            {
                                m_htAnchors.Add(entry.Key, entry.Value);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao carregar linhas.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        #endregion
    }
}
