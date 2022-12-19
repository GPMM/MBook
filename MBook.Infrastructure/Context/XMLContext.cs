using MBook.Domain;
using MBook.Domain.Entities;
using MBook.Domain.ValueObjects;
using System;
using System.Collections;
using System.Xml;

namespace MBook.Infrastructure.Context
{
    /// <summary>
    /// Classe responsavel por carregar e manter o XML no contexto da aplicacao 
    /// </summary>
    public class XMLContext
    {
        #region "Attributes"

        // Singleton
        private static XMLContext m_oInstance = null;

        // Lista de livros
        private Hashtable m_htBooks;

        // Lista de efeitos
        private Hashtable m_htEffects;
                
        #endregion // Attributes

        #region "Properties"

        public static XMLContext Instance
        {
            get
            {
                if (m_oInstance == null)
                {
                    m_oInstance = new XMLContext();
                }
                return m_oInstance;
            }
        }

        public Hashtable Books
        {
            get { return m_htBooks; }
        }

        public Hashtable Effects
        {
            get { return m_htEffects; }
        }
        
        #endregion // Properties

        #region "Constructor"

        private XMLContext()
        {
            // Dados lidos do arquivo de configuração
            m_htBooks = new Hashtable();

            // Dados lidos do arquivo de efeitos
            m_htEffects = new Hashtable();

        }

        #endregion // Constructor

        #region "Static Config"

        private bool LoadLibrary(string sLibraryFileName, out string erro)
        {
            erro = null;
            // Interpreta o XML
            try
            {
                // Carrega o XML com as configurações do programa.
                XmlNode oMainNode = Util.LoadXmlFile(sLibraryFileName, GenDef.AppName + "-lib", out erro);
                if (oMainNode == null)
                {
                    return false;
                }
                // Carrega os Books da Biblioteca
                if(!LoadBookData(oMainNode, out erro))
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                erro = $"Erro ao ler arquivo da biblioteca de livros:\n {e.Message}";
                return false;
            }            
        }

        private bool LoadEffects(string sConfigFileName, out string erro)
        {
            erro = null;            

            // Interpreta o XML
            try
            {
                // Carrega o XML com as configurações do programa.
                XmlNode oMainNode = Util.LoadXmlFile(sConfigFileName, GenDef.AppName + "Config", out erro);
                if (oMainNode == null)
                {
                    return false;
                }
                // Carrega os Efeitos
                if(!LoadAnchorData(oMainNode["anchors"], out erro))
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                erro = $"Erro ao ler arquivo de efeitos:\n{e.Message}";
                return false;
            }            
        }


        #endregion

        #region Public Methods

        public bool LoadData(out string erro)
        {
            // Lê o arquivo da biblioteca de livros
            string sLibraryFileName = GenDef.ConfigFileName;
            if (!LoadLibrary(sLibraryFileName, out erro))
            {
                return false;
            }
            return true;
        }        

        #endregion

        #region "Private Methods"

        private bool LoadBook(out string erro)
        {
            // Interpreta o XML
            try
            {
                // Carrega o XML com as configurações do programa.
                XmlNode oMainNode = Util.LoadXmlFile(GenDef.BooksDir, GenDef.AppName, out erro);
                if (oMainNode == null)
                {
                    return false;
                }
                // Carrega os Books da Biblioteca
                if (!LoadBookData(oMainNode, out erro))
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                erro = $"Erro ao ler arquivo da biblioteca de livros:\n{e.Message}";
                return false;
            }
        }

        private bool LoadBookData(XmlNode oMainNode, out string erro)
        {
            try
            {
                int iBookId = 1;
                erro = null;
                if (oMainNode == null)
                {
                    erro = $"Não existem livros cadastrados.";
                    return false;
                }
                foreach (XmlNode oNode in oMainNode)
                {
                    if (oNode.Name == "book")
                    {
                        Book oBook = new Book(iBookId, oNode);
                        m_htBooks.Add(iBookId, oBook);
                        iBookId++;
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                erro = $"Erro ao carregar livros:\n{e.Message}";
                return false;
            }            
        }

        private bool LoadAnchorData(XmlNode oAnchorsNode, out string erro)
        {
            erro=null;           

            try
            {
                if (oAnchorsNode == null)
                {
                    erro = "Não existem efeitos cadastrados.";
                    return false;
                }
                int iEffectId = 1;
                foreach (XmlNode oNode in oAnchorsNode)
                {
                    if (oNode.Name == "effect")
                    {
                        Effect oEffect = new Effect(iEffectId, oNode);
                        m_htEffects.Add(iEffectId, oEffect);
                        iEffectId++;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                erro = $"Erro ao carregar efeitos.\n{ex.Message}";
                return false;
            }
        }
        #endregion
    }
}
