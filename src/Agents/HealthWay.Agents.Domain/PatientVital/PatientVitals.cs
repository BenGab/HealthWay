using System.ComponentModel;

namespace HealthWay.Agents.Domain;

[Description("Latest vital signs of a patient.")]
public sealed record PatientVitals(
    [property: Description("Unique identifier of the patient.")] string PatientId,
    [property: Description("Date and time of the measurement (UTC, ISO 8601).")] DateTimeOffset MeasuredAt,
    [property: Description("Blood pressure reading.")] BloodPressure BloodPressure,
    [property: Description("Heart rate in beats per minute.")] int HeartRateBpm,
    [property: Description("Blood glucose concentration in mmol/L.")] decimal BloodGlucoseMmolPerL);
