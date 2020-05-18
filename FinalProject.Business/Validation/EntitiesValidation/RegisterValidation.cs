using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Validation.EntitiesValidation
{
    public class RegisterValidation:AbstractValidator<FinalProject.Associate.VM.RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email adresinizi doğru giriniz!");
            RuleFor(x => x.UserName).MinimumLength(5).WithMessage("Kullanıcı adı minimum 5 karakter olacak şekilde giriniz!");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim  boş geçilemez!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad boş geçilemez!");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Lütfen parolanızı en az 8 karakter olacak şekilde giriniz!");
        }
    }
}
