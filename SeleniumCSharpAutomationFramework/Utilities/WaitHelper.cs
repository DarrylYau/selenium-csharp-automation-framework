using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


public class WaitHelper
{
    public static IWebElement WaitForElement(IWebDriver driver, By locator)
    {
        WebDriverWait wait=new WebDriverWait (driver,TimeSpan.FromSeconds(10));
        try
        {
            Console.WriteLine("Using WaitHelper");
            return wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
        }
        catch (WebDriverTimeoutException)
        {
            return null; //Prevent test failure if element is not found within timeout
        }
        
    }
}
