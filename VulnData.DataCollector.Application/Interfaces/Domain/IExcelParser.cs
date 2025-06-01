namespace VulnData.DataCollector.Application.Interfaces.Domain;

public interface IExcelParser<T>
{
    Task<List<T>> ParseDataAsync(string filePath);
}