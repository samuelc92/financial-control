using FinancialControlReader.Api.Domain.Models;
using MongoDB.Driver;

namespace FinancialControlReader.Api.Database.Repositories;

public class AnnualReportRepository : IAnnualReportRepository
{
  readonly IMongoCollection<AnnualReport> _annualReports;
  public AnnualReportRepository(IMongoClient mongoClient, IDatabaseSettings databaseSettings)
  {
    var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
    _annualReports = database.GetCollection<AnnualReport>(AnnualReport.CollectionName());
  }

  public Task<AnnualReport> Get(int id) => _annualReports.Find(x => x.Id == id).FirstOrDefaultAsync();

  public Task Insert(AnnualReport annualReport) => _annualReports.InsertOneAsync(annualReport);

  public Task Update(AnnualReport annualReport) => _annualReports.ReplaceOneAsync(x => x.Id == annualReport.Id, annualReport);
}