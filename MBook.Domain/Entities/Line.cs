using System.Collections;
using System.Xml;

namespace MBook.Domain.Entities
{
    public class CLine
    {
        #region Attributes

        protected int m_iLineId;
        protected string m_sText;
        protected string m_sAction;
        protected Hashtable m_htActions;
        protected Hashtable m_htAnchors;

        #endregion // Attributes

        #region Properties
        public int LineId
        {
            get { return m_iLineId; }
        }

        public string Text
        {
            get { return m_sText; }
        }

        public string Action
        {
            get { return m_sAction; }
        }

        public Hashtable Anchor
        {
            get { return m_htAnchors; }
        }

        #endregion // Properties

        #region Constructor

        public CLine(int iLineId, XmlNode oLineNode)
        {
            m_iLineId = iLineId;
            m_sAction = oLineNode.Attributes["Effects"]!=null ? oLineNode.Attributes["Effects"].Value : "";
            m_htActions = new Hashtable();
            m_sText = oLineNode.InnerText.ToString();

            m_htAnchors = new Hashtable();
            LoadAnchorData(oLineNode);
        }

        #endregion // Constructor

        #region Private Methods
        private bool LoadAnchorData(XmlNode oLineNode)
        {
            if (oLineNode == null)
                return false;

            try
            {
                foreach (XmlNode oNode in oLineNode)
                {
                    if (oNode.Name == "anchor")
                        m_htAnchors.Add(oNode.InnerText, oNode.Attributes["id"].Value);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
