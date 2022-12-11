using BPCalculator;

namespace BPMonitorUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            BloodPressure BPressure = new BloodPressure() { Systolic = 140, Diastolic = 90 };
            Assert.AreEqual(BPressure.Category, BPCategory.High);

        }
        [TestMethod]
        public void TestMethod2()
        {

            BloodPressure BPressure = new BloodPressure() { Systolic = 150, Diastolic = 80 };
            Assert.AreEqual(BPressure.Category, BPCategory.High);

        }
        [TestMethod]
        public void TestMethod3()
        {
            BloodPressure BPressure = new BloodPressure() { Systolic = 130, Diastolic = 85 };
            Assert.AreEqual(BPressure.Category, BPCategory.PreHigh);

        }
        [TestMethod]
        public void TestMethod4()
        {
            BloodPressure BPressure = new BloodPressure() { Systolic = 100, Diastolic = 70 };
            Assert.AreEqual(BPressure.Category, BPCategory.Ideal);

        }
        [TestMethod]
        public void TestMethod5()
        {
            BloodPressure BPressure = new BloodPressure() { Systolic = 89, Diastolic = 50 };
            Assert.AreEqual(BPressure.Category, BPCategory.Low);

        }
        [TestMethod]
        public void TestMethod6()
        {
            BloodPressure THeartrate = new BloodPressure() { Age = 40, Resting = 43 };
            Assert.AreEqual(THeartrate.HRCategory, TargetHRCategory.abnormal);

        }
        [TestMethod]
        public void TestMethod7()
        {
            BloodPressure THeartrate = new BloodPressure() { Age = 40, Resting = 43 };
            Assert.AreEqual(THeartrate.HRCategory, TargetHRCategory.abnormal);

        }
        [TestMethod]
        public void TestMethod8()
        {
            BloodPressure THeartrate = new BloodPressure() { Age = 40, Resting = 109 };
            Assert.AreEqual(THeartrate.HRCategory, TargetHRCategory.normal);

        }

    }
}
