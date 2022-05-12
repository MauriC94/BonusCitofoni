
using BonusCitofoni;


Console.WriteLine("Hello, Bonus citofoni!");
var client = new IntercomRequestor();
var results = await client.Run();

Console.ReadKey();
