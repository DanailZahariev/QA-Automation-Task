namespace QaAutomationTask.Config;

public class AppSettings
{
    public ApiSettings ApiSettings { get; set; } = null!;
    public UiSettings UiSettings { get; set; } = null!;
}

public class ApiSettings
{
    public string BaseUrl { get; set; } = null!;
    public ApiEndpoints Endpoints { get; set; } = null!;
    public int TimeoutSeconds { get; set; }
}

public class ApiEndpoints
{
    public string AddUser { get; set; } = null!;
}

public class UiSettings
{
    public string BaseUrl { get; set; } = null!;
    public string Browser { get; set; } = null!;
    public bool Headless { get; set; }
    public int SlowMo { get; set; }
}