using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TorTest
{
    [TestClass]
    public class UnitTest2
    {
        public IWebDriver Driver { get; set; }
        public Process TorProcess { get; set; }
        public WebDriverWait Wait { get; set; }

        [TestMethod]
        public void Open_Tor_Browser()
        {
            Driver.Navigate().GoToUrl(@"https://www.qtptutorial.net/automation-practice");
            var radioButton = Driver.FindElement(By.XPath("//input[@value='I love HP UFT']"));
            radioButton.Click();

            Assert.IsTrue(radioButton.Selected, "The radio button that you want was not selected.");
        }

        [TestInitialize]
        public void SetupTest()
        {
            var torBinaryPath = @"C:\Source\Github\Tor\TorTest\Drivers\Browser\firefox.exe";
            TorProcess = new Process();
            TorProcess.StartInfo.FileName = torBinaryPath;
            TorProcess.StartInfo.Arguments = "-n";
            TorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            TorProcess.Start();

            var profile = new FirefoxProfile();
            profile.SetPreference("network.proxy.type", 1);
            profile.SetPreference("network.proxy.socks", "127.0.0.1");
            profile.SetPreference("network.proxy.socks_port", 9150);
            Driver = new FirefoxDriver(profile);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Driver.Quit();
            TorProcess.Kill();
        }
    }
}
