using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDataManager.library
{
    public class ConfigHelper
    {
        public static decimal GetTaxRate()
        {
            string rateText = ConfigurationManager.AppSettings["taxRate"];
            bool IsValidTaxRate = decimal.TryParse(rateText, out decimal output);
            if (IsValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("This tax rate is not set up properly");
            }
            return output;
        }
    }
}
