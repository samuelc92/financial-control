using ExpenseService.Api.Infrastructure.DataAccess;
using ExpenseService.Api.Ports.Requests;
using ExpenseService.Api.Ports.Responses;
using Microsoft.EntityFrameworkCore;
using Paramore.Darker;

namespace ExpenseService.Api.Ports.Handlers;
public class FindExpenseByDescriptionHandler : QueryHandlerAsync<FindExpenseByDescription, FindExpenseResult>
{
   private readonly ExpenseDbContext _uow; 

   public FindExpenseByDescriptionHandler(ExpenseDbContext uow)
   {
     _uow = uow; 
   }

   public override async Task<FindExpenseResult> ExecuteAsync(FindExpenseByDescription query, CancellationToken cancellationToken = new CancellationToken())
   {
     return await _uow.Expenses
        .Where(p => p.Description == query.Description) 
        .Select(p => new FindExpenseResult(p))
        .SingleAsync(cancellationToken);
   }
}