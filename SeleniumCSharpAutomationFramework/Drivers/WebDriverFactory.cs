using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

public class WebDriverFactory
{
    public static IWebDriver CreateDriver()
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

            return new ChromeDriver(options);
        }
        throw new Exception ("Unsupported browser specified in configuration: " + browser);
    }
}