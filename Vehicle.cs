using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE
{
    class Vehicle : HomeLoan
    {
        public double purchasePrice;
        public double deposit;
        public double interestRate;
        public double months;
        public double repayment;
        public double insurance;

        public Vehicle(double purchasePrice, double deposit, double interestRate, double months, double repayment, double insurance,
                       double vPrice, double vDeposit, double vInterestRate, int vMonths, double vRepayment, 
                       double vIncome, double vTax, double vRent, List<double> expenses)
            : base(vPrice, vDeposit, vInterestRate, vMonths, vRepayment, vIncome, vTax, vRent, expenses)
        {
            this.purchasePrice = purchasePrice;
            this.deposit = deposit;
            this.interestRate = interestRate;
            this.months = months;
            this.repayment = repayment;
            this.insurance = insurance;
        }

        public static double GetMonthlyCost(double insurance, double repayment)
        {
            double montlyCost = insurance + repayment;
            return montlyCost;
        }
    }
}
