using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Task12
{
    [TestFixture]
    public class GismeteoTests
    {
        private string _gismeteoUrl = "https://www.gismeteo.ua/";
        private IWebDriver _chromeDriver = new ChromeDriver(@"D:\");

        [OneTimeSetUp]
        public void SetUp()
        {
            _chromeDriver.Manage().Window.Maximize();
            _chromeDriver.Navigate().GoToUrl(_gismeteoUrl);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _chromeDriver.Close();
            _chromeDriver.Quit();
        }

        //1. Find all divs on the page
        public IReadOnlyCollection<IWebElement> SearchdivsList => _chromeDriver.FindElements(By.CssSelector("div"));

        //2. Find all divs with h2
        public IReadOnlyCollection<IWebElement> Searchh2IndivsList => _chromeDriver.FindElements(By.CssSelector("div > h2"));

        //3. Find all items with news titles(the block under list of cities)(х items)
        public IReadOnlyCollection<IWebElement> SearchReadMoreItemsList => _chromeDriver.FindElements(By.CssSelector(".readmore_item"));

        //5. Get all titles from items from #3
        public IReadOnlyCollection<IWebElement> SearchReadMoreTitlesList => _chromeDriver.FindElements(By.CssSelector(".readmore_title"));

        //8. Find all top menu link
        public IReadOnlyCollection<IWebElement> SearchNavItemsList => _chromeDriver.FindElements(By.CssSelector(".nav_item a"));


        [Test, Order(1)]
        public void FindElements()
        {
            //4. Find the last span with news title
            IWebElement searchElementLastReadMoreItem = _chromeDriver.FindElement(By.XPath("//div[@class='readmore_item'][last()]"));

            //6.Find element with text Киев
            IWebElement searchElementByTextKiev = _chromeDriver.FindElement(By.XPath("//span[contains(text(),'Киев')]"));

            //7.Find the element that describes city next after Киев (not ready)
            //IWebElement nextElement = searchElementByTextKiev.FindElement(By.XPath("//following-sibling::div[@class='cities_item']"));

            IWebElement searchElementBySoftlink = _chromeDriver.FindElement(By.CssSelector(@"a[href$=""soft/""]"));

            //9. On the current city weather page find element for 3 current weekdays
            IWebElement searchElementWeather3Days = _chromeDriver.FindElement(By.CssSelector(@"a[href*=""3-days""]"));

            //10. Find element for currently selected weekday
            IWebElement searchElementWeatherNow = _chromeDriver.FindElement(By.CssSelector(".weather_now"));

            //11. Find temperature when it's Малооблачно (1 element!!)
            if (_chromeDriver.FindElement(By.CssSelector(".description")).Text == "Малооблачно")
            {
                string temperature = _chromeDriver.FindElement(By.CssSelector(".js_meas_container[data-value]")).Text;
            }

            searchElementByTextKiev.Click();
        }
    }
}