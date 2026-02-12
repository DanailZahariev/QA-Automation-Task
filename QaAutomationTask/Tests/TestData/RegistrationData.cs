using QaAutomationTask.Models;
using QaAutomationTask.Utils;

namespace QaAutomationTask.Tests.TestData;

public class RegistrationData
{
    public static RegistrationDto CreateValidUser()
    {
        return new RegistrationDto
        {
            FirstName = "Ivan",
            LastName = "Petrov",
            Email = "ivan.petrov@example.com",
            Gender = "Male",
            Mobile = "0888123456",
            BirthDate = "01 Jan 1990",
            Subject = "Maths",
            Hobbies = new[] { "Sports", "Reading" },
            Address = "123 Test Street",
            State = "Uttar Pradesh",
            City = "Merrut"
        };
    }

    public static IEnumerable<TestCaseData> GetInvalidRegistrationScenarios()
    {
        var validUser = CreateValidUser();

        yield return new TestCaseData(
            new RegistrationDto
            {
                FirstName = "", LastName = validUser.LastName, Email = validUser.Email, Gender = validUser.Gender,
                Mobile = validUser.Mobile, BirthDate = validUser.BirthDate, Subject = validUser.Subject,
                Hobbies = validUser.Hobbies, Address = validUser.Address, State = validUser.State, City = validUser.City
            },
            "firstName"
        ).SetName("Validation_Error_When_FirstName_Is_Empty");

        yield return new TestCaseData(
            new RegistrationDto
            {
                FirstName = validUser.FirstName, LastName = "", Email = validUser.Email, Gender = validUser.Gender,
                Mobile = validUser.Mobile, BirthDate = validUser.BirthDate, Subject = validUser.Subject,
                Hobbies = validUser.Hobbies, Address = validUser.Address, State = validUser.State, City = validUser.City
            },
            "lastName"
        ).SetName("Validation_Error_When_LastName_Is_Empty");

        yield return new TestCaseData(
            new RegistrationDto
            {
                FirstName = validUser.FirstName, LastName = validUser.LastName, Email = validUser.Email,
                Gender = validUser.Gender, Mobile = "123456789", BirthDate = validUser.BirthDate,
                Subject = validUser.Subject,
                Hobbies = validUser.Hobbies, Address = validUser.Address, State = validUser.State, City = validUser.City
            },
            "userNumber"
        ).SetName("Validation_Error_When_Mobile_Is_Short");

        yield return new TestCaseData(
            new RegistrationDto
            {
                FirstName = validUser.FirstName, LastName = validUser.LastName, Email = validUser.Email, Gender = null,
                Mobile = validUser.Mobile, BirthDate = validUser.BirthDate, Subject = validUser.Subject,
                Hobbies = validUser.Hobbies, Address = validUser.Address, State = validUser.State, City = validUser.City
            },
            "gender-radio-1"
        ).SetName("Validation_Error_When_Gender_Not_Selected");
    }

    public static IEnumerable<TestCaseData> GenerateRandomUsers()
    {
        for (var i = 1; i <= 3; i++)
        {
            var user = UserFactory.GenerateRegistrationUser();

            yield return new TestCaseData(user)
                .SetName($"Random_User_{i}");
        }
    }
}