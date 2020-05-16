using FinalProject.Kernel.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Kernel.Entity.Abstract
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        DateTime? CreatedDate { get; set; }
        string CreatedComputerName { get; set; }
        string CreatedIP { get; set; }


        DateTime? ModifiedDate { get; set; }
        string ModifiedComputerName { get; set; }
        string ModifiedIP { get; set; }

        Status Status { get; set; }
    }
}
