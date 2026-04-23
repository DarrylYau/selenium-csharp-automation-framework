
using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll;

[Binding]
public class TestHooks
{
    private static ExtentReports extent;
  
    private static ThreadLocal<ExtentTest> _scenario = new ThreadLocal<ExtentTest>();
    private static ThreadLocal<ExtentTest> _step = new ThreadLocal<ExtentTest>();


    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        extent = ExtentManager.GetInstance();
    }

    [BeforeScenario]
    public void BeforeScenario(ScenarioContext scenarioContext)
    {
        WebDriverFactory.InitDriver();

        _scenario.Value = extent.CreateTest($"Scenario: {scenarioContext.ScenarioInfo.Title}");

        WebDriverFactory.GetDriver().Navigate().GoToUrl(ConfigReader.Get("baseUrl"));
        //_test.Value.Info("Navigated to URL: " + ConfigReader.Get("baseUrl"));

    }


    [AfterScenario]
    public void AfterScenario(ScenarioContext scenarioContext)
    {
        try
        {
            var driver = WebDriverFactory.GetDriver();

            if (scenarioContext.TestError != null)
            {
                _scenario.Value.Fail("Test failed: " + scenarioContext.TestError.Message);
            }
            else
            {
                _scenario.Value.Pass("Test passed");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in AfterScenario: " + ex.Message);
        }
        finally
        {
            WebDriverFactory.QuitDriver();
        }
    }

    [BeforeStep]
    public void BeforeStep(ScenarioContext scenarioContext)
    {
        /*Log Step name before execution
        _test.Value.Info("STEP STARTED: " + scenarioContext.StepContext.StepInfo.Text);*/

        var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
        var stepText = scenarioContext.StepContext.StepInfo.Text;

        switch (stepType)
        {
            case "Given":
                _step.Value = _scenario.Value.CreateNode<AventStack.ExtentReports.Gherkin.Model.Given>(stepText);
                break;

            case "When":
                _step.Value = _scenario.Value.CreateNode<AventStack.ExtentReports.Gherkin.Model.When>(stepText);
                break;

            case "Then":
                _step.Value = _scenario.Value.CreateNode<AventStack.ExtentReports.Gherkin.Model.Then>(stepText);
                break;

            default:
                _step.Value = _scenario.Value.CreateNode(stepText);
                break;
        }
    }

    [AfterStep]
    public void AfterStep(ScenarioContext scenarioContext)
    {
        if (_step.Value == null)
        {
            return;
        }

        var stepInfo = scenarioContext.StepContext.StepInfo.Text;

        if (scenarioContext.TestError != null)
        {
            string screenshot = CaptureScreenshot(WebDriverFactory.GetDriver(), stepInfo);

            _step.Value.Fail("FAILED STEP: " + stepInfo).AddScreenCaptureFromPath(screenshot, "Click to view screenshot");
        }
        else
        {
            _step.Value.Pass("PASSED STEP: " + stepInfo);
        }

    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        extent.Flush();
    }

    private string CaptureScreenshot(IWebDriver driver, string testName)
    {
        string screenshotDir = Path.Combine(ExtentManager.ReportDir, "Screenshots");

        if (!Directory.Exists(screenshotDir))
        {
            Directory.CreateDirectory(screenshotDir);
        }
        //string fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        string safeName = SanitizeFileName(testName);
        string fileName = $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        string fullPath = Path.Combine(screenshotDir, fileName);

        //driver.Manage().Window.Maximize();
        driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
      

        Thread.Sleep(3000); // Wait for the page to scroll and stabilize before taking the screenshot
        
     

        if (driver is ITakesScreenshot screenshotDriver)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(fullPath);
        }

        return Path.Combine("Screenshots", fileName);
    }


    private string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}