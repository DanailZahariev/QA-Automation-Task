using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using FluentAssertions;
using QaAutomationTask.Config;
using QaAutomationTask.Models;
using QaAutomationTask.Pages;
using QaAutomationTask.Tests.TestData;

namespace QaAutomationTask.Tests.Ui;

[TestFixture]
[Category("Ui")]
[AllureEpic("Task 2: UI Test Scenario")]
[AllureFeature("Registration Form Validation")]
public class RegistrationValidationTests : BaseTest
{
    private RegistrationPage _registrationPage;

    [SetUp]
    public async Task PageSetup()
    {
        _registrationPage = new RegistrationPage(Page);
        await _registrationPage.NavigateToAsync(ConfigReader.AppSettings.UiSettings.BaseUrl);
    }

    [Test]
    [AllureStory("Validate empty form submission")]
    [AllureTag("Validation", "Negative")]
    [AllureSeverity(SeverityLevel.critical)]
    public async Task Should_Show_Error_When_Required_Fields_Are_Empty()

    {
        await _registrationPage.SubmitAsync();
        await _registrationPage.WaitForValidationClass();


        var isFormValidated = await _registrationPage.IsFormValidated();

        isFormValidated.Should().BeTrue("Form should have 'was-validated' class after submit");
        (await _registrationPage.IsFieldInvalidAsync("firstName"))
            .Should().BeTrue("First Name should be invalid");
        (await _registrationPage.IsFieldInvalidAsync("lastName"))
            .Should().BeTrue("Last Name should be invalid");
        (await _registrationPage.IsFieldInvalidAsync("userNumber"))
            .Should().BeTrue("Mobile Number should be invalid");
        (await _registrationPage.SuccessModal.IsVisibleAsync())
            .Should().BeFalse("Success modal should not be visible on invalid submission");
    }

    [Test]
    [AllureStory("Validate specific invalid data inputs")]
    [AllureTag("Validation", "DataDriven")]
    [Description(
        "Verifies that the form correctly handles various invalid inputs provided via test data (e.g. missing names, missing gender, short phone number).")]
    [TestCaseSource(typeof(RegistrationData), nameof(RegistrationData.GetInvalidRegistrationScenarios))]
    public async Task Should_Show_Error_When_Data_Is_Invalid(RegistrationDto user, string expectedErrorFieldId)
    {
        await _registrationPage.FillFormAsync(user);

        await _registrationPage.SubmitAsync();

        await _registrationPage.WaitForValidationClass();

        var isFieldInvalid = await _registrationPage.IsFieldInvalidAsync(expectedErrorFieldId);

        isFieldInvalid.Should().BeTrue($"Field '{expectedErrorFieldId}' should be marked as invalid.");

        (await _registrationPage.SuccessModal.IsVisibleAsync()).Should().BeFalse();
    }
}