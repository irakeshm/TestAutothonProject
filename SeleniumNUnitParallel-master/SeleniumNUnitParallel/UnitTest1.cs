using NUnit.Framework;
using OpenQA.Selenium;
using System;

/// <summary>
/// Author: Sahil Mutreja
/// Date: March 24, 2018
/// Desc: Parallel test execution using NUnit framework and Parallelizable attribute
/// </summary>
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
        public void FirefoxGoogleTest()
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
