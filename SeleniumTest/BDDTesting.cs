// NuGet install Selenium WebDriver package and Support Classes

using OpenQA.Selenium;

// NuGet install Chrome Driver
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


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
            this.webAppUri = "https://dh-bpmonitor-ca1.azurewebsites.net/";
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

               
                IWebElement valueAge = driver.FindElement(By.Id("BP_Age"));
                valueAge.Clear();
                valueAge.SendKeys("40");

                IWebElement valueSystolic = driver.FindElement(By.Id("BP_Systolic"));
                valueSystolic.Clear();
                valueSystolic.SendKeys("100");

                IWebElement valueDiastolic = driver.FindElement(By.Id("BP_Diastolic"));
                valueDiastolic.Clear();
                valueDiastolic.SendKeys("55");

                 IWebElement valueResting = driver.FindElement(By.Id("BP_Resting"));
                valueResting.Clear();
                valueResting.SendKeys("60");


                // submit the form
                driver.FindElement(By.Id("form3")).Submit();

                  IWebElement categoryofBP = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(x => x.FindElement(By.Id("ActCategory")));
                IWebElement RestingCate = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(x => x.FindElement(By.Id("HRCategory")));
                String Cate = categoryofBP.Text.ToString();
                String HR_Cat = RestingCate.Text.ToString();


                StringAssert.Contains(Cate, "Ideal");
                StringAssert.Contains(HR_Cat , "Normal");

                driver.Quit();

            }
        }
    }
}
