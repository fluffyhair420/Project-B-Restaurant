using Newtonsoft.Json;


public class ReviewToJson
{
    public void Start()
    {
        SerializeToJson();
    }

    void SerializeToJson()
    {
        var p = new Product("Product A", new DateTime(2021, 12, 28),
        new string[] { "small" });

        var json = JsonConvert.SerializeObject(p);
        Console.WriteLine(json);

        record Product(string Name, DateTime Created, string[] Sizes);

    }
}
