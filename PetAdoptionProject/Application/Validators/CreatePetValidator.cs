using Application.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{

    public class CreatePetValidator : AbstractValidator<CreatePetRequest>
    {
        public CreatePetValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Type)
                .IsInEnum();
        }
    }
}
