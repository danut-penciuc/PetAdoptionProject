using Application.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{

    public class CreateAdopterValidator : AbstractValidator<CreateAdopterRequest>
    {
        public CreateAdopterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.ClientCode)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
