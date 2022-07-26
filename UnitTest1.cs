using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        [Obsolete]
        public void TestMethod1()
        {

            using (IWebDriver driver = new ChromeDriver())
            {
                NisExpresTestPage NEPage = new NisExpresTestPage(driver);
                NEPage.NavigateTo();
                NEPage.DestinationCheck("nis", "beograd");
                DriverHelper.Pause();
            }

        }
        [TestMethod]
        [Obsolete]
        public void TicketSaling()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
               NisExpresTestPage NEPage = new NisExpresTestPage(driver);
               NEPage.NavigateTo();
               NEPage.SalingMenuButton();
               
            }
        }
        [TestMethod]
        [Obsolete]
        public void XCSAttacking()
        {
            using(IWebDriver driver = new ChromeDriver())
            {
                NisExpresTestPage NEPage = new NisExpresTestPage(driver);
                NEPage.NavigateTo();
                NEPage.CSSWriting();
            }
        }
       
    }
}
