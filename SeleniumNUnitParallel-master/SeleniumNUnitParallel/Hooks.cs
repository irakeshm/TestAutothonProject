using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Reflection;

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
                    break;
                case BrowserType.chrome:
                    Driver = new ChromeDriver();
                    break;
                default:
                    break;
            }
        }
    }
    public enum BrowserType
    {
        firefox,
        chrome
    }
}