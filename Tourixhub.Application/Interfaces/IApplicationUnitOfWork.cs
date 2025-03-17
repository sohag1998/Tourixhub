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
        IAppUserRepository AppUserRepository { get; }
        IPostRepository PostRepository { get; }
        ICommentRepository CommentRepository { get; }
        IPostImageRepository PostImageRepository { get; }

        IFriendRepository FriendRepository { get; }
        IFriendRequestReopsitory FriendRequestReopsitory { get; }

        IChatRepository ChatRepository { get; }
    }
}
