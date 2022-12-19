using MBook.Domain.Base;
using System.Collections;
using System.Windows.Forms;
using System.Xml;


namespace MBook.Domain.Entities
{
    public class Effect:BaseEntity
    {

        #region Attributes

        // Identificador do Efeito
        protected int m_iEffectId;

        // Nome atribuído ao Efeito
        protected string m_sName;

        // Codigo do efeito
        protected string m_sCode;

        // Lista de ações do efeito
        protected Hashtable m_htActions;

        #endregion // Attributes

        #region Properties

        public override int Id
        {
            get { return m_iEffectId; }
            set { m_iEffectId = value; }
        }

        public override string Name
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        public string Code
        {
            get { return m_sCode; }
            set { m_sCode = value; }
        }

        public Hashtable Actions
        {
            get { return m_htActions; }
        }

        #endregion // Properties

        #region Constructor

        public Effect(int iEffectId, XmlNode oEffectNode)
        {
            m_iEffectId = iEffectId;
            m_sName = oEffectNode.Attributes["Name"] != null ? oEffectNode.Attributes["Name"].Value : "";
            m_sCode = oEffectNode.Attributes["Code"] != null ? oEffectNode.Attributes["Code"].Value : "";
            m_htActions = new Hashtable();

            XmlNode oActionNode = oEffectNode["actions"];
            LoadActionData(oActionNode);
        }

        #endregion // Constructor

        #region Public Methods

        public bool ConstainsActions(int iActionId)
        {
            return m_htActions.ContainsKey(iActionId);
        }

        public Action GetAction(int iActionId)
        {
            return m_htActions[iActionId] as Action;
        }

        // Permite que os efeitos sejam ordenados pelo seu nome
        public int CompareTo(object obj)
        {
            Effect oEffect = (Effect)obj;
            return m_sName.CompareTo(oEffect.m_sName);
        }

        #endregion // Public Methods

        #region Private Methods

        private bool LoadActionData(XmlNode oActionNode)
        {
            string sErrorMsg = "Não existem efeitos cadastrados.";
            if (oActionNode == null)
            {
                MessageBox.Show(sErrorMsg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                int iActionId = 1;

                foreach (XmlNode oNode in oActionNode)
                {
                    if (oNode.Name == "action")
                    {
                        Action oAction = new Action(iActionId, oNode);
                        m_htActions.Add(iActionId, oAction);
                        iActionId++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao carregar efeitos.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        #endregion
    }
}
