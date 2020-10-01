using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AppiumDemo
{
    public class Tests : Extent
    {
        private static AndroidDriver<AndroidElement> _driver;
        
        [SetUp]
        public void Setup()
        {

            var options = new AppiumOptions { PlatformName = "Android" };
            options.AddAdditionalCapability("deviceName", "Pixel 2 API 30");
            //options.AddAdditionalCapability(MobileCapabilityType.App, appPath);
            // add app or appPackage / appActivity depending on preference to use a hybrid ap
            options.AddAdditionalCapability("udid", "emulator-5554");
            options.AddAdditionalCapability("automationName", "UiAutomator2"); // this one is important
            options.AddAdditionalCapability("appPackage", "com.etsy.android");
            options.AddAdditionalCapability("appActivity", "com.etsy.android.ui.homescreen.HomescreenTabsActivity");

            // these are optional  -- see DesiredCapabilities Appium docs to learn more
           
            options.AddAdditionalCapability("autoGrantPermissions", true);
            options.AddAdditionalCapability("allowSessionOverride", true);
            options.AddAdditionalCapability("adbExecTimeout", 30000);
            options.AddAdditionalCapability("newCommandTimeout", 60);
           
            _driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), options);
            Extent extent = new Extent();
            extent.StartReport();            

        }


        public void TapByCoordinates(int x, int y)
        {
            var actions = new TouchAction(_driver);
            actions.Tap(x, y).Wait(250).Perform();

        }

        [Test]
        public void Test1()
        {
            test = extent.CreateTest("Etsy App demo using Appium");
            test.Log(Status.Info, "Launching the App");
            Thread.Sleep(1000);
            test.Log(Status.Info, "Click Getting started link to Login");
            _driver.FindElementById("com.etsy.android:id/btn_link").Click();
            Thread.Sleep(1000);
            test.Log(Status.Info, "Enter Userid/email");
            _driver.FindElementById("com.etsy.android:id/edit_username").SendKeys("Username/emailid");
            Thread.Sleep(1000);
            test.Log(Status.Info, "Enter Password");
            _driver.FindElementById("com.etsy.android:id/edit_password").SendKeys("Password");
            Thread.Sleep(1000);
            test.Log(Status.Info, "Sign in");
            _driver.FindElementById("com.etsy.android:id/button_signin").Click();
            Thread.Sleep(2000);
            test.Log(Status.Pass, "You have successfully logged in");
            Thread.Sleep(2000);
            test.Log(Status.Pass, "Click Favourites tab");
            TapByCoordinates(327, 1707);
            Thread.Sleep(2000);
            test.Log(Status.Pass, "Click Updates tab");
            TapByCoordinates(539, 1712);
            Thread.Sleep(2000);
            string msg = _driver.FindElementById("com.etsy.android:id/first_notification_text").Text;
            Console.WriteLine("Notification message displayed: {0}", msg);
            Thread.Sleep(2000);
            Assert.AreEqual("Your first notification!",msg);
            test.Log(Status.Info, "Click Home tab");
            TapByCoordinates(107, 1712);
            Thread.Sleep(1000);
            test.Log(Status.Pass, "Notification message assertion passed! Etsy app demo successfull"); 
            Assert.Pass();  
        }

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            
        }
    }
}
 
