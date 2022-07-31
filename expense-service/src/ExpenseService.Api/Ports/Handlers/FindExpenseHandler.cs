using ExpenseService.Api.Infrastructure.DataAccess;
using ExpenseService.Api.Ports.Requests;
using ExpenseService.Api.Ports.Responses;
using Microsoft.EntityFrameworkCore;
using Paramore.Darker;

namespace ExpenseService.Api.Ports.Handlers;
public class FindExpenseHandler : QueryHandlerAsync<FindExpenseParameter, ICollection<FindExpenseResult>>
{
   private readonly ExpenseDbContext _uow; 

   public FindExpenseHandler(ExpenseDbContext uow)
   {
     _uow = uow; 
   }

   public override async Task<ICollection<FindExpenseResult>> ExecuteAsync(FindExpenseParameter query, CancellationToken cancellationToken = new CancellationToken()) {
     var result = await _uow.Expenses
       .Where(p => query.Id.HasValue ? p.Id == query.Id.Value : true) 
       .Select(p => new FindExpenseResult(p))
       .ToListAsync<FindExpenseResult>(cancellationToken);
      // TODO: Refactor when change database 
      return result.OrderByDescending(p => p.TransactionDate).ToList();
   }
}