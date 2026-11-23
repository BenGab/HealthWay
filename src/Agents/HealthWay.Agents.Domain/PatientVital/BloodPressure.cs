using System.ComponentModel;

namespace HealthWay.Agents.Domain;

[Description("Blood pressure reading in mmHg.")]
public sealed record BloodPressure(
    [property: Description("Systolic (upper) blood pressure in mmHg.")] int SystolicMmHg,
    [property: Description("Diastolic (lower) blood pressure in mmHg.")] int DiastolicMmHg);
