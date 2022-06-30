using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE
{
    abstract class Expense
    {
        double gIncome;
        double tax;
        double rent;
        List<double> expenses = new List<double>();

        public Expense(double gIncome, double tax, double rent, List<double> expenses)
        {
            this.gIncome = gIncome;
            this.tax = tax;
            this.rent = rent;
            this.expenses = expenses;
        }

        //Method that calculates money after deductions if user chose to buy property
        public static double AvailableMoneyWithRepayment(double income, double tax, double totalExpenses, double repayment)
        {
            double amount = income - (tax + totalExpenses + repayment);
            return amount;
        }

        //Method that calculates money after deductions if user chose to rent accommodation
        public static double AvailableMoneyWithRent(double income, double tax, double totalExpenses, double rent)
        {
            double amount = income - (tax + totalExpenses + rent); 
            return amount;
        }
        //Method that calculates money after deductions if user chose to buy a car
        public static double AvailableMoneyWithCar(double income, double tax, double totalExpenses, double monthlyCostCar)
        {
            double amount = income - (tax + totalExpenses + monthlyCostCar);
            return amount;
        }
        //Method that calculates money after deduction if the user chose to save
        public static double AvailableMoneyWithSaving(double income, double tax, double totalExpenses, double monthlyCostCar)
        {
            double amount = income - (tax + totalExpenses + monthlyCostCar);
            return amount;
        }
    }
}
