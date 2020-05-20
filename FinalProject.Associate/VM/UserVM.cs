using FinalProject.Associate.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Associate.VM
{
   public class UserVM
    {
        public IList<RetweetDTO> Retweets { get; set; }
        public UserDTO User { get; set; }
        public IList<TweetDTO> Tweets { get; set; }
    }
}
