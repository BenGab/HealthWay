using FluentValidation;
using HealthWay.Agents.Domain;

namespace HealthWay.Agents.Application.PatientVital;

public sealed class BloodPressureValidator : AbstractValidator<BloodPressure>
{
    public BloodPressureValidator()
    {
        RuleFor(x => x.SystolicMmHg).InclusiveBetween(40, 300);
        RuleFor(x => x.DiastolicMmHg).InclusiveBetween(20, 200);
        RuleFor(x => x.SystolicMmHg)
            .GreaterThan(x => x.DiastolicMmHg)
            .WithMessage("Systolic pressure must be greater than diastolic pressure.");
    }
}
