using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinancialControlReader.Api.Domain.Models;
public class AnnualReport
{
  public AnnualReport(int id, AnnualReportResume data) 
  {
    Id = id;
    Resume = new List<AnnualReportResume> { data };
  }

	[BsonId]
  public int Id { get; set; } 

  public ICollection<AnnualReportResume>? Resume { get; set; }

	public static string CollectionName() => "annualReports";

  public AnnualReportResume? GetAnnualReportResumeByMonth(int month) =>
    Resume?.Where(data => data.Month == month).FirstOrDefault();
}

public class AnnualReportResume
{
  public int Month { get; set; } 

	[BsonRepresentation(BsonType.Double)]
 	public double Total { get; set; }
}