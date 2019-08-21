﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Threading;
using Zu.WebBrowser.AsyncInteractions;

namespace SeleniumNUnitParallel
{
    [TestFixture]
    [Parallelizable]
    public class FirefoxTest : Hooks
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

                //To bring Video in Center
                while (true)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> Videos = Driver.FindElements(By.XPath(".//*[@id='video-title' and text()='" + videoName + "'] "));
                    if (Videos.Count > 0)
                    {
                        if (Videos[0].Location.Y != Driver.Manage().Window.Position.Y + Driver.Manage().Window.Size.Height / 2)
                        {
                            ScrollTo(0, Videos[0].Location.Y - Driver.Manage().Window.Size.Height / 2); // Make sure element is in the view but below the top navigation pane
                        }
                        break;
                    }                    
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> AllVideos=Driver.FindElements(By.XPath(".//*[@id='video-title']"));
                    ScrollTo(0, AllVideos[AllVideos.Count - 1].Location.Y);
                }





                //locate the video on screen  take screenshot
                Screenshot ScreenShot = ((OpenQA.Selenium.ITakesScreenshot)Driver).GetScreenshot();
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
                    nextAllVideos = Driver.FindElements(By.Id("video-title"));
                    foreach (IWebElement ele in nextAllVideos)
                    {
                        if (!ele.Displayed)
                            nextAllVideos.Remove(ele);
                    }
                } while (nextVideosDisplayed.Count != nextAllVideos.Count);

                List<string> NextVideoNames = new List<string>();
                foreach (IWebElement ele in nextVideosDisplayed)
                {
                    if (ele.Displayed)
                        NextVideoNames.Add(ele.GetAttribute("title"));
                }




            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void ScrollTo(int xPosition = 0, int yPosition = 0)
        {
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            ((OpenQA.Selenium.IJavaScriptExecutor)Driver).ExecuteScript(js);
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
        public void ChromeMobileTest()
        {
            try
            {
                _driver.Navigate().GoToUrl("https://www.youtube.com");
                 System.Threading.Thread.Sleep(3000);
                IWebElement el = _driver.FindElement(By.XPath("//*[@id='search-icon-legacy']/preceding-sibling::*"));
                el.Click();
            _driver.FindElementByAccessibilityId("Search").Click();
                //ele.Click();
                //_driver.FindElement(By.Name("btnK")).Click();
                //Assert.That(Driver.PageSource.Contains("Selenium"), Is.EqualTo(true),
                //  "Text Selenium was not found");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
