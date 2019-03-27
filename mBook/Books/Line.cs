using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using mBook.Effects;

namespace mBook.Books
{
    public class CLine
    {
        #region Attributes

        protected int m_iLineId;
        protected string m_sText;
        protected string m_sEffect;
        protected Hashtable m_htEffects;

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

        public string Effect
        {
            get { return m_sEffect; }
        }

        #endregion // Properties

        #region Constructor

        public CLine(int iLineId, XmlNode oLineNode)
        {
            m_iLineId = iLineId;
            m_sEffect = oLineNode.Attributes["Effects"]!=null ? oLineNode.Attributes["Effects"].Value : "";
            m_htEffects = new Hashtable();
            m_sText = LoadEffectData(oLineNode.InnerText);

        }

        #endregion // Constructor

        #region Private Methods

        private string LoadEffectData(string sLineNodeText)
        {
            if (sLineNodeText == "")
                return "";

            CEffect oEffect = new CEffect(sLineNodeText);
            if (oEffect.PositionOfEffect.Count != 0)
                m_htEffects.Add(m_iLineId, oEffect);

            return oEffect.LineWithoutEffect;
        }

        #endregion
    }
}
