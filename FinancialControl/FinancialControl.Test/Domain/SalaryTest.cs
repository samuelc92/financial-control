using FinancialControl.Api.Income.Domain.Models;
using NUnit.Framework;

namespace FinancialControl.Test.Domain
{
    [TestFixture]
    public class SalaryTest
    {
        [Test]
        public void createSalary_Success()
        {
            var salary = new Salary(1750, 1715);
            Assert.AreEqual(437.5, salary.Irs);
            Assert.AreEqual(402.5, salary.Iva.Amount);
        }
    }
}