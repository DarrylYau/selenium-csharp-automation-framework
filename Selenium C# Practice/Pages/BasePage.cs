

using OpenQA.Selenium;


public class BasePage
{
    protected IWebDriver driver;

    public BasePage(IWebDriver driver)
    {
        this.driver = driver;
    }

    protected void Click(By locator)
    {
        driver.FindElement(locator).Click();
    }

    protected void Type(By locator, string text)
    {
        driver.FindElement(locator).SendKeys(text);
    }
}