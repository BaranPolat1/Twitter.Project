using FinalProject.Associate.Helper;
using FinalProject.Kernel.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinalProject.Associate.DTO
{
    public class BaseDTO
    {
        
  
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
        public string ImagePath { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
