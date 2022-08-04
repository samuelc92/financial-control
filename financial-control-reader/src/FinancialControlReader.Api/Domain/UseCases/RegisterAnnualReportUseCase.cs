using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialControlReader.Api.Domain.UseCases;
public class RegisterAnnualReportUseCase : IRegisterAnnualReportUseCase
{
  public Task RegisterAsync(string category, double amount, DateTime transactionDate)
  {
    throw new NotImplementedException();
  }
}