using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class Asserttation
    {
        IWebDriver driver;
        public Asserttation(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void IsTextEqual(string CssSelectPath, string expectedTextOfElement)
        {
            IWebElement element = driver.FindElement(By.CssSelector(CssSelectPath));
            Assert.AreEqual(expectedTextOfElement.ToLower(), element.Text.ToLower());
           
        }
        public void IsTextEqual(IWebElement element, string expectedTextOfElement)
        {
            Assert.AreEqual(expectedTextOfElement, element.Text);

        }
        public void IsYesturday()
        {
            string monatStr;
            int yesturday = DateTime.Now.Day - 1, mont = DateTime.Now.Month, year = DateTime.Now.Year;
            if (mont.ToString().Length<2)
            {
                monatStr = $"0{mont.ToString()}";
            }
            else monatStr = mont.ToString();
            //_=( mont.ToString().Length < 2 ) => monatStr = $"0{mont.ToString()}": monatStr = mont.ToString();
            string datumBox = driver.FindElement(By.CssSelector("span.d-flex.justify-content-center.tabela-datum")).Text;
            Assert.AreEqual($"{yesturday.ToString()}.{monatStr}.{year.ToString()}", datumBox);
        }
        public void AllertWindow()
        {
            IWebElement allertWin = driver.FindElement(By.CssSelector("div.modal-content"));
            IWebElement allertText = driver.FindElement(By.CssSelector("div.modal-body"));
            Assert.IsNotNull(allertWin);
            Assert.AreEqual("Unesite odredišnu stanicu", allertText.Text);
        }
    }
}
