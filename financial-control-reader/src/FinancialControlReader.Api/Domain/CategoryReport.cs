using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinancialControlReader.Api.Domain;

public class CategoryReport 
{
	[BsonId]
	public string Id { get; set; } 
 	public ICollection<Resume> Resume { get; set; }   

	public static string CollectionName() => "categoryReports";
}

public class Resume
{
	public string Category { get; set; }

	[BsonRepresentation(BsonType.Double)]
 	public double Total { get; set; }
}