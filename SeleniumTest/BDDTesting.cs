// NuGet install Selenium WebDriver package and Support Classes

using OpenQA.Selenium;

// NuGet install Chrome Driver
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

// run 2 instances of VS to do run Selenium tests against localhost
// instance 1 : run web app e.g. on IIS Express
// instance 2 : from Test Explorer run Selenium test
// or use the dotnet vstest task
// e.g. dotnet vstest seleniumtest\bin\debug\netcoreapp2.1\seleniumtest.dll /Settings:seleniumtest.runsettings

namespace BDDTest
{
    [TestClass]
    public class BDDTest
    {
        // .runsettings file contains test run parameters
        // e.g. URI for app
        // test context for this run

        private TestContext testContextInstance;

        // test harness uses this property to initliase test context
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        // URI for web app being tested
        private String webAppUri;

        // .runsettings property overriden in vsts test runner 
        // release task to point to run settings file
        // also webAppUri overriden to use pipeline variable

        [TestInitialize]                // run before each unit test
        public void Setup()
        {
            // read URL from SeleniumTest.runsettings
            //this.webAppUri = testContextInstance.Properties["webAppUri"].ToString();
            this.webAppUri = "https://dh-bpmonitor-ca1-staging.azurewebsites.net/";
        }

        [TestMethod]
        public void TestBPMonUI()
        {

            String chromeDriverPath = Environment.GetEnvironmentVariable("ChromeWebDriver");
            if (chromeDriverPath is null)
            {
                chromeDriverPath = ".";                 // for IDE
            }

            using (IWebDriver driver = new ChromeDriver(chromeDriverPath))
            {
                // any exception below results in a test fail

                // navigate to URI for temperature converter
                // web app running on IIS express
                driver.Navigate().GoToUrl(webAppUri);

                // get weight in stone element
                IWebElement valueAge = driver.FindElement(By.Id("BP_Age"));
                // enter 10 in element
                valueAge.Clear();
                valueAge.SendKeys("40");


                // get weight in stone element
                IWebElement valueSystolic = driver.FindElement(By.Id("BP_Systolic"));
                // enter 10 in element
                valueSystolic.Clear();
                valueSystolic.SendKeys("100");

                // get weight in stone element
                IWebElement valueDiastolic = driver.FindElement(By.Id("BP_Diastolic"));
                // enter 10 in element
                valueDiastolic.Clear();
                valueDiastolic.SendKeys("55");

                // get weight in stone element
                IWebElement valueResting = driver.FindElement(By.Id("BP_Resting"));
                // enter 10 in element
                valueResting.Clear();
                valueResting.SendKeys("60");

                //valueDiastolic.SendKeys("50");

                // submit the form
                driver.FindElement(By.Id("form3")).Submit();

                // explictly wait for result with "BMIValue" item
                IWebElement categoryofBP = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(x => x.FindElement(By.Id("ActCategory")));
                IWebElement RestingCate = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(x => x.FindElement(By.Id("HRCategory")));
                // item comes back like "BMIValue: 24.96"
                String Cate = categoryofBP.Text.ToString();
                String HR_Cat = RestingCate.Text.ToString();


                StringAssert.Contains(Cate, "Ideal");
                StringAssert.Contains(HR_Cat , "Abnormal");

                driver.Quit();

                // alternative - use Cypress or Playright
            }
        }
    }
}
