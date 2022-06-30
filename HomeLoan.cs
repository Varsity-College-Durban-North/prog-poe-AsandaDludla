using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE
{
    class HomeLoan : Expense
    {
        public double purchasePrice;
        public double deposit;
        public double interestRate;
        public int months;
        public double repayment;

        public HomeLoan(double purchasePrice, double deposit, double interestRate, int months, double repayment,
                        double income, double tax, double rent, List<double> expenses)
                    : base(income, tax, rent, expenses)
        {
            this.purchasePrice = purchasePrice;
            this.deposit = deposit;
            this.interestRate = interestRate;
            this.months = months;
            this.repayment = repayment;
        }

        public static double HomeLoanRepayment(double purchasePrice, double deposit, double interestRate, int months)
        {
            //Declarations for simple interest formula + repayment calculation
            double P, A, i, n;
            double repayment;

            //Assigning values to variables need for simple interest formula
            P = purchasePrice - deposit;
            i = interestRate / 100;
            n = months / 12;

            //Final calculations
            A = P * (1 + (i * n));
            repayment = A / months;

            return repayment;
        }
    }
}
