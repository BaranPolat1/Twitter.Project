﻿using FinalProject.DataAccess.Context;
using FinalProject.DataAccess.KernelRepository.Concrete;
using FinalProject.DataAccess.Repository.Abstraction;
using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.DataAccess.Repository.Concrete
{
    public class UserRepositoryEF : EntityRepositoryEF<AppUser>, IUserRepository
    {
        public UserRepositoryEF(ProjectContext db) : base(db)
        {

        }
        public ProjectContext ProjectContext
        {
            get { return db as ProjectContext; }
        }

    }
}
