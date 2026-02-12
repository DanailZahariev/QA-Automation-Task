using System.Net;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using FluentAssertions;
using QaAutomationTask.Models;
using QaAutomationTask.Services;

namespace QaAutomationTask.Tests.Api;

[AllureNUnit]
[AllureEpic("Task 1: REST API Automated Test Exercise")]
[AllureFeature("User Management")]
public class CreateUserTests
{
    private IUserApiService _userService;

    [SetUp]
    public void Setup()
    {
        _userService = new UserApiService();
    }

    [Test]
    [AllureStory("Create a user and verify the response")]
    [AllureTag("API", "Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    [Description("Sends a POST request to create a user and validates the 201 Created status and response body.")]
    public async Task Should_Create_User_Successfully()
    {
        var userPayload = new UserDto
        {
            FirstName = "Muhammad",
            LastName = "Ovi",
            Age = 250
        };

        var response = await _userService.CreateUserAsync(userPayload);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Data.Should().NotBeNull();
        response.Data!.FirstName.Should().Be(userPayload.FirstName);
        response.Data!.LastName.Should().Be(userPayload.LastName);
        // response.Data.Id.Should().NotBeNull();
    }
}