using MBook.Domain.Base;
using MBook.Domain.Entities;
using MBook.Domain.Interfaces;
using MBook.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBook.Infrastructure.Repositories
{
    /// <summary>
    /// repository para xml
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class XmlRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly XMLContext xMLContext;

        public XmlRepository(XMLContext context)
        {
            xMLContext = context;
        }

        public Book GetBook(int iBookId)
        {
            if (XMLContext.Instance.Books.ContainsKey(iBookId))
            {
                return XMLContext.Instance.Books[iBookId] as Book;
            }
            return null;
        }

        public Book GetBook(string sBookName)
        {
            foreach (Book oBook in XMLContext.Instance.Books.Values)
            {
                if (oBook.Name == sBookName)
                {
                    return oBook;
                }
            }

            return null;
        }

        public Effect GetEffect(int iEffectId)
        {
            if (xMLContext.Effects.ContainsKey(iEffectId))
            {
                return xMLContext.Effects[iEffectId] as Effect;
            }
            return null;
        }

        public string GetEffect(string sEffect)
        {
            string sEffectCode = sEffect.Substring(0, 1);
            string sEffectStream = sEffect.Substring(1, sEffect.Length - 1);

            foreach (Effect oEffect in xMLContext.Effects.Values)
            {
                if (oEffect.Code == sEffectCode)
                {
                    foreach (Domain.Entities.Action oAction in oEffect.Actions.Values)
                    {
                        if (oAction.Id.ToString() == sEffectStream)
                        {
                            return oAction.Name;
                        }
                    }
                }
            }
            return null;
        }

        #region "IRepository Members generic"
        
        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// retorna um objeto (TEntity) de mesmo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// retona um bojeto (TEntity) de mesmo nome
        /// </summary>
        /// <param name="sBookName"></param>
        /// <returns></returns>
        public TEntity GetByName(string sBookName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Não implementado (modelo readonly)
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Save(TEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
