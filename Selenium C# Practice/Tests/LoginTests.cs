using NUnit.Framework;
public class LoginTests : BaseTest
{
    [Test]
    public void ValidUserCanLogin()
    {
        Console.WriteLine("Test started");
        Console.WriteLine(driver.Url);

        LoginData data = TestDataReader.GetLoginData();

        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(data.username, data.password);

        Assert.That(driver.Url.Contains("inventory"), "After login the browser should be on the inventory page ");
    }
}

