using FinalProject.Entities.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Validation.EntitiesValidation
{
   public class MessageValidation:AbstractValidator<Message>
    {
        public MessageValidation()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Boş bırakılamaz!");
        }
    }
}
