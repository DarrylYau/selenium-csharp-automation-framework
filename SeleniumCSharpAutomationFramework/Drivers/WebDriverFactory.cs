using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

public class WebDriverFactory
{
    private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

    public static void InitDriver()
    {
        string browser = ConfigReader.Get("browser");
        bool headless = ConfigReader.GetBool("headless");

        if (browser.ToLower() == "chrome")
        {
            var options = new ChromeOptions();

            if (headless)
            {
                options.AddArgument("--headless=new"); // Run Chrome in headless mode for testing without opening a browser window
            }
            options.AddArgument("--start-maximized");
            options.AddArgument("--no-sandbox"); // Bypass OS security model, required for running Chrome in headless mode as root user
            options.AddArgument("--disable-dev-shm-usage"); // Overcome limited resource problems in Docker containers

            driver.Value = new ChromeDriver(options);
        }
        else
        {
            throw new Exception("Unsupported browser specified in configuration: " + browser);
        }
       
    }

    public static IWebDriver GetDriver()
    {
        if(driver.Value == null)
        {
            throw new Exception("Driver is not initialized. Call InitDriver() first.");
        }
        return driver.Value;
    }

    public static void QuitDriver()
    {
        driver.Value?.Quit();
        driver.Value = null; // Clear the driver instance for the current thread
    }
}