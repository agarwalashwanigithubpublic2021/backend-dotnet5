using System.Collections.Generic;
using TariffComparer.Models;

namespace TariffComparer.Business.Interface
{
    public interface ITariffAnnualCostsCalculator
    {
        IEnumerable<TariffDTO> AnnualConsumptionCostsCalculator(double consumptionInKwhPerYear);
    }
}
