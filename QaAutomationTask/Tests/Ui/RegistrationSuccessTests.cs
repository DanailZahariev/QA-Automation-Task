using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using FluentAssertions;
using Microsoft.Playwright;
using QaAutomationTask.Config;
using QaAutomationTask.Models;
using QaAutomationTask.Pages;
using QaAutomationTask.Tests.TestData;

namespace QaAutomationTask.Tests.Ui;

[TestFixture]
[AllureEpic("Task 2: UI Test Scenario")]
[AllureFeature("Automation Practice Form")]
public class RegistrationSuccessTests : BaseTest
{
    private RegistrationPage _registrationPage;

    [SetUp]
    public async Task TestSetup()
    {
        _registrationPage = new RegistrationPage(Page);
        await _registrationPage.NavigateToAsync(ConfigReader.AppSettings.UiSettings.BaseUrl);
    }

    [Test]
    [AllureStory("Automate filling and submitting a registration form")]
    [AllureTag("UI", "Regression")]
    [AllureSeverity(SeverityLevel.critical)]
    [Description("Validates that a user can submit the form with valid data and sees the success modal.")]
    public async Task Should_Submit_Registration_Form_And_Verify_Data()
    {
        var userData = RegistrationData.CreateValidUser();

        await _registrationPage.FillFormAsync(userData);

        await _registrationPage.SubmitAsync();

        await _registrationPage.SuccessModal.WaitForAsync();
        (await _registrationPage.SuccessModal.IsVisibleAsync()).Should().BeTrue();
        (await _registrationPage.ModalTitle.InnerTextAsync()).Should().Be("Thanks for submitting the form");

        (await _registrationPage.ResultValue("Student Name").InnerTextAsync()).Should()
            .Be($"{userData.FirstName} {userData.LastName}");
        (await _registrationPage.ResultValue("Student Email").InnerTextAsync()).Should().Be(userData.Email);
        (await _registrationPage.ResultValue("Mobile").InnerTextAsync()).Should().Be(userData.Mobile);
        (await _registrationPage.ResultValue("Address").InnerTextAsync()).Should().Be(userData.Address);

        (await _registrationPage.CloseModalButton.IsVisibleAsync()).Should().BeTrue();

        await _registrationPage.CloseModalButton.ClickAsync();
        await _registrationPage.SuccessModal.WaitForAsync(new LocatorWaitForOptions
            { State = WaitForSelectorState.Hidden });
        (await _registrationPage.SuccessModal.IsVisibleAsync()).Should().BeFalse();
    }

    [Test]
    [AllureStory("Parameterize the data and test with multiple users")]
    [AllureTag("UI", "DataDriven")]
    [TestCaseSource(typeof(RegistrationData), nameof(RegistrationData.GenerateRandomUsers))]
    public async Task Should_Register_Multiple_Random_Users_Successfully(RegistrationDto user)
    {
        await _registrationPage.FillFormAsync(user);

        await _registrationPage.SubmitAsync();

        await _registrationPage.SuccessModal.WaitForAsync();
        (await _registrationPage.SuccessModal.IsVisibleAsync()).Should().BeTrue();
        (await _registrationPage.ResultValue("Student Name").InnerTextAsync()).Should()
            .Be($"{user.FirstName} {user.LastName}");
        (await _registrationPage.ResultValue("Mobile").InnerTextAsync()).Should().Be(user.Mobile);

        (await _registrationPage.CloseModalButton.IsVisibleAsync()).Should().BeTrue();

        await _registrationPage.CloseModalButton.ClickAsync();
        await _registrationPage.SuccessModal.WaitForAsync(new LocatorWaitForOptions
            { State = WaitForSelectorState.Hidden });
        (await _registrationPage.SuccessModal.IsVisibleAsync()).Should().BeFalse();
    }
}