using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class NisExpresTestPage
    {
        private readonly IWebDriver Driver;
        public NisExpresTestPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void NavigateTo(string pageURL = "http://www.nis-ekspres.rs/")
        {
            Driver.Navigate().GoToUrl(pageURL);
            Driver.Manage().Window.Maximize();
            IWebElement NEclose = Driver.FindElement(By.Id(("close")));
            if (NEclose.Displayed == true)
            {
                NEclose.Click();
            }
            
            DriverHelper.Pause();
        }

        [Obsolete]
        public void DestinationCheck( string firstTown, string secoundTown)
        {
            Driver.FindElement(By.ClassName("select2-selection__arrow")).Click();
            Driver.FindElement(By.ClassName("select2-search__field")).SendKeys(firstTown);
            //List<string> towns = ElementTextOutput("ul.select2-results__options li");
            //Trace.WriteLine("Towns are:");
            //for (int i = 0; i < towns.Count; i++)
            //{
            //    Trace.WriteLine($"{i}: {towns[i]}");
            //}
            Trace.WriteLine($"Start stationen set:");
            DriverHelper.ListElementsToText(Driver, "ul.select2-results__options li");
            Driver.FindElement(By.CssSelector("ul.select2-results__options li:nth-of-type(4)")).Click();
            Driver.FindElement(By.CssSelector("span[title='Izaberite.....']")).Click();
            IWebElement secoTown = Driver.FindElement(By.CssSelector("input[class='select2-search__field']"));
            secoTown.SendKeys(secoundTown);
            Trace.WriteLine($"End stationen set:");
            DriverHelper.ListElementsToText(Driver, "ul.select2-results__options li");
            secoTown.SendKeys(Keys.Enter);
            IWebElement travelDate = Driver.FindElement(By.Id("vrijeme"));
            travelDate.Click();
            string day = DateTime.Now.Day.ToString(), month = DateTime.Now.Month.ToString(), year = DateTime.Now.Year.ToString();
            travelDate.SendKeys(day);
            travelDate.SendKeys(month);
            travelDate.SendKeys(Keys.Tab);
            travelDate.SendKeys(year);
            Driver.FindElement(By.Id("button-putovanja")).Click();
            DriverHelper.Pause();
            Driver.FindElement(By.Id("button-putovanja")).Click();
            DriverHelper.WaitExplicitly(Driver, "div#main-kolona-linija");
            Trace.WriteLine("Bus lines are:");
            DriverHelper.ListElementsToText(Driver, "div.row.tabela-row-ruta");
            DriverHelper.Pause();

        }
        public ReadOnlyCollection<IWebElement> listElements(string elementCssSelector) => Driver.FindElements(By.CssSelector(elementCssSelector));
        public List<string> ElementTextOutput(string elementCssSelector)
        {
            List<string> ElementsStrings = new List<string>();
            ReadOnlyCollection<IWebElement> MylistElements = listElements(elementCssSelector);
            for (int i = 0; i < MylistElements.Count; i++)
            {
                ElementsStrings.Add(MylistElements[i].Text);
            }
            return ElementsStrings;
        }

        [Obsolete]
        public void SalingMenuButton()
        {
            IWebElement salingMenu = Driver.FindElement(By.LinkText("Prevoz"));
            salingMenu.Click();
            Driver.FindElement(By.LinkText("Prodaja karata")).Click();
            DriverHelper.Pause();
            IWebElement startStation = Driver.FindElement(By.CssSelector("input.ng-untouched.ng-pristine.ng-valid"));
            startStation.Clear();
            startStation.SendKeys("beograd");
            DriverHelper.WaitExplicitly(Driver, "button.dropdown-item.active");
            Driver.FindElement(By.CssSelector("button.dropdown-item.active")).Click();
            IWebElement endStation = Driver.FindElement(By.CssSelector("section#travel-section div div div:nth-of-type(2) input"));
            endStation.SendKeys("novi sad");
            DriverHelper.WaitExplicitly(Driver, "button.dropdown-item.active");
            Driver.FindElement(By.CssSelector("button.dropdown-item.active")).Click();

            DriverHelper.WaitExplicitly(Driver, "button.btn.btn-primary.w-100.top-btn");
            Driver.FindElement(By.CssSelector("button.btn.btn-primary.w-100.top-btn")).Click();

            List<string> listText = new List<string>();
            ReadOnlyCollection<IWebElement> webElements = DriverHelper.Elements(Driver, "div.row.border-bottom.route-item");
            DriverHelper.ListElementsToString(Driver, "div.row.border-bottom.route-item",listText);
            foreach (string element in listText)
            {
                Trace.WriteLine(element);
            }
            DriverHelper.Pause();
            Driver.FindElement(By.CssSelector("button.btn.btn-primary.w-100.top-btn")).Click();
            
            DriverHelper.Pause(10000);
        }

        [Obsolete]
        public void CSSWriting()
        {
            Driver.FindElement(By.ClassName("select2-selection__arrow")).Click();
            Driver.FindElement(By.ClassName("select2-search__field")).SendKeys("nis");
            DriverHelper.ListElementsToText(Driver, "ul.select2-results__options li");
            Driver.FindElement(By.CssSelector("ul.select2-results__options li:nth-of-type(4)")).Click();
            Driver.FindElement(By.CssSelector("span[title='Izaberite.....']")).Click();
            IWebElement secoTown = Driver.FindElement(By.CssSelector("input[class='select2-search__field']"));
            secoTown.SendKeys("beograd");
            DriverHelper.WaitExplicitly(Driver, "ul[role='listbox']");
            List<string> listElements = new List<string>();
            DriverHelper.ListElementsToString(Driver, "ul[role='listbox']", listElements);
            foreach (string element in listElements)
            {
                Trace.WriteLine(element);
            }
            Driver.FindElement(By.CssSelector("span.select2-results ul")).Click();
            DriverHelper.Pause();
            IWebElement travelDate = Driver.FindElement(By.Id("vrijeme"));
            travelDate.Click();
            string day = DateTime.Now.Day.ToString(), month = DateTime.Now.Month.ToString(), year = DateTime.Now.Year.ToString();
            travelDate.SendKeys(day);
            travelDate.SendKeys(month);
            travelDate.SendKeys(Keys.Tab);
            travelDate.SendKeys(year);
            Driver.FindElement(By.Id("button-putovanja")).Click();
            DriverHelper.Pause();
            Driver.FindElement(By.Id("button-putovanja")).Click();
            DriverHelper.WaitExplicitly(Driver, "div#main-kolona-linija");
            Trace.WriteLine("Bus lines are:");
            DriverHelper.WaitExplicitly(Driver, "div.row.tabela-row-ruta");
            DriverHelper.ListElementsToText(Driver, "div.row.tabela-row-ruta");
            DriverHelper.Pause();
        }
    }
}
