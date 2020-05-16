using FinalProject.Kernel.Entity.Abstract;
using FinalProject.Kernel.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Kernel.Entity.Concrete
{
    public class KernelEntity : IEntity<Guid>
    {

        public Guid Id { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }


        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIP { get; set; }

        public Status Status { get; set; }
    }
}
