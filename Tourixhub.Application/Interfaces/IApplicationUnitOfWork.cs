using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Repository;

namespace Tourixhub.Application.Interfaces
{
    public interface IApplicationUnitOfWork:IUnitOfWork
    {
        IPostRepository PostRepository { get; }
    }
}
