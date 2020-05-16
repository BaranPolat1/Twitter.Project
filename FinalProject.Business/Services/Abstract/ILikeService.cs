using FinalProject.Associate.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Services.Abstract
{
   public interface ILikeService
    {
        JsonLikeVM Like(Guid Id, string userName);
    }
}
