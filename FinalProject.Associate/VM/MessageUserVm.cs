using FinalProject.Associate.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Associate.VM
{
    public class MessageUserVm
    {
        public IList<MessageDTO> Messages { get; set; }
        public UserDTO User { get; set; }
    }
}
