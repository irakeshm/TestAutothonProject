using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumNUnitParallel
{
    public class BrowserSearch
    {
        public void LaunchURl()
        {
            //Drive
        }
        public static void PerformPageLoad(IWebDriver driver, IList<IWebElement> elementList)
        {
            ((OpenQA.Selenium.IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", elementList[elementList.Count - 1]);
            Thread.Sleep(10 * 1000);
        }
    }
}
