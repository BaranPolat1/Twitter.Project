using FinalProject.Associate.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Validation.EntitiesValidation
{
    public class TweetValidation:AbstractValidator<TweetDTO>
    {
        public TweetValidation()
        {
            RuleFor(x => x.Content).NotEmpty().When(x=>x.ImageUpload == null).WithMessage("Boş bırakılamaz!").MaximumLength(140).WithMessage("140 karakterden fazla kullanamazsınız!");
            
        }
    }
}
