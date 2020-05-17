using FinalProject.DataAccess.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.UnitOfWork.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        ICommentRepository Comment { get; }
        IFollowRepository Follow { get; }
        ILikeRepository Like { get; }
        IMessageRepository Message { get; }
        IRetweetRepository Retweet { get; }
        ITweetRepository Tweet { get; }
        IUserRepository User { get; }
        void SaveChange();
    }
}
