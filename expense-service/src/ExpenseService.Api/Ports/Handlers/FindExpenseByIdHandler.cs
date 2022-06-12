using ExpenseService.Api.Infrastructure.DataAccess;
using ExpenseService.Api.Ports.Requests;
using ExpenseService.Api.Ports.Responses;
using Microsoft.EntityFrameworkCore;
using Paramore.Darker;

namespace ExpenseService.Api.Ports.Handlers;
public class FindExpenseByIdHandler : QueryHandlerAsync<FindExpenseById, FindExpenseResult>
{
   private readonly ExpenseDbContext _uow; 

   public FindExpenseByIdHandler(ExpenseDbContext uow)
   {
     _uow = uow; 
   }

   public override async Task<FindExpenseResult> ExecuteAsync(FindExpenseById query, CancellationToken cancellationToken = new CancellationToken())
   {
     return await _uow.Expenses
        .Where(p => p.Id == query.Id) 
        .Select(p => new FindExpenseResult(p))
        .SingleAsync(cancellationToken);
   }
}