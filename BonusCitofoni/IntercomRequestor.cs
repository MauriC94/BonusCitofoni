using BonusCitofoni.Service;

namespace BonusCitofoni
{
    public class IntercomRequestor
    {

        IntercomBonusService intercomBonusService = new MyIntercomService(200000, 1000, 3, 200);
        

        public async Task<IEnumerable<BonusResponse>> Run()
        {
            IEnumerable<BonusRequest> bonusRequests = CreateManyRequests();
            return await ProcessRequests(bonusRequests);

        }


        async Task<IEnumerable<BonusResponse>> ProcessRequests(IEnumerable<BonusRequest> bonusRequests, int parallelism = 50)
        {
            List<BonusResponse> responses = new List<BonusResponse>();
            for (int index = 0; index < bonusRequests.Count(); index += parallelism)
            {
                var concurrentTasks = bonusRequests.Skip(index).Take(parallelism).Select(req => Task.Run(() => intercomBonusService.ProcessRequest(req)));
                var results = await Task.WhenAll(concurrentTasks);
                responses.AddRange(results);
            }
            return responses;

        }

        IEnumerable<BonusRequest> CreateManyRequests()
        {
            var theList = new List<BonusRequest>();
            var requests = File.ReadAllLines("citofoni.csv");
            foreach (var request in requests)
            {
                var values = request.Split(',');
                theList.Add(new BonusRequest()
                {
                    Name = $"{values[0]} {values[1]}",
                    IR = int.Parse(values[2]),
                    IBAN = values[3],
                    AmountRequested = decimal.Parse(values[4])
                });

            }
            return theList;
        }


    }
}
