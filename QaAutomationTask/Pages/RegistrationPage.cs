using Microsoft.Playwright;
using QaAutomationTask.Models;

namespace QaAutomationTask.Pages;

public class RegistrationPage(IPage page) : BasePage(page)
{
    private ILocator FirstNameInput => Page.Locator("#firstName");
    private ILocator LastNameInput => Page.Locator("#lastName");
    private ILocator EmailInput => Page.Locator("#userEmail");

    private ILocator GenderRadio(string gender) =>
        Page.GetByText(gender, new PageGetByTextOptions { Exact = true });

    private ILocator MobileInput => Page.Locator("#userNumber");
    private ILocator DateOfBirthInput => Page.Locator("#dateOfBirthInput");
    private ILocator SubjectsInput => Page.Locator("#subjectsInput");
    private ILocator HobbiesCheckbox(string hobby) => Page.Locator($"//label[contains(text(), '{hobby}')]");
    private ILocator AddressInput => Page.Locator("#currentAddress");
    private ILocator StateDropdown => Page.Locator("#state");
    private ILocator CityDropdown => Page.Locator("#city");
    private ILocator SubmitButton => Page.Locator("#submit");
    public ILocator SuccessModal => Page.Locator(".modal-content");
    public ILocator ModalTitle => Page.Locator("#example-modal-sizes-title-lg");
    public ILocator CloseModalButton => Page.Locator("#closeLargeModal");
    public ILocator ResultValue(string label) => Page.Locator($"//td[text()='{label}']/following-sibling::td");

    public async Task FillFormAsync(RegistrationDto user)
    {
        await RemoveAdsAndFooter();

        await FirstNameInput.FillAsync(user.FirstName ?? "");
        await LastNameInput.FillAsync(user.LastName ?? "");
        await EmailInput.FillAsync(user.Email ?? "");

        if (!string.IsNullOrEmpty(user.Gender)) await GenderRadio(user.Gender).ClickAsync();

        await MobileInput.FillAsync(user.Mobile ?? "");

        await DateOfBirthInput.ClickAsync();
        await DateOfBirthInput.FillAsync(user.BirthDate ?? "");
        await DateOfBirthInput.PressAsync("Enter");

        await SubjectsInput.FillAsync(user.Subject ?? "");
        await SubjectsInput.PressAsync("Tab");

        if (user.Hobbies != null)
        {
            foreach (var hobby in user.Hobbies)
            {
                await HobbiesCheckbox(hobby).ClickAsync();
            }
        }

        await AddressInput.FillAsync(user.Address ?? "");

        await ScrollToBottom();

        await SelectDropdownValue(StateDropdown, user.State ?? "");
        await SelectDropdownValue(CityDropdown, user.City ?? "");
    }

    public async Task SubmitAsync()
    {
        await SubmitButton.ScrollIntoViewIfNeededAsync();
        await SubmitButton.ClickAsync(new LocatorClickOptions { Force = true });
    }

    private async Task SelectDropdownValue(ILocator dropdown, string value)
    {
        await dropdown.ClickAsync();
        await Page.GetByText(value, new PageGetByTextOptions { Exact = true }).ClickAsync();
    }

    public async Task<bool> IsFieldInvalidAsync(string fieldId)
    {
        return await Page.Locator($"#{fieldId}").EvaluateAsync<bool>("el => !el.checkValidity()");
    }

    public async Task WaitForValidationClass()
    {
        await Page.Locator("#userForm.was-validated").WaitForAsync();
    }

    public async Task<bool> IsFormValidated()
    {
        return await Page.Locator("#userForm")
            .GetAttributeAsync("class")
            .ContinueWith(t => t.Result.Contains("was-validated"));
    }

    private async Task ScrollToBottom()
    {
        await Page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");
    }

    private async Task RemoveAdsAndFooter()
    {
        await Page.EvaluateAsync(@"
        const ads = document.querySelector('#fixedban');
        if (ads) ads.style.display = 'none';
    ");
    }
}