using Xunit;
namespace CRUDTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
MyMath mm=new MyMath();
            int input1 = 10, input2 = 5;
            int excpected = 15;
            int actual =mm.Add(input1, input2);
            Assert.Equal(excpected, actual);
        }
    }
}