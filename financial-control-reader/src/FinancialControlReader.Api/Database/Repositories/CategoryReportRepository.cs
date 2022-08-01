using FinancialControlReader.Api.Domain;
using MongoDB.Driver;

namespace FinancialControlReader.Api.Database.Repositories;

public class CategoryReportRepository : ICategoryReportRepository
{
	private readonly IMongoCollection<CategoryReport> _categoryReports;

  public CategoryReportRepository(IMongoClient mongoClient, IDatabaseSettings databaseSettings)
  {
    var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
    _categoryReports = database.GetCollection<CategoryReport>(CategoryReport.CollectionName());
  }

  public Task<CategoryReport> GetCategoryReport(string id) =>
	  _categoryReports.Find(x => true).FirstOrDefaultAsync();

  public Task Insert(CategoryReport categoryReport) => _categoryReports.InsertOneAsync(categoryReport);

  public Task Update(CategoryReport categoryReport) =>
	  _categoryReports.ReplaceOneAsync(x => x.Id == categoryReport.Id, categoryReport);
}