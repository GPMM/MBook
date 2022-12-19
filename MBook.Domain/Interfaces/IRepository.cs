using MBook.Domain.Base;
using System.Collections.Generic;

namespace MBook.Domain.Interfaces
{
    /// <summary>
    /// Interface para repositorios
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// obtem um item por id do repositorio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);
        
        /// <summary>
        /// lista todos os itens do respositorio
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// salva um item no repositorio
        /// </summary>
        /// <param name="entity"></param>
        void Save(TEntity entity);
    }
}
