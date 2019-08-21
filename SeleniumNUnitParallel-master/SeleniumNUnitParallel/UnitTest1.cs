using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SeleniumNUnitParallel
{
    [TestFixture]
    [Parallelizable]
    public class FirefoxTest: Hooks
    {
        public FirefoxTest() : base(BrowserType.chrome)
        {
        }

        [Test]
        public void LaunchURLandNavigateTOVIdeoTest()
        {
            try
            {

                Driver.Navigate().GoToUrl("https://www.youtube.com");
                //Driver.FindElement(By.XPath("//*[@id='search-icon-legacy']/preceding-sibling::*")).SendKeys("step-inforum");
                Driver.FindElement(By.Id("search-form")).Click();
                String js = "arguments[0].setAttribute('value','nitin')";
                ((OpenQA.Selenium.IJavaScriptExecutor)Driver).ExecuteScript(js, Driver.FindElement(By.Id("search-form")));

                Driver.FindElement(By.Id("search-form")).SendKeys("step-inforum");
                Driver.FindElement(By.XPath("//*[@id='channel-title']/span[text()='STeP-IN Forum'])[1]")).Click();
                //click Video  tab on step in forum page
                Driver.FindElement(By.XPath("//*[@id='tabsContent']/paper-tab[2]/div")).Click();
                //get video name from API

                //Search the video on page
                string videoName = APICall.fetchAPIResult();

                //locate the video on screen  take screenshot
                Screenshot ScreenShot = ((ITakesScreenshot)Driver).GetScreenshot();
                //Save the screenshot
                String finalpath = @"C:\Users\ngarg1\source\repos\TestAutothonProject2\SeleniumNUnitParallel-master\SeleniumNUnitParallel\bin\Debug\" + "ScreenShot_" + DateTime.Now.ToString("ddMMhhmmss") + ".png";
                ScreenShot.SaveAsFile(finalpath, ScreenshotImageFormat.Png);
                //change the video quality to P360

                //Get  the name of all vidoes using Up next
                IList<IWebElement> nextVideos = Driver.FindElements(By.Id("video-title"));
                IList<IWebElement> nextVideosDisplayed = new List<IWebElement>();
                foreach (IWebElement ele in nextVideos)
                {
                    if (ele.Displayed)
                        nextVideosDisplayed.Add(ele);
                }
              
                IList<IWebElement> nextAllVideos = new List<IWebElement>();
              
                do
                {
                    BrowserSearch.PerformPageLoad(Driver, nextVideosDisplayed);
                    //((OpenQA.Selenium.IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView();", nextVideosDisplayed[nextVideosDisplayed.Count - 1]);
                    //Thread.Sleep(10 * 1000);
                    nextAllVideos=Driver.FindElements(By.Id("video-title"));
                    foreach (IWebElement ele in nextAllVideos)
                    {
                        if (!ele.Displayed)
                            nextAllVideos.Remove(ele);
                    }
                } while (nextVideosDisplayed.Count != nextAllVideos.Count);

                List<string> NextVideoNames = new List<string>();
                foreach (IWebElement ele in nextVideosDisplayed)
                {
                    if(ele.Displayed)
                        NextVideoNames.Add(ele.GetAttribute("title"));
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
