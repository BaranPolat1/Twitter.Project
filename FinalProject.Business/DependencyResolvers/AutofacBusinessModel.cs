using Autofac;
using FinalProject.Associate.DTO;
using FinalProject.Associate.VM;
using FinalProject.Business.Services.Abstract;
using FinalProject.Business.Services.Concrete;
using FinalProject.Business.Validation.EntitiesValidation;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.DataAccess.Repository.Concrete;
using FinalProject.Entities.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace FinalProject.Business.DependencyResolvers
{
   public class AutofacBusinessModel: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
            builder.RegisterType<FollowService>().As<IFollowService>().InstancePerLifetimeScope();
            builder.RegisterType<LikeService>().As<ILikeService>().InstancePerLifetimeScope();
            builder.RegisterType<RetweetService>().As<IRetweetService>().InstancePerLifetimeScope();
            builder.RegisterType<TweetService>().As<ITweetService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageService>().As<IMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork.Concrete.UnitOfWork>().As<UnitOfWork.Abstraction.IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepositoryEF>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CommentRepositoryEF>().As<ICommentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FollowRepositoeyEF>().As<IFollowRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LikeRepositoryEF>().As<ILikeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MessageRepositoryEF>().As<IMessageRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RetweetRepositoryEF>().As<IRetweetRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TweetRepositoryEF>().As<ITweetRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
            builder.RegisterType<UserValidation>().As<IValidator<UserDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<TweetValidation>().As<IValidator<TweetDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CommentValidation>().As<IValidator<CommentDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<MessageValidation>().As<IValidator<Message>>().InstancePerLifetimeScope();
            builder.RegisterType<LoginValidation>().As<IValidator<LoginDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterValidation>().As<IValidator<RegisterDTO>>().InstancePerLifetimeScope();
        }
    }
}
