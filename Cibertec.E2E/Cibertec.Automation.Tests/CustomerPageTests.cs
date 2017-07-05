using FluentAssertions;
using Xunit;

namespace Cibertec.Automation.Tests
{
    public class CustomerPageTests
    {
        private readonly CustomerPage _page;
        public CustomerPageTests()
        {
            Driver.GetInstance();
            _page = new CustomerPage();
        }

        [Fact]
        private void Test_Index()
        {
            _page.Go();
            _page.GotToIndex();
            _page.GetList().Should().BeGreaterThan(90);
            Driver.CloseInstance();
        }
    }
}
