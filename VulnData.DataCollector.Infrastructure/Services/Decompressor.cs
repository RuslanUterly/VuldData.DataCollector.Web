using System.IO.Compression;
using VulnData.DataCollector.Application.Interfaces.Domain;

namespace VulnData.DataCollector.Domain.Services;

public class Decompressor : IDecompressor
{
    public async Task<string> DecompressAsync(string compressedfilePath, string decompressedfilePath)
    {
        await using var compressionStream = new FileStream(compressedfilePath, FileMode.OpenOrCreate);
        
        await using GZipStream decompressionStream = new GZipStream(
            compressionStream,
            CompressionMode.Decompress
        );
        
        await using FileStream outputFileStream = File.Create(decompressedfilePath);
        
        await decompressionStream.CopyToAsync(outputFileStream);
        
        return decompressedfilePath;
    }
}