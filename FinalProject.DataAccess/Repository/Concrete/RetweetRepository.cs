﻿using FinalProject.DataAccess.Context;
using FinalProject.DataAccess.KernelRepository.Concrete;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.DataAccess.Repository.Concrete
{
    public class RetweetRepositoryEF : EntityRepositoryEF<Retweet>, IRetweetRepository
    {
        public RetweetRepositoryEF(ProjectContext db) : base(db)
        {

        }
        public ProjectContext ProjectContext
        {
            get { return db as ProjectContext; }
        }
    }
}
