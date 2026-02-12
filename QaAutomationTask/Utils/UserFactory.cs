using System.Globalization;
using Bogus;
using QaAutomationTask.Models;

namespace QaAutomationTask.Utils;

public class UserFactory
{
    public static UserDto GenerateUser()
    {
        return new Faker<UserDto>()
            .RuleFor(user => user.FirstName, faker => faker.Name.FirstName())
            .RuleFor(user => user.LastName, faker => faker.Name.LastName())
            .RuleFor(user => user.Age, faker => faker.Random.Int(18, 99)).Generate();
    }

    public static RegistrationDto GenerateRegistrationUser()
    {
        return new Faker<RegistrationDto>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Gender, f => f.PickRandom("Male", "Female", "Other"))
            .RuleFor(u => u.Mobile, f => f.Random.ReplaceNumbers("##########"))
            .RuleFor(u => u.BirthDate, f => f.Date.Past(20).ToString("dd MMM yyyy", CultureInfo.InvariantCulture))
            .RuleFor(u => u.Subject, "Maths")
            .RuleFor(u => u.Hobbies, f => ["Sports", "Reading", "Music"])
            .RuleFor(u => u.Address, f => f.Address.StreetAddress())
            .RuleFor(u => u.State, "Uttar Pradesh")
            .RuleFor(u => u.City, "Merrut")
            .Generate();
    }
}