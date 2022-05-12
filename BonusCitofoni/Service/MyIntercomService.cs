using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusCitofoni.Service
{
    public class MyIntercomService : CoreIntercomBonusService, IntercomBonusService
    {
        private decimal _richMaxAmount;
        //private Dictionary<string, BonusRequest> _ibanRequest = new Dictionary<string, BonusRequest>();
        private ConcurrentDictionary<string,BonusRequest> _ibanRequest = new ConcurrentDictionary<string, BonusRequest>();
        public MyIntercomService(decimal budget, decimal maxAmount, int maxIR, decimal amountMaxiR) : base(budget, maxAmount, maxIR)
        {
            this._richMaxAmount = amountMaxiR;
        }
        
        protected override bool CheckBudget(BonusRequest request)
        {
            if(_budget <= 0)
            {
                return false;
            }

            lock (this)
            {
                if (request.AmountRequested > _budget)
                {
                    request.AmountRequested = _budget;
                    _budget = 0;
                }
                else if (request.AmountRequested <= _budget)
                {
                    _budget -= request.AmountRequested;
                }
            }
            return true;

        }

        protected override bool VerifyIR(BonusRequest request) => CheckIbanRequest(request);

        protected override bool VerifyAmount(BonusRequest request)
        {
            _ibanRequest.TryAdd(request.IBAN, request);

            if (request.IR < this._maxIR)
            {
                return request.AmountRequested < this._maxAmount;

            }
            else
            {
                return request.AmountRequested < _richMaxAmount;
            }       
        }

        protected bool CheckIbanRequest(BonusRequest request)
        {
            if (!_ibanRequest.ContainsKey(request.IBAN))
                return true;
            return false;
        }
    }
}
