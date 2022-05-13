
using BonusCitofoni;


Console.WriteLine("Hello, Bonus citofoni!");
var client = new IntercomRequestor();
var results = await client.Run();
foreach (var result in results)
    Console.WriteLine($"{result.Request.Name} --> {result.Reason}");

Console.ReadKey();
