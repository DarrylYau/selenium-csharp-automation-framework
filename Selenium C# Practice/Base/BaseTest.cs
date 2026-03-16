using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

public class BaseTest
{
    protected IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        Console.WriteLine("Setup executed");

        driver = new ChromeDriver();
        Console.WriteLine("Driver created");
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        Console.WriteLine("Navigation completed");
        
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            ScreenshotHelper.TakeScreenshot(driver, "failure.png");
        }    
        
        driver?.Dispose();
    }
}
