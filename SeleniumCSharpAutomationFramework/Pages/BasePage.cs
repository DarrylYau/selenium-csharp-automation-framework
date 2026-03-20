

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


public class BasePage
{
    protected IWebDriver driver;
    protected WebDriverWait wait;

    public BasePage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait (driver, TimeSpan.FromSeconds(10));
    }

    protected IWebElement WaitForElement(By locator)
    {
        return WaitHelper.WaitForElement(driver, locator);
       /* return wait.Until(d =>
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
        });  */      
    }
    
    protected void Click(By locator)
    {
        WaitForElement(locator).Click();
    }

    protected void Type(By locator, string text)
    {
        WaitForElement(locator).SendKeys(text);
    }
}