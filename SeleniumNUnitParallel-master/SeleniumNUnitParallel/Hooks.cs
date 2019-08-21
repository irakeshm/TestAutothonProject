using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Reflection;
using OpenQA.Selenium.Appium.Android;

namespace SeleniumNUnitParallel
{
    [TestFixture]
    public class Hooks : Base
    {
        BrowserType _browser;

        public Hooks(BrowserType browser)
        {
            _browser = browser;
        }


        [SetUp]
        public void SetUpEnvironment()
        {
            SelectBrowser(_browser);
        }

        [TearDown]
        public void TearDownEnvironment()
        {
            Driver.Quit();
        }

        private void SelectBrowser(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.firefox:
                    var driverDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(driverDir, "geckodriver.exe");
                    service.HideCommandPromptWindow = true;
                    service.SuppressInitialDiagnosticInformation = true;
                    FirefoxOptions options = new FirefoxOptions();
                    Driver = new FirefoxDriver(service, options, TimeSpan.FromMinutes(1));
                    Driver.Manage().Window.Maximize();
                    break;
                case BrowserType.chrome:
                    Driver = new ChromeDriver();
                    break;

                case BrowserType.Mobile:
                    AppiumOptions appiumoptions = new AppiumOptions();
                    appiumoptions.AddAdditionalCapability("browserName", "chrome");
                    appiumoptions.AddAdditionalCapability("platformName", "Android");
                    appiumoptions.AddAdditionalCapability("platformVersion", "7.0");
                    appiumoptions.AddAdditionalCapability("deviceName", "ZY2243FCHG");
                    _driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), appiumoptions);
                    break;


                default:
                    break;
            }
        }
    }

    public enum BrowserType
    {
        firefox,
        chrome,
        Mobile
    }
}