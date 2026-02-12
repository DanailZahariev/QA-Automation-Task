using Newtonsoft.Json;

namespace QaAutomationTask.Models;

public class UserDto
{
    [JsonProperty("id")] public int? Id { get; set; }
    [JsonProperty("firstName")] public required string FirstName { get; set; }

    [JsonProperty("lastName")] public required string LastName { get; set; }

    [JsonProperty("age")] public int Age { get; set; }
}