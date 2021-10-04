using System;
using System.Collections.Generic;
using TariffComparer.Business.Interface;
using TariffComparer.Models;

namespace TariffComparer.Business.Service
{
    public class TariffAnnualCostsCalculator : ITariffAnnualCostsCalculator
    {
        const double monthlyBaseCosts = 5 * 12;
        const double consumptionCostsPerKWh = 0.22;

        public IEnumerable<TariffDTO> AnnualConsumptionCostsCalculator(double consumptionKwhPerYear)
        {
            List<TariffDTO> tariffs = new()
            {
                new TariffDTO() { AnnualCosts = BasicElectricityTariffAnnualCostsCalculator(consumptionKwhPerYear), TariffName = "basic electricity tariff" },
                new TariffDTO() { AnnualCosts = PackagedTariffAnnualCostsCalculator(consumptionKwhPerYear), TariffName = "Packaged tariff" }
            };

            return tariffs;
        }       

        private double BasicElectricityTariffAnnualCostsCalculator(double consumptionKwhPerYear)
        {
            if (consumptionKwhPerYear <= 0)
                throw new ArgumentException("Consumption should be greater than zero(0)");

            return monthlyBaseCosts + consumptionCostsPerKWh * consumptionKwhPerYear;
        }

        private double PackagedTariffAnnualCostsCalculator(double consumptionKwhPerYear)
        {
            const double baseCost = 800;
            const double anualBaseCostLimit = 4000;
            const double anualConsumptionCostsPerKWh = 0.30;

            if (consumptionKwhPerYear < 0)
                throw new ArgumentException("Consumption should be greater than zero(0)");

            if (consumptionKwhPerYear < anualBaseCostLimit)
            {
                return baseCost;
            }

            return baseCost + ((consumptionKwhPerYear - anualBaseCostLimit) * anualConsumptionCostsPerKWh);
        }
    }
}
