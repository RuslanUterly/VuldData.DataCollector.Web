namespace VulnData.DataCollector.Domain.Options;

public class RabbitMQOptions
{
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public string Queue { get; set; }
    public string Exchange { get; set; }
}