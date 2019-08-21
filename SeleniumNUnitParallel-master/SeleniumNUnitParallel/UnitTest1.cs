using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using Zu.WebBrowser.AsyncInteractions;

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
                //IWebElement el = Driver.FindElement(By.XPath(".//*[@id='search']"));
                String inputText = "step-inforum";
                IWebElement el = Driver.FindElement(By.XPath("//*[@id='search-icon-legacy']/preceding-sibling::*"));
                el.Click();
                Actions actions = new Actions(Driver);
                actions.Build();
                actions.MoveToElement(el);
                actions.Click();
                actions.SendKeys(el, inputText);
                //actions.Release();
                actions.Perform();

                IWebElement searchbutton = Driver.FindElement(By.XPath("//*[@id='search-icon-legacy']"));
                searchbutton.Click();
                
                string abc = "(//*[@id='channel-title']/span[text()='STeP-IN Forum'])[1]";
                Driver.FindElement(By.XPath(abc)).Click();
                
                //click Video  tab on step in forum page
                Driver.FindElement(By.XPath("//*[@id='tabsContent']/paper-tab[2]/div")).Click();
                //get video name from API

                //Search the video on page
                string videoName = APICall.fetchAPIResult();

                while (true)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> Videos = Driver.FindElements(By.XPath(".//*[@id='video-title' and text()='" + videoName + "'] "));
                    if (Videos.Count > 0)
                    {
                        Videos[0].Click();

                        break;
                    }
                    //scroll
                }
                

                
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
}
