using OpenQA.Selenium;

public class ScreenshotHelper
{
    public static void TakeScreenshot(IWebDriver driver, string fileName)
    {
        Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        screenshot.SaveAsFile(fileName);
    }
}