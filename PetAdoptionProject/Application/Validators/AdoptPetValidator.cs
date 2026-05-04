using Application.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{

    public class AdoptPetValidator : AbstractValidator<AdoptPetRequest>
    {
        public AdoptPetValidator()
        {
            RuleFor(x => x.PetId)
                .NotEmpty();

            RuleFor(x => x.AdopterId)
                .NotEmpty();
        }
    }
}
