using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinancialControlReader.Api.Domain;
public class AnnualReport
{
  public AnnualReport(string id) 
  {
    Id = id;
  }

	[BsonId]
  public string Id { get; set; } 

  public ICollection<AnnualReportData>? Data { get; set; }

	public static string CollectionName() => "annualReports";
}

public class AnnualReportData
{
  public int Month { get; set; } 

	[BsonRepresentation(BsonType.Double)]
 	public double Total { get; set; }
}