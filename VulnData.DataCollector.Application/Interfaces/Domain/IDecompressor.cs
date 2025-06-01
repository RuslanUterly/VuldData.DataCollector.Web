namespace VulnData.DataCollector.Application.Interfaces.Domain;

public interface IDecompressor
{
    Task<string> DecompressAsync(string compressedfilePath, string decompressedfilePath);
}