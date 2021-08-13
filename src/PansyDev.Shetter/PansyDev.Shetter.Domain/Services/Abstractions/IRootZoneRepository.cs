namespace PansyDev.Shetter.Domain.Services.Abstractions
{
    public interface IRootZoneRepository
    {
        bool IsValidRootZone(string zone);
    }
}