using System.ComponentModel;
using HealthWay.Agents.Domain;

namespace HealthWay.Agents.Application.PatientVital;

public interface IPatientVitalService
{
    [Description("Gets the latest recorded vital signs (blood pressure, heart rate, blood glucose) of a patient.")]
    Task<PatientVitals> GetLastVitalsAsync(
        [Description("Unique identifier of the patient, for example \"42\".")] string patientId,
        CancellationToken cancellationToken = default);
}
