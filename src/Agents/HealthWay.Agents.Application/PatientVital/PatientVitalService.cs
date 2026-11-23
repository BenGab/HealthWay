using System.ComponentModel;
using FluentValidation;
using HealthWay.Agents.Domain;
using Microsoft.Extensions.Logging;

namespace HealthWay.Agents.Application.PatientVital;

public sealed class PatientVitalService(
    IValidator<PatientVitals> validator,
    ILogger<PatientVitalService> logger) : IPatientVitalService
{
    [Description("Gets the latest recorded vital signs (blood pressure, heart rate, blood glucose) of a patient.")]
    public async Task<PatientVitals> GetLastVitalsAsync(
        [Description("Unique identifier of the patient, for example \"42\".")] string patientId,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetLastVitalsAsync called for patient {PatientId}", patientId);

        var vitals = new PatientVitals(
            patientId,
            DateTimeOffset.UtcNow,
            new BloodPressure(SystolicMmHg: 120, DiastolicMmHg: 80),
            HeartRateBpm: 72,
            BloodGlucoseMmolPerL: 5.4m);

        await validator.ValidateAndThrowAsync(vitals, cancellationToken);

        logger.LogInformation("GetLastVitalsAsync returning vitals for patient {PatientId}", patientId);

        return vitals;
    }
}
