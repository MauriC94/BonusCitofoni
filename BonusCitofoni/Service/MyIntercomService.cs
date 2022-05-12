using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusCitofoni.Service
{
    public class MyIntercomService : CoreIntercomBonusService
    {
        private decimal _richMaxAmount;
        private Dictionary<string, BonusRequest> _ibanRequest = new Dictionary<string, BonusRequest>();
        public MyIntercomService(decimal budget, decimal maxAmount, int maxIR, decimal amountMaxiR) : base(budget, maxAmount, maxIR)
        {
            this._richMaxAmount = amountMaxiR;
        }

        protected override bool CheckBudget(BonusRequest request)
        {
            //bool result = false;    
            //if (request.IR < this._maxIR)
            //{
            //    result = base.CheckBudget(request);
            //}     
        }

        protected virtual void VerifyIR(BonusRequest request)
        {

        }

        protected virtual bool VerifyAmount(BonusRequest request)
        {
            if (CheckIbanRequest(request))
            {
                _ibanRequest.Add(request.IBAN, request);
                if (request.IR < this._maxIR)
                {
                    return request.AmountRequested < this._maxAmount;

                }
                else
                {
                    return request.AmountRequested < _richMaxAmount;
                }
            }
            else
            {
                return false;
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
