using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


public class WaitHelper
{
    public static IWebElement WaitForElement(IWebDriver driver, By locator)
    {
        WebDriverWait wait=new WebDriverWait (driver,TimeSpan.FromSeconds(10));
        return wait.Until(d => d.FindElement(locator));
    }
}
