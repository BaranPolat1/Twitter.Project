using FinalProject.Associate.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Validation.EntitiesValidation
{
    public class UserValidation:AbstractValidator<UserDTO>
    {
        public UserValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim boş bırakılamaz!").MaximumLength(15).WithMessage("En fazla 15 karakter girilebilir!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad boş bırakılamaz!").MaximumLength(30).WithMessage("En fazla 30 karakter!");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Lütfen en az 8 karakter giriniz!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Boş geçilemez!").MaximumLength(15).WithMessage("En fazla 15 karakter!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen mail adresinizi doğru giriniz!");
                
        }
    }
}
