using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Associate.DTO
{
    public class FollowDTO:BaseDTO
    {
        public Guid Id { get; set; }
        public string FollowerId { get; set; }
        public string FollowedId { get; set; }

    }
}
