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
    public class CPage
    {
        #region Attributes

        protected int m_iPageId;
        protected string m_sEffects;
        protected Hashtable m_htLines;

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

        public Hashtable Lines
        {
            get { return m_htLines; }
        }

        #endregion // Properties

        #region Constructors

        public CPage(int iPageId, XmlNode oPageNode)
        {
            m_iPageId = iPageId;
            m_sEffects = oPageNode.Attributes["Effects"]!=null ? oPageNode.Attributes["Effects"].Value : "";
            m_htLines = new Hashtable();

            XmlNode oLineNode = oPageNode["lines"];
            LoadLineData(oLineNode);
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
