// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


parseJSONAndGetDetails();


void parseJSONAndGetDetails()
{
    var path = @"D:/JSONDATA/PolicyData.json";
    if (!File.Exists(path)) return;

    using StreamReader reader = new StreamReader(path);
    var json = reader.ReadToEnd();

    ParseToJsonObject(json);

    ParseToClassObject(json);


    //Serialization Sample (Class Object to Json)
    //Order order = new Order()
    //{
    //    Id = 100,
    //    OrderNumber = "OR-100",
    //    Price = 39.99m,
    //    OrderDate = DateTime.Now
    //};
    //var jsonOrder = System.Text.Json.JsonSerializer.Serialize(order);
    //Console.WriteLine(jsonOrder);

}

//JSON using JObject.Parse
void ParseToJsonObject(string json)
{   
    try
    {
        var jObj = JObject.Parse(json);

        Console.WriteLine("\nPolicy Details with Json Object");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Policy Number: {jObj["PolicyNumber"]}"  );
        Console.WriteLine($"Policy Type: {jObj["PolicyType"]}" );
        Console.WriteLine($"Effective Date: {jObj["EffectiveDt"]}" );
        Console.WriteLine($"Expiration Date: {jObj["ExpirationDt"]}" );
        Console.WriteLine($"Policy Premium: {jObj["Premium"]}");

        if (jObj["ClaimDetails"] != null)
        {
            Console.WriteLine("\nClaim Details:");
            Console.WriteLine("----------------");

            Console.WriteLine(" ID \t Date\t\t Amount");
            foreach (var claims in jObj["ClaimDetails"])
            {
                Console.Write($" {claims["claimid"]}");
                Console.Write($"\t {claims["claimDt"]}");
                Console.WriteLine($"\t {claims["claimAmt"]}");
            }
        }
    }
    catch (Exception ex) { }
    finally { }
}


//JSON using JsonConverter
void ParseToClassObject(string json)
{
    try
    {
        PolicyDetails obj = JsonConvert.DeserializeObject<PolicyDetails>(json);

        Console.WriteLine("\nPolicy Details with class Object");
        Console.WriteLine("-------------------------");

        if (obj != null)
        {
            Console.WriteLine($"Policy Number: {obj.PolicyNumber}"  );
            Console.WriteLine($"Policy Type: {obj.PolicyType}" );
            Console.WriteLine($"Effective Date: {obj.EffectiveDt}" );
            Console.WriteLine($"Expiration Date: {obj.ExpirationDt}");
            Console.WriteLine($"Policy Premium: {obj.Premium}" );
            if (obj.claimDetails != null)
            {
                Console.WriteLine("\nClaim Details:");
                Console.WriteLine("----------------");

                Console.WriteLine(" ID \t Date \t\t\t Amount");
                foreach (var claims in obj.claimDetails)
                {
                    Console.Write($" {claims.ClaimID}");
                    Console.Write($"\t {claims.ClaimDate}");
                    Console.WriteLine($"\t {claims.ClaimAmount}");
                }
            }
        }
    }
    catch (Exception ex)
    { }
    finally { }

}

public partial class PolicyDetails
{
    [JsonProperty("PolicyNumber")]
    public string PolicyNumber { get; set; }

    [JsonProperty("PolicyType")]
    public string PolicyType { get; set; }

    [JsonProperty("EffectiveDt")]
    public DateTime EffectiveDt { get; set; }

    [JsonProperty("ExpirationDt")]
    public DateTime ExpirationDt { get; set; }
    
    [JsonProperty("Premium")]
    public decimal Premium { get; set; }

    [JsonProperty("ClaimDetails")]
    public ClaimDetails[]? claimDetails { get; set; }    

}
public partial class ClaimDetails
{
    [JsonProperty("claimid")]
    public int ClaimID { get; set; }

    [JsonProperty("claimDt")]
    public DateTime ClaimDate { get; set; }

    [JsonProperty("claimAmt")]
    public decimal ClaimAmount { get; set; }
}




public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public decimal Price { get; set; }
    public DateTime OrderDate { get; set; }
}