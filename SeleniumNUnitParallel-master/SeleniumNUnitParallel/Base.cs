using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace SeleniumNUnitParallel
{
    public class Base
    {
        //IWebdriver Instance
        public IWebDriver Driver { get; set; }
        
        //AppiumDriverInstance
        public AppiumDriver<AndroidElement> _driver { get; set; }
    }
}