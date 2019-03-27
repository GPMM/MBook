using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mBook.Effects
{
    public class CEffect
    {

        #region Attributes

        protected Hashtable m_htEffect;
        protected string m_sLine;

        #endregion // Attributes

        #region Properties

        public Hashtable PositionOfEffect
        {
            get { return m_htEffect; }
        }

        public string LineWithoutEffect
        {
            get { return m_sLine; }
        }

        #endregion // Properties

        #region Constructor

        public CEffect(string sLineWithEffect)
        {
            m_htEffect = new Hashtable();
            m_sLine = "";
            RemoveEffect(sLineWithEffect);
        }

        #endregion // Constructor

        #region Public Methods

        public void RemoveEffect(string sEffect)
        {
            string[] sLineSplited = sEffect.Split(' ');
            for(int i=0; i<sLineSplited.Length; i++)
            {
                if (!sLineSplited[i].Contains('|'))
                    m_sLine = m_sLine + ' ' + sLineSplited[i];
                else
                    m_htEffect.Add(m_sLine.Split(' ').Length + 1, sLineSplited[i].Split('|'));
            }
        }
        #endregion
    }
}
