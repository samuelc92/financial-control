using System.Collections.Generic;
using FinancialControl.Api.Income.Domain.Models;

namespace FinancialControl.Api.Income.Domain.Ports.Repositories
{
    public interface ISalaryRepository
    {
        void Insert(Salary salary);

        void Update(Salary salary);

        Salary GetById(string id);

        IEnumerable<Salary> GetUnpaidIva(); 
            
        double GetTotalIvaAmountUnpaid();
    }
}