using FluentValidation;
using HealthWay.Agents.Domain;

namespace HealthWay.Agents.Application.PatientVital;

public sealed class PatientVitalsValidator : AbstractValidator<PatientVitals>
{
    private static readonly TimeSpan ClockSkewTolerance = TimeSpan.FromMinutes(5);

    public PatientVitalsValidator(TimeProvider timeProvider)
    {
        RuleFor(x => x.PatientId).NotEmpty();

        RuleFor(x => x.MeasuredAt)
            .Must(measuredAt => measuredAt <= timeProvider.GetUtcNow() + ClockSkewTolerance)
            .WithMessage("Measurement time must not be in the future.");

        RuleFor(x => x.BloodPressure).NotNull().SetValidator(new BloodPressureValidator());

        RuleFor(x => x.HeartRateBpm).InclusiveBetween(20, 300);

        RuleFor(x => x.BloodGlucoseMmolPerL).GreaterThan(0m).LessThanOrEqualTo(60m);
    }
}
