using NUnit.Framework;
public class LoginTests : BaseTest
{
    [Test, TestCaseSource(typeof(TestDataReader), nameof(TestDataReader.GetLoginData))]
    public void ValidUserCanLogin(LoginData data)
    {
        TestContext.Out.WriteLine("Test started");
        TestContext.Out.WriteLine($"Running test for user: {data.username}");
        TestContext.Out.WriteLine("Current url:" + driver.Url);

        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(data.username, data.password);

        bool isLoginSuccessful;
        bool isErrorDisplayed = loginPage.IsErrorDisplayed();
        
       

        if (data.expectedSuccess)
        {
            //Only create inventoryPage driver when needed
            InventoryPage inventoryPage = new InventoryPage(driver);
            isLoginSuccessful = inventoryPage.IsInventoryPageDisplayed();

           /* //Expect login success
            Assert.That(isLoginSuccessful, $"Expected login success for {data.username}, but login failed.");
            Assert.That(inventoryPage.IsInventoryPageDisplayed(), 
                "Expected user redirected to inventory page but failed");*/
        }
        else
        {
            //Expect login failure
            isLoginSuccessful = false; // Set to false since we expect login to fail
            /*Assert.That(isErrorDisplayed,
                $"Expected error message for {data.username}, but none displayed.");*/
        }

        //Logging more information for Extent Report
        test.Info($"User: {data.username}");
        test.Info($"Expected Login Result: {data.expectedSuccess}");
        test.Info($"Actual Login Result: {isLoginSuccessful}");

        if (data.expectedSuccess)
        {
            //Only create inventoryPage driver when needed
            InventoryPage inventoryPage = new InventoryPage(driver);
            Assert.That(isLoginSuccessful, $"Expected login success for {data.username}, but login failed.");
            Assert.That(inventoryPage.IsInventoryPageDisplayed(),
                "Expected user redirected to inventory page but failed");
        }
        else
        {
            Assert.That(isErrorDisplayed,
                $"Expected error message for {data.username}, but none displayed.");
        }

        if (data.expectedSuccess && isLoginSuccessful)
        {
            test.Pass($"Login successful for {data.username}");

        }
        else if (!data.expectedSuccess && isErrorDisplayed)
        {
            test.Pass($"Login failed as expected for {data.username}");
        }
        else
        {
            test.Fail($"Unexpected login result for {data.username}");
        }



    }
}

