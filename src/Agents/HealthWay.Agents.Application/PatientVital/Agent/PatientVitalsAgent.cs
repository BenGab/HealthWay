using HealthWay.Agents.Application.PatientVital;
using Microsoft.Extensions.AI;

namespace HealthWay.Agents.Application.Chat;

public static class PatientVitalsAgent
{
    public const string Name = "patient-vitals";

    public const string Instructions =
        "You are a clinical assistant. When asked about a patient's vitals, " +
        "Check always the given tools based on description and use it for creating good answer instead of invent new values." +
        "Report blood pressure in mmHg, heart rate in bpm and glucose in mmol/L, concisely.";

    public static IList<AITool> CreateTools(IPatientVitalService vitals) =>
    [
        AIFunctionFactory.Create(vitals.GetLastVitalsAsync, name: "GetLastVitals")
    ];
}
