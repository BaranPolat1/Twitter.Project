using FinalProject.Associate.DTO;
using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Services.Abstract
{
    public interface ICommentService
    {
        void Add(CommentDTO model, string content, string userName, Guid tweetId);
        void Delete(Comment comment);
        void Delete(Guid Id);
        IList<CommentDTO> GetAll();
        IList<CommentDTO> GetByTweet(Guid Id);
        IList<CommentDTO> GetByUser(string Id);
        CommentDTO Get(Guid Id);
    }
}
