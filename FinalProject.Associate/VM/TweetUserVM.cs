using FinalProject.Associate.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Associate.VM
{
    public class TweetUserVM
    {
        
        public IList<TweetDTO> Tweets { get; set; }
        public IList<CommentDTO> Comments { get; set; }
        public IList<RetweetDTO> Retweets { get; set; }
        public CommentDTO Comment { get; set; }
        public TweetDTO Tweet { get; set; }
        public UserDTO User { get; set; }


    }
}
