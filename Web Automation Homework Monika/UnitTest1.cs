using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Web_Automation_Homework_Monika
{
    public class Tests: WebDriver
    {

        private string userEmail = "fasanej568@keagenan.com";
        private string userPassword = "keagenan";
        private string itemName = "Blouse";
        private string orderCompletionMessage = "Your order on My Store is complete.";

        [SetUp]
        public void Setup()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
            
        }

        [Test]
        public void LoginTest()
        {

            DoLogin();
            IWebElement log_off = Driver.FindElement(By.LinkText("Sign out"));
            Assert.AreEqual("Sign out", log_off.Text, "Error message: this button is not visible on the page");
        }

        [Test]
        public void ItemSearchTest()
        {
            DoLogin();
            SearchItem();

            IWebElement item_title = Driver.FindElement(By.LinkText("Blouse")); ;
            Assert.AreEqual(itemName, item_title.Text, "Error message: item title does not contain search word 'Blouse'");
        }
        [Test]
        public void OrderIsCompletedTest()
        {
            DoLogin();

            SearchItem();

            BuyItem();


            IWebElement order_completed = Driver.FindElement(By.CssSelector("#order_step + .box p")); ;
            Assert.AreEqual(orderCompletionMessage, order_completed.Text, "Error message: order is not completed");
        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
                Driver.Quit();
        }

        //-------------------------------------------------------------------------------------
        public void DoLogin()
        {
            IWebElement login_btn = Driver.FindElement(By.ClassName("login"));
            login_btn.Click();

            IWebElement email_txt = Driver.FindElement(By.Id("email"));
            email_txt.SendKeys(userEmail);

            IWebElement password_txt = Driver.FindElement(By.Id("passwd"));
            password_txt.SendKeys(userPassword);

            IWebElement loginToAccount_btn = Driver.FindElement(By.Id("SubmitLogin"));
            loginToAccount_btn.Click();
        }

        public void SearchItem()
        {
            IWebElement search_field = Driver.FindElement(By.Id("search_query_top"));
            search_field.SendKeys(itemName);

            IWebElement search_btn = Driver.FindElement(By.Name("submit_search"));
            search_btn.Click();
        }

        public void BuyItem()
        {
            IWebElement item_picture = Driver.FindElement(By.CssSelector("#center_column > ul > li > div > div.left-block > div > a.product_img_link > img"));
            item_picture.Click();

            IWebElement add_to_cart_btn = Driver.FindElement(By.Id("add_to_cart"));
            add_to_cart_btn.Click();

            IWebElement checkout_btn_modal = Driver.FindElement(By.CssSelector("#layer_cart [title='Proceed to checkout']"));
            checkout_btn_modal.Click();

            IWebElement checkout_btn_summary_step = Driver.FindElement(By.LinkText("Proceed to checkout"));
            checkout_btn_summary_step.Click();

            IWebElement checkout_btn_address_step = Driver.FindElement(By.Name("processAddress"));
            checkout_btn_address_step.Click();

            IWebElement check_agree_btn = Driver.FindElement(By.Id("cgv"));
            check_agree_btn.Click();

            IWebElement checkout_btn_shipping_step = Driver.FindElement(By.Name("processCarrier"));
            checkout_btn_shipping_step.Click();

            IWebElement bank_pay_btn = Driver.FindElement(By.ClassName("bankwire"));
            bank_pay_btn.Click();

            IWebElement confirm_order_btn = Driver.FindElement(By.CssSelector("#cart_navigation > button"));
            confirm_order_btn.Click();
        }
    }
}