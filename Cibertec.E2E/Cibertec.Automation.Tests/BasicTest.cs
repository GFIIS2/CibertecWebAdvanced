using System;
using System.Linq;
using Xunit;

namespace Cibertec.Automation.Tests
{
    public class BasicTest
    {
        [Fact]
        public void GoToGoogle()
        {
            var test = new SimpleTest();
            test.Navigate();
        }
    }
}
