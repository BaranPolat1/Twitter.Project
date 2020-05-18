using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Validation.EntitiesValidation
{
  public class LoginValidation:AbstractValidator<FinalProject.Associate.VM.LoginDTO>
    {
        public LoginValidation()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Mail adresi yanlış!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola yanlış!");
           
        }
    }
}
