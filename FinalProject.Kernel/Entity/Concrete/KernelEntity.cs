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

        private DateTime? _createDate = DateTime.Now;
        public DateTime? CreatedDate { get { return _createDate; } set { _createDate = value; } }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }


        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIP { get; set; }

        private Status _status = Status.Active;
        public Status Status { get { return _status; } set { _status = value; } }
    }
}
