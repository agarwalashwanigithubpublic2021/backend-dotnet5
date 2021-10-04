using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TariffComparer.Business.Interface;
using TariffComparer.Models;

namespace TariffComparerWebAPI.Controllers
{
    public class TariffsComparerController  : BaseApiController
    {
        private readonly ITariffAnnualCostsCalculator _tariffAnnualCostsCalculator;

        public TariffsComparerController(ITariffAnnualCostsCalculator tariffAnnualCostsCalculator)
        {
            _tariffAnnualCostsCalculator = tariffAnnualCostsCalculator;
        }

        [HttpGet("compare-tariff/{consumption}")]
        public ActionResult<IEnumerable<TariffDTO>> GetTariffsByAnnualConsumption(double consumption)
        {
            if (consumption <= 0)
                return BadRequest("Consumption should be greater than zero(0)");

            IEnumerable<TariffDTO> list = _tariffAnnualCostsCalculator.AnnualConsumptionCostsCalculator(consumption);

            return Ok(list.OrderBy(tariff => tariff.AnnualCosts).ToArray());
        }
    }
}
