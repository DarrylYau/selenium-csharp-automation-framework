using Reqnroll;
using NUnit.Framework;
using OpenQA.Selenium;

[Binding]
public class LoginSteps
{
    private readonly IWebDriver driver;
    private readonly LoginPage loginPage;
    private readonly InventoryPage inventoryPage;

    public LoginSteps ()
    {
        driver = WebDriverFactory.GetDriver();
        loginPage = new LoginPage(driver);
        inventoryPage = new InventoryPage(driver);
    }

    [Given("user is on the login page")]
    public void GivenUserIsOnLoginPage()
    {
        driver.Navigate().GoToUrl(ConfigReader.Get("baseUrl"));
    }

    [When("user logs in with username {string} and password {string}")]
    public void WhenUserLogsIn(string username, string password)
    {
        loginPage.Login(username, password);
    }

    [Then("login should be {string}")]
    public void ThenLoginShouldBe(string result)
    {
        if (result.Equals("success", StringComparison.OrdinalIgnoreCase))
        {
            Assert.IsTrue(inventoryPage.IsInventoryPageDisplayed(), "Expected login to be successful, but it failed.");
        }
        else
        {
            Assert.IsTrue(loginPage.IsErrorDisplayed(), "Expected login to fail, but it succeeded.");
        }
    }
}
