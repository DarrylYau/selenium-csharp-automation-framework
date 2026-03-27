using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class BaseTest
{
    //protected IWebDriver driver;
    protected static ExtentReports extent;
    
    private static ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();
    protected IWebDriver driver => _driver.Value;

    private static ThreadLocal<ExtentTest> _test = new ThreadLocal<ExtentTest>();
    protected ExtentTest test => _test.Value;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        extent = ExtentManager.GetInstance();
    }


    [SetUp]
    public void Setup()
    {
       // test.Info($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        //TestContext.Out.WriteLine("Setup executed");
        _test.Value = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        
        _driver.Value = WebDriverFactory.CreateDriver();
        //driver = WebDriverFactory.CreateDriver();
        driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
        //Console.WriteLine("Driver created");
        test.Info("Browser launched");
        driver.Navigate().GoToUrl(ConfigReader.Get("baseUrl"));

        test.Info("Navigated to application");
        //driver.Navigate().GoToUrl(ConfigReader.Get("baseUrl"));
        //Console.WriteLine("Navigation completed");
        
    }


    [TearDown]
    public void TearDown()
    {
        try
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {

                string screenshotPath = CaptureScreenshot();

                string fileName = Path.GetFileName(screenshotPath);
                string relativePath = Path.Combine("Screenshots", fileName);

                //string base64 = CaptureScreenshot();
                if(!string.IsNullOrEmpty(screenshotPath))
                {
                    //var media = MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64, "Failure Screenshot").Build();
                    test.Fail("Test failed: " + TestContext.CurrentContext.Result.Message).AddScreenCaptureFromPath(relativePath, "Click to view screenshot");
                }
                else
                {
                    test.Fail("Test failed: " + TestContext.CurrentContext.Result.Message + " (Screenshot not available)");
                }
                
                
            }
            else
            {
                test.Pass("Test passed");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in TearDown:" + ex.Message);
        }
        
        _driver.Value?.Quit();
        _driver.Value?.Dispose();
    }


    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        extent.Flush();

        _driver?.Dispose();
        _test?.Dispose();
    }

    private string CaptureScreenshot()
    {
        try
        {
            string screenshotDir = Path.Combine(ExtentManager.ReportDir, "Screenshots");

            if (!Directory.Exists(screenshotDir))
            {
                Directory.CreateDirectory(screenshotDir);
            }


            string fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string fullPath = Path.Combine(screenshotDir, fileName);

            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
            Thread.Sleep(500); // Small delay to ensure screenshot captures the final state after failure (e.g., error message displayed)

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(fullPath);

            return fullPath;
            
            
        }
        catch
        {
            Console.WriteLine("Driver not available for screenshot");
            return null;
        }
       
    }
       


}
