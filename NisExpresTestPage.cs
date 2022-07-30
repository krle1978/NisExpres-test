using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
            Asserttation assert = new Asserttation(Driver);
            Driver.FindElement(By.ClassName("select2-selection__arrow")).Click();
            Driver.FindElement(By.ClassName("select2-search__field")).SendKeys(firstTown);
            Trace.WriteLine($"Start stationen set:");
            DriverHelper.ListElementsToText(Driver, "ul.select2-results__options li");
            Driver.FindElement(By.CssSelector("ul.select2-results__options li:nth-of-type(4)")).Click();
            Driver.FindElement(By.CssSelector("span[title='Izaberite.....']")).Click();
            IWebElement secoTown = Driver.FindElement(By.CssSelector("input[class='select2-search__field']"));
            secoTown.SendKeys(secoundTown);
            Trace.WriteLine($"End stationen set:");
            DriverHelper.ListElementsToText(Driver, "ul.select2-results__options li");
            secoTown.SendKeys(Keys.Enter);
            assert.IsTextEqual("span.select2-selection__rendered",firstTown);
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
            DriverHelper.WaitExplicitlyVisable(Driver, "div#main-kolona-linija");
            Trace.WriteLine("Bus lines are:");
            DriverHelper.ListElementsToText(Driver, "div#column-putovanja");
            DriverHelper.Pause();

        }

        [Obsolete]
        internal void EmptyFieldsAssert()
        {
            Asserttation assert = new Asserttation(Driver);
            DriverHelper.HoverOverButton(Driver,"div.nav-item.dropdown.shadow.dropdown-objekti.show");
            Driver.FindElement(By.CssSelector("ul.navbar-nav.d-md-flex.justify-content-md-end li:nth-of-type(2) div div a:nth-of-type(2)")).Click();
            DriverHelper.WaitExplicitlyClickable_Click(Driver,"button.btn.btn-primary.w-100.top-btn");
            assert.AllertWindow();
            Driver.FindElement(By.CssSelector("button.btn.btn-primary.alert-close-button")).Click();
        }

        [Obsolete]
        internal void TravelingAssert()
        {
            Asserttation assert = new Asserttation(Driver);
            Driver.FindElement(By.ClassName("select2-selection__arrow")).Click();
            IWebElement firstStation = Driver.FindElement(By.ClassName("select2-search__field"));
            firstStation.SendKeys("beograd");
            Trace.WriteLine($"Start stationen set:");
            DriverHelper.ListElementsToText(Driver, "ul.select2-results__options li");
            firstStation.SendKeys(Keys.Enter);
            //firstStation.SendKeys(Keys.Tab);
            Driver.FindElement(By.CssSelector("span[title='Izaberite.....']")).Click();
            IWebElement secoTown = Driver.FindElement(By.CssSelector("input[class='select2-search__field']"));
            secoTown.SendKeys("novi sad");
            Trace.WriteLine($"End stationen set:");
            DriverHelper.ListElementsToText(Driver, "ul.select2-results__options li");
            secoTown.SendKeys(Keys.Enter);
            
            IWebElement travelDate = Driver.FindElement(By.Id("vrijeme"));
            travelDate.Click();
            string day = DateTime.Now.Day.ToString(), month = DateTime.Now.Month.ToString(), year = DateTime.Now.Year.ToString();
            int yesturday = int.Parse(day)-1;
            travelDate.SendKeys(yesturday.ToString());
            travelDate.SendKeys(month);
            travelDate.SendKeys(Keys.Tab);
            travelDate.SendKeys(year);
            Driver.FindElement(By.Id("button-putovanja")).Click();
            DriverHelper.Pause();
            Driver.FindElement(By.Id("button-putovanja")).Click();
            DriverHelper.WaitExplicitlyVisable(Driver, "div#main-kolona-linija");
            assert.IsYesturday();
            DriverHelper.Pause();
        }

        [Obsolete]
        public void AttributsElement()
        {
            IWebElement iconRezervacija = Driver.FindElement(By.CssSelector("img#ikonica-1")), 
                iconInfo = Driver.FindElement(By.CssSelector("button.btn")), 
                icoAktuel = Driver.FindElement(By.CssSelector("img#ikonica-2"));
            string icoRezSrcAttr = iconRezervacija.GetAttribute("src"), 
                icoRezCssValue = iconRezervacija.GetCssValue("src"),
                infoAttrClass = iconInfo.GetAttribute("class"),
                infoAttrType = iconInfo.GetAttribute("type"),
                infoAttrStyle = iconInfo.GetAttribute("style");

            Trace.WriteLine($"Icon Rezervacija:\n" +
                $"SRC: {icoRezSrcAttr}\nCSS Value: {icoRezCssValue}\n_____\n" +
                $"Icon Informacije:\n" +
                $"class: {infoAttrClass}\nType: {infoAttrType}\nStyle: {infoAttrStyle}");

            icoAktuel.Click();
            DriverHelper.Pause();
            DriverHelper.WaitExplicitlyVisable(Driver, "div.container");
            List<string> strongList = new List<string>();
            DriverHelper.ListElementsToString(Driver, "div.modal-body strong", strongList);
            Trace.WriteLine("\nBolded text is:");
            for (int i = 0; i < strongList.Count; i++)
            {
                if (strongList[i] != string.Empty)
                {
                    Trace.WriteLine(strongList[i]);

                }
            }
            IWebElement submitButton = Driver.FindElement(By.CssSelector("button.btn.btn-primary.close-button"));
            if (submitButton.Enabled)
            {
                Trace.WriteLine($"____________\nSubmit button:\n" +
                    $"class: {submitButton.GetAttribute("class")}\n" +
                    $"data-bss-hover-animate: {submitButton.GetAttribute("data-bss-hover-animate")}\n" +
                    $"data-dismiss: {submitButton.GetAttribute("data-dismiss")}\n" +
                    $"type: {submitButton.GetAttribute("type")} \n" +
                    $"style: {submitButton.GetAttribute("style")} \n");
               
            }
            DriverHelper.FindElementWithJS_Click(Driver, "button.btn.btn-primary.close-button");
            DriverHelper.WaitExplicitlyClickable_Click(Driver, "button.btn.btn-primary.close-button");
            //Driver.FindElement(By.CssSelector("button.btn.btn-primary.close-button")).Click();
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
            DriverHelper.WaitExplicitlyVisable(Driver, "button.dropdown-item.active");
            Driver.FindElement(By.CssSelector("button.dropdown-item.active")).Click();
            IWebElement endStation = Driver.FindElement(By.CssSelector("section#travel-section div div div:nth-of-type(2) input"));
            endStation.SendKeys("novi sad");
            DriverHelper.WaitExplicitlyClickable_Click(Driver, "button.dropdown-item.active");
            //Driver.FindElement(By.CssSelector("button.dropdown-item.active")).Click();

            DriverHelper.WaitExplicitlyVisable(Driver, "button.btn.btn-primary.w-100.top-btn");
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
            DriverHelper.WaitExplicitlyVisable(Driver, "ul[role='listbox']");
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
            DriverHelper.WaitExplicitlyVisable(Driver, "div#main-kolona-linija");
            Trace.WriteLine("Bus lines are:");
            DriverHelper.WaitExplicitlyVisable(Driver, "div.row.tabela-row-ruta");
            DriverHelper.ListElementsToText(Driver, "div.row.tabela-row-ruta");
            DriverHelper.Pause();
        }
    }
}
