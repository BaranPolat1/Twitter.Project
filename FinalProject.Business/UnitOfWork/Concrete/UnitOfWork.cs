using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.DataAccess.Context;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.DataAccess.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectContext db;
        public UnitOfWork(ProjectContext _db)
        {
            db = _db ?? throw new ArgumentNullException("db can not be null");
        }
        private ICommentRepository _comment;
        public ICommentRepository Comment
        {
            get { return _comment ?? (_comment = new CommentRepositoryEF(db)); }
        }
        private IFollowRepository _follow;
        public IFollowRepository Follow
        {
            get { return _follow ?? (_follow = new FollowRepositoeyEF(db)); }
        }

        private ILikeRepository _like;
        public ILikeRepository Like
        {
            get { return _like ?? (_like = new LikeRepositoryEF(db)); }
        }

        private IMessageRepository _message;
        public IMessageRepository Message
        {
            get { return _message ?? (_message = new MessageRepositoryEF(db)); }
        }

        private IRetweetRepository _retweet;
        public IRetweetRepository Retweet
        {
            get { return _retweet ?? (_retweet = new RetweetRepositoryEF(db)); }
        }

        private ITweetRepository _tweet;
        public ITweetRepository Tweet
        {
            get { return _tweet ?? (_tweet = new TweetRepositoryEF(db)); }
        }

        private IUserRepository _user;
        public IUserRepository User
        {
            get { return _user ?? (_user = new UserRepositoryEF(db)); }
        }


        public void SaveChange()
        {
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}

