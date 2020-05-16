using FinalProject.DataAccess.Context;
using FinalProject.DataAccess.KernelRepository.Concrete;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.Entities.Entity;


namespace FinalProject.DataAccess.Repository.Concrete
{
    public class CommentRepositoryEF : EntityRepositoryEF<Comment>, ICommentRepository
    {
        public CommentRepositoryEF(ProjectContext db) : base(db)
        {

        }

        public ProjectContext ProjectContext
        {
            get { return db as ProjectContext; }
        }
    }
}
