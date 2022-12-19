using System;

namespace MBook.Domain.Base
{
    /// <summary>
    /// classe base para entities
    /// </summary>
    public abstract class BaseEntity:IComparable<BaseEntity>
    {
        /// <summary>
        /// primeira escrita da propriedade Id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// primeira escrita da propriedade Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Permite comparar o Id de uma instância com a o Id de outra
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int CompareTo(BaseEntity obj)
        {
            return Id.CompareTo(obj.Id);
        }
    }
}
