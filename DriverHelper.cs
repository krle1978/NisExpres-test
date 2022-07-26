﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class DriverHelper
    {
       
        public static void Pause(int pauseInSec = 5000)
        {
            Thread.Sleep(pauseInSec);
        }

        [Obsolete]
        public static void WaitExplicitlyVisable(IWebDriver driver, string CssSelectorElement)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement elementWaited = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(CssSelectorElement)));
        }
        [Obsolete]
        public static void WaitExplicitlyClickable_Click(IWebDriver driver, string CssSelectorElement)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement elementWaited = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(CssSelectorElement)));
            elementWaited.Click();
        }

        public static void FindElementWithJS_Click(IWebDriver driver,string cssCelectorPath)
        {
            string script = $"document.querySelector('{cssCelectorPath}').click()";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script);
        }

        public static ReadOnlyCollection<IWebElement> Elements(IWebDriver driver, string elementCssSelector) =>  driver.FindElements(By.CssSelector(elementCssSelector));

        public static void ListElementsToString(IWebDriver driver,string elementCssSelector, List<string> listTextStrings)
        {
            ReadOnlyCollection<IWebElement> webElements = Elements(driver, elementCssSelector);
            for (int i = 0; i < webElements.Count; i++)
            {
                listTextStrings.Add(webElements[i].Text);
            }
        }
        public static void ListElementsToText(IWebDriver driver, string elementCssSelector)
        {
            ReadOnlyCollection<IWebElement> webElements = Elements(driver, elementCssSelector);
            for (int i = 0; i < webElements.Count; i++)
            {
                Trace.WriteLine($"{i} {webElements[i].Text}");
                Trace.WriteLine("------------\n");
            }
        }
        public static void HoverOverButton(IWebDriver driver,string cssElementPath)
        {
            IWebElement buttonToHover = driver.FindElement(By.CssSelector("ul.navbar-nav.d-md-flex.justify-content-md-end li:nth-of-type(2)"));
            Actions action = new Actions(driver);
            action.MoveToElement(buttonToHover).Perform();
        }
    }
    
}
