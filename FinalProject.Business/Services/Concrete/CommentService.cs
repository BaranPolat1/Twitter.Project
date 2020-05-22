using AutoMapper;
using FinalProject.Associate.DTO;
using FinalProject.Business.Services.Abstract;
using FinalProject.Business.UnitOfWork.Abstraction;
using FinalProject.Entities.Entity;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Business.Services.Concrete
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        public CommentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public void Add(CommentDTO model, string content, string userName, Guid tweetId)
        {
            try
            {
                Comment comment = new Comment();
                var user = _uow.User.Find(x => x.UserName == userName);
                model.Content = content;
                model.UserId = user.Id;
                model.TweetId = tweetId;
                comment.InjectFrom(model);
                _uow.Comment.Add(comment);
                _uow.SaveChange();
            }
            catch
            {

               
            }
           
        }

        public void Delete(Comment comment)
        {
            if (comment != null)
            {
                _uow.Comment.Delete(comment);
                _uow.SaveChange();
            }
            
        }

        public void Delete(Guid Id)
        {
            var comment = _uow.Comment.GetById(Id);
            if (comment!= null)
            {
                _uow.Comment.Delete(comment);
                _uow.SaveChange();
            }
         
        }

        public CommentDTO Get(Guid Id)
        {
            var comment = _uow.Comment.GetById(Id);
            CommentDTO model = new CommentDTO();
            model.InjectFrom(comment);
            return model;
        }

        public IList<CommentDTO> GetAll()
        {
            var comment = _uow.Comment.GetAll();
            var model = _mapper.Map<IList<CommentDTO>>(comment);
            return model;
        }

        public IList<CommentDTO> GetByTweet(Guid Id)
        {
            var comment = _uow.Comment.FindByList(x => x.TweetId == Id).OrderByDescending(x => x.CreatedDate).Take(10);
            var model = _mapper.Map<IList<CommentDTO>>(comment);
            return model;
        }

        public IList<CommentDTO> GetByUser(string Id)
        {
            var comment = _uow.Comment.FindByList(x => x.UserId == Id);
            var model = _mapper.Map<IList<CommentDTO>>(comment);
            return model;
        }
    }
}
