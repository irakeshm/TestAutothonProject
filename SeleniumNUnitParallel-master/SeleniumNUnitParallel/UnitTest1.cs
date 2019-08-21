using NUnit.Framework;
using OpenQA.Selenium;
using System;


namespace SeleniumNUnitParallel
{
    [TestFixture]
    [Parallelizable]
    public class FirefoxTest: Hooks
    {
        public FirefoxTest() : base(BrowserType.firefox)
        {
        }

        [Test]
        public void LaunchURLandNavigateTOVIdeoTest()
        {
            try
            {

                Driver.Navigate().GoToUrl("https://www.youtube.com");
                Driver.FindElement(By.XPath("//*[@id='search-icon-legacy']/preceding-sibling::*")).SendKeys("step-inforum");
                Driver.FindElement(By.XPath("//*[@id='channel-title']/span[text()='STeP-IN Forum'])[1]")).Click();
                //click Video  tab on step in forum page
                Driver.FindElement(By.XPath("//*[@id='tabsContent']/paper-tab[2]/div")).Click();
                //get video name from API

                //Search the video on page
                string videoName = "";


                //Driver.FindElement(By.Name("q")).SendKeys("Selenium");
                //Driver.FindElement(By.Name("btnK")).Click();
                //Assert.That(Driver.PageSource.Contains("Selenium"), Is.EqualTo(true),
                //                "Text Selenium was not found");
                APICall.fetchAPIResult();
                Driver.Navigate().GoToUrl("https://www.google.co.in");
                Driver.FindElement(By.Name("q")).SendKeys("Selenium");
                Driver.FindElement(By.Name("btnK")).Click();
                Assert.That(Driver.PageSource.Contains("Selenium"), Is.EqualTo(true),
                                "Text Selenium was not found");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } 
        } 
    }

    

    [TestFixture]
    [Parallelizable]
    public class ChromeTest : Hooks
    {
        public ChromeTest() : base(BrowserType.chrome)
        {
        }

        [Test]
        public void ChromeGoogleTest()
        {
            try
            {
                Driver.Navigate().GoToUrl("https://www.google.co.in");
                Driver.FindElement(By.Name("q")).SendKeys("Selenium");
                Driver.FindElement(By.Name("btnK")).Click();
                Assert.That(Driver.PageSource.Contains("Selenium"), Is.EqualTo(true),
                                "Text Selenium was not found");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } 
        }
    }



    [TestFixture]
    [Parallelizable]
    public class MobileTest : Hooks
    {
        public MobileTest() : base(BrowserType.Mobile)
        {
        }

        [Test]
        public void ChromeGoogleTest()
        {
            try
            {
                _driver.Navigate().GoToUrl("https://www.google.co.in");
                _driver.FindElement(By.Name("q")).SendKeys("Selenium");
                _driver.FindElement(By.Name("btnK")).Click();
                Assert.That(Driver.PageSource.Contains("Selenium"), Is.EqualTo(true),
                                "Text Selenium was not found");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
