using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace SeleniumTest
{
    class SeleniumTest
    {
        public static void Main(string[] args)
        {
            IWebDriver driver = new FirefoxDriver();

            int currentDistributorID = 1115;

            driver.Navigate().GoToUrl("http://212.147.154.88/Distributor/etDistributorEdit.aspx?DistributorID=" + currentDistributorID);

            IWebElement query1 = null;

            bool onLoginPage = true;

            try
            {
                query1 = driver.FindElement(By.Name("Login1$UserName"));
            }
            catch (NoSuchElementException)
            {
                onLoginPage = false;
            }

            if (onLoginPage)
            {
                try
                {
                    query1.SendKeys("dobyrne@ezetop.com");
                    query1 = driver.FindElement(By.Name("Login1$Password"));
                    query1.SendKeys("0kBxiep6");
                    query1 = driver.FindElement(By.Name("Login1$LoginButton"));
                    query1.Click();
                }
                catch (NoSuchElementException e)
                {
                    Console.WriteLine("Found login username field but failed to find password field??? Error: " + e.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.ToString());
                    return;
                }
            }

            Thread.Sleep(5000);

            try
            {
                query1 = driver.FindElement(By.XPath("/html/body/form/div/div[@id='contents']/h2"));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element not found.");
                Console.ReadKey();
                return;
            }
            catch
            {
                Console.WriteLine("Everything went to crap...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Found text: " + query1.Text);

            if (query1.Text.Contains("Edit Distributor -"))
                Console.WriteLine("Found the Edit Distributor Tag, awesome. Here it is: " + query1.Text);
            else
                Console.WriteLine("Didn't find it, bummer...");

            query1 = driver.FindElement(By.XPath("/html/body/form/div/div/div/fieldset/input[@value='Manage Operators']"));
            query1.Click();

            Console.ReadKey();

            driver.Quit();

            /*
            IWebElement query = driver.FindElement(By.Name("q"));

            query.SendKeys("Cheese");

            query.Submit();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.Title.ToLower().StartsWith("cheese"); });

            System.Console.WriteLine("Page title is: " + driver.Title);

            driver.Quit();
            */
        }
    }
}
