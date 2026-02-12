using Newtonsoft.Json;

namespace QaAutomationTask.Models;

public class UserResponseDto
{
    [JsonProperty("id")] 
    public int? Id { get; set; } 

    [JsonProperty("firstName")] 
    public string FirstName { get; set; }

    [JsonProperty("lastName")] 
    public string LastName { get; set; }

    [JsonProperty("age")] 
    public int Age { get; set; }
}