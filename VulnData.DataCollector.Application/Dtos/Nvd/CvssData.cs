namespace VulnData.DataCollector.Application.Dtos.Nvd;

public class CvssData
{
    public string Version { get; set; }
    public string VectorString { get; set; }
    public double BaseScore { get; set; }
    public string BaseSeverity { get; set; }
    public string AttackVector { get; set; }
    public string AttackComplexity { get; set; }
    public string PrivilegesRequired { get; set; }
    public string UserInteraction { get; set; }
    public string Scope { get; set; }
    public string ConfidentialityImpact { get; set; }
    public string IntegrityImpact { get; set; }
    public string AvailabilityImpact { get; set; }
    public string AttackRequirements { get; set; }
    public string VulnConfidentialityImpact { get; set; }
    public string VulnIntegrityImpact { get; set; }
    public string VulnAvailabilityImpact { get; set; }
    public string SubConfidentialityImpact { get; set; }
    public string SubIntegrityImpact { get; set; }
    public string SubAvailabilityImpact { get; set; }
    public string ExploitMaturity { get; set; }
    public string ConfidentialityRequirement { get; set; }
    public string IntegrityRequirement { get; set; }
    public string AvailabilityRequirement { get; set; }
    public string ModifiedAttackVector { get; set; }
    public string ModifiedAttackComplexity { get; set; }
    public string ModifiedAttackRequirements { get; set; }
    public string ModifiedPrivilegesRequired { get; set; }
    public string ModifiedUserInteraction { get; set; }
    public string ModifiedVulnConfidentialityImpact { get; set; }
    public string ModifiedVulnIntegrityImpact { get; set; }
    public string ModifiedVulnAvailabilityImpact { get; set; }
    public string ModifiedSubConfidentialityImpact { get; set; }
    public string ModifiedSubIntegrityImpact { get; set; }
    public string ModifiedSubAvailabilityImpact { get; set; }
    public string Safety { get; set; }
    public string Automatable { get; set; }
    public string Recovery { get; set; }
    public string ValueDensity { get; set; }
    public string VulnerabilityResponseEffort { get; set; }
    public string ProviderUrgency { get; set; }
    public string AccessVector { get; set; }
    public string AccessComplexity { get; set; }
    public string Authentication { get; set; }
}