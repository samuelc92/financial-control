using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinancialControlReader.Api.Domain.Models;
public class AnnualReport
{
  public AnnualReport(int id, AnnualReportData data) 
  {
    Id = id;
    Data = new List<AnnualReportData> { data };
  }

	[BsonId]
  public int Id { get; set; } 

  public ICollection<AnnualReportData>? Data { get; set; }

	public static string CollectionName() => "annualReports";
}

public class AnnualReportData
{
  public int Month { get; set; } 

	[BsonRepresentation(BsonType.Double)]
 	public double Total { get; set; }
}