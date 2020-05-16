using FinalProject.DataAccess.KernelRepository.Abstraction;
using FinalProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.DataAccess.Repository.Abstraction
{
    public interface IUserRepository : IEntityRepository<AppUser>
    {
        
    }
}
