using FinalProject.DataAccess.Context;
using FinalProject.DataAccess.KernelRepository.Concrete;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FinalProject.DataAccess.Repository.Concrete
{
    public class TweetRepositoryEF : EntityRepositoryEF<Tweet>, ITweetRepository
    {
        public TweetRepositoryEF(ProjectContext db) : base(db)
        {

        }
        public ProjectContext ProjectContext
        {
            get { return db as ProjectContext; }
        }
    }
}
