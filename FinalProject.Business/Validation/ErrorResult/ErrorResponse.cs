using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Validation.ErrorResult
{
   public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
