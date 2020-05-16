using FinalProject.Associate.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Business.Validation.EntitiesValidation
{
    public class CommentValidation:AbstractValidator<CommentDTO>
    {
        public CommentValidation()
        {
            RuleFor(x => x.Content).NotEmpty().When(x => x.ImageUpload == null).WithMessage("Boş geçiklemez!").MaximumLength(140).WithMessage("En fazla 140 karakter!");
        }
    }
}
