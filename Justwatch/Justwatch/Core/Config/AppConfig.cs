using Newtonsoft.Json;

namespace Justwatch.Core.Config;

internal sealed class AppConfig
{

    public const string FILE_NAME = "config.json";
    
    private static AppConfig? instance;
    public static ServerSettings ServerSettings { get; set; } = new ServerSettings();
    public static SmtpSettings SmtpSettings { get; set; } = new SmtpSettings();
    public static string ConnectionString { get; set; } = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=justwatch";



    private static readonly object lockObj = new object();
    
    public static async Task<AppConfig> GetInstance()
    {
        if (instance == null)
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = LoadFromFile().GetAwaiter().GetResult();
                }
            }
        }
        return instance;
    }

    
    private static async Task<AppConfig> LoadFromFile()
    {
        if (!File.Exists(FILE_NAME))
        {
            Console.WriteLine($"Путь {FILE_NAME} не найден");
            return null;
        }
        else
        {
            var json = await File.ReadAllTextAsync(FILE_NAME);
            var config = JsonConvert.DeserializeObject<AppConfig>(json);
            return config;
        }
    }
}


public class ServerSettings
{
    public string Domain { get; set; } = "localhost";
    public int Port { get; set; } = 3232;
    public string StaticDirectoryPath { get; set; } = "Static/";
}

public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}


