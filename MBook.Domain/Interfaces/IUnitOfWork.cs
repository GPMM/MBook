using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBook.Domain.Interfaces
{
    /// <summary>
    /// interface que realiza o commit dos dados no repositorio (persistência)
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// realiza a persistencia no repositório
        /// </summary>
        /// <returns></returns>
        Task Commit();
    }
}
