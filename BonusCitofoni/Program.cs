
using BonusCitofoni;


Console.WriteLine("Hello, Bonus citofoni!");
var client = new IntercomRequestor();
var results = await client.Run();
foreach(var outcome in results)
    Console.WriteLine($"{outcome.Request.Name}--->{outcome.Reason}");

Console.ReadKey();
