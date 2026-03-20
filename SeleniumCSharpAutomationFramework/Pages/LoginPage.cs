using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

public class LoginPage : BasePage
{
    //private IWebDriver driver;
    public LoginPage(IWebDriver driver): base(driver)
    { }

    private By username = By.Id("user-name");
    private By password = By.Id("password");
    private By loginButton = By.Id("login-button");

    private By errorMessage = By.CssSelector("[data-test='error']");


    public void Login (string user, string pass)
    {
        Type(username, user);
        Type(password, pass);
        Click(loginButton);
    }

    public bool IsErrorDisplayed()
    {
        var element = WaitForElement(errorMessage);
        return element != null && element.Displayed;
        /*
        try
        {
            return WaitForElement(errorMessage).Displayed;
        }
        catch
        {
            return false;
        }
        */
    }
}