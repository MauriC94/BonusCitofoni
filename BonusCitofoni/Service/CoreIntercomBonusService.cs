namespace BonusCitofoni
{
    /// <summary>
    /// DO NOT CHANGE THIS CODE
    /// </summary>
    public  class CoreIntercomBonusService : IntercomBonusService
    {
        protected decimal _budget;
        protected decimal _maxAmount;
        protected int _maxIR;

        public CoreIntercomBonusService(decimal budget,decimal maxAmount, int maxIR)
        {
            _budget = budget;
            _maxIR = maxIR;
            _maxAmount = maxAmount; 
        }

        public  BonusResponse ProcessRequest(BonusRequest request)
        {

            try
            {
                VerifyIR(request);
                VerifyAmount(request);
                CheckBudget(request);
                return new BonusResponse() { Request = request, Accepted = true };
            }
            catch (Exception e)
            {
                return new BonusResponse() { Request = request, Accepted = false, Reason = e.Message };

            }

            //if(!VerifyIR(request)) return new BonusResponse() { Request = request, Accepted = false , Reason="Sei troppo ricco!"};
            //if (!VerifyAmount(request)) return new BonusResponse() { Request = request, Accepted = false, Reason = "Vuoi troppi soldi!" };
            //if (!CheckBudget(request)) return new BonusResponse() { Request = request, Accepted = false, Reason = "Finito il budget" };
            //return new BonusResponse() { Request = request, Accepted = true };



        }

        protected virtual void CheckBudget(BonusRequest request)
        {
             bool outcome = true;
            lock (this)
            {
                _budget -= request.AmountRequested;
                if (_budget < 0)
                {
                    _budget += request.AmountRequested;
                    outcome= false;

                }
            }
            if (!outcome) throw new Exception("Finito il budget");
            //return outcome;

        }
        //protected virtual bool VerifyIR(BonusRequest request) => request.IR < _maxIR;
        //protected virtual bool VerifyAmount(BonusRequest request) => request.AmountRequested < _maxAmount;
        protected virtual void VerifyIR(BonusRequest request)
        {
            if (request.IR > _maxIR) throw new Exception("I soldi li devi dare tu a me");
        }

        protected virtual void VerifyAmount(BonusRequest request)
        {
            if (request.AmountRequested > _maxAmount) throw new Exception("Oh ma quanto costa sto citofono!!");
        }

    }
}
