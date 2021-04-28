using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mBook.Actions
{
    public class CAction
    {

        #region Attributes
        // Identificador do Efeito
        protected int m_iActionId;

        // Ação atribuída ao Efeito
        protected string m_sAction;

        protected Hashtable m_htActions;
        protected string m_sLine;

        #endregion // Attributes

        #region Properties

        public int Id
        {
            get { return m_iActionId; }
            set { m_iActionId = value; }
        }

        public string Action
        {
            get { return m_sAction; }
            set { m_sAction = value; }
        }

        #endregion // Properties

        #region Constructor

        public CAction(int iActionId, XmlNode oActionNode)
        {
            m_iActionId = iActionId;
            m_sAction = oActionNode.Attributes["Value"] != null ? oActionNode.Attributes["Value"].Value : "";
            m_htActions = new Hashtable();
        }

        #endregion // Constructor

        #region Public Methods

        public void RemoveAction(string sAction)
        {
            string[] sLineSplited = sAction.Split(' ');
            for(int i=0; i<sLineSplited.Length; i++)
            {
                if (!sLineSplited[i].Contains('|'))
                    m_sLine = m_sLine + ' ' + sLineSplited[i];
                else
                    m_htActions.Add(m_sLine.Split(' ').Length + 1, sLineSplited[i].Split('|'));
            }
        }
        #endregion
    }
}
