using BPCalculator;
using System.Runtime.Intrinsics.X86;

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

    }
}