using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG_POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void notifyExceed();
        private notifyExceed notifyExceedFunction;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxSelection.SelectedIndex == 0) //If user selects "Renting Accommodation"
            {
                //details needed for RENT will be visible to the user, user will only be able to enter rental amount
                canvasRenting.Visibility = Visibility.Visible;
                //HomeLoan details will be hidden so user doesnt enter information corresponding to buying a property
                canvasBuying.Visibility = Visibility.Collapsed;
                canvasVehicle.Visibility = Visibility.Collapsed;
                canvasSaving.Visibility = Visibility.Collapsed;
            }
            else if(comboBoxSelection.SelectedIndex == 1) //If user selects "Buying a property"
            {
                //details needed for Home Loan will be visible to the user
                canvasBuying.Visibility = Visibility.Visible;
                //Renting details will be hidden so user doesnt enter information corresponding to renting accommodation
                canvasRenting.Visibility = Visibility.Collapsed;
                canvasVehicle.Visibility = Visibility.Collapsed;
                canvasSaving.Visibility = Visibility.Collapsed;
            }
            else if(comboBoxSelection.SelectedIndex == 2) //if user selects "Buying a Car"
            {
                //details needed for total monthly cost will be visible to the user
                canvasVehicle.Visibility = Visibility.Visible;
                //Renting details will be hidden so user doesnt enter information corresponding to renting accommodation
                canvasRenting.Visibility = Visibility.Collapsed;
                canvasBuying.Visibility = Visibility.Collapsed;
                canvasSaving.Visibility = Visibility.Collapsed;
            }
            else if(comboBoxSelection.SelectedIndex == 3) // if user selects to "Save"
            {
                canvasSaving.Visibility = Visibility.Visible;
                canvasRenting.Visibility = Visibility.Collapsed;
                canvasBuying.Visibility = Visibility.Collapsed;
                canvasVehicle.Visibility = Visibility.Collapsed;
            }
        }

        private void buttonDone_Click(object sender, RoutedEventArgs e)
        {
            //Declarations
            double income;
            double tax;
            double totalExpenses;
            // if user selected "Renting Accommodation"
            double rent;
            // if user selected "Buying a Property"
            double purchasePrice;
            double deposit;
            double interestRate;
            int months;
            double insurance; // for buying a vehicle
            double thirdIncome; // 1/3 of income
            double sFIncome; //75% of income
            double availableMoney;
            double repayment;
            double finalExpenses;

            List<double> expenses= new List<double>(); // generic list to store expenses

            //Assignment of values
            income = Convert.ToDouble(textBoxIncome.Text);
            tax = Convert.ToDouble(textBoxTax.Text);
            //storing expenses in list
            expenses.Add(Convert.ToDouble(textBoxGroceries.Text));
            expenses.Add(Convert.ToDouble(textBoxWaterLights.Text));
            expenses.Add(Convert.ToDouble(textBoxTravelCosts.Text));
            expenses.Add(Convert.ToDouble(textBoxPhone.Text));
            expenses.Add(Convert.ToDouble(textBoxOtherExpenses.Text));

            totalExpenses = expenses.Sum(); //adds all values in the expeneses list

            switch(comboBoxSelection.SelectedIndex)
            {
                //if user selected "renting accommodation"
                case 0:
                    //assigns value for montly rental amount
                    rent = Convert.ToDouble(textBoxRent.Text);

                    //calls AvailableMoneyWithRent() method 
                    availableMoney = Expense.AvailableMoneyWithRent(income, tax, totalExpenses, rent);
                    MessageBox.Show("Available money for the month after deductions is: R" + Math.Round(availableMoney));

                    //if total expense > 75% of userse gross income
                    double sF = income * 0.75;
                    sFIncome = sF;
                    finalExpenses = totalExpenses + tax + rent;
                    if (finalExpenses > sFIncome)
                    {
                        notifyExceedFunction = MyNotifyExceedFunction;
                        MyNotifyExceedFunction();
                    }

                    break;
                //if user selected "renting accommodation"
                case 1:
                    //assigns values needed to calculate monthly loan repayment
                    purchasePrice = Convert.ToDouble(textBoxPrice.Text);
                    deposit = Convert.ToDouble(textBoxDeposit.Text);
                    interestRate = Convert.ToDouble(textBoxInterestRate.Text);
                    months = Convert.ToInt32(textBoxMonths.Text);

                    //calls HomeLoanRepayment method that calculates the monthly home loan repayment
                    repayment = HomeLoan.HomeLoanRepayment(purchasePrice, deposit, interestRate, months);

                    MessageBox.Show("Monthly Home Loan Repayment amount is: R" + Math.Round(repayment));

                    //if the monthly home loan repayment is more than a third of user's gross monthly income's if statement
                    double v = income * 0.33333333333;
                    thirdIncome = v;
                    if (Math.Round(repayment) > thirdIncome)
                    {
                        MessageBox.Show("ALERT! \nThe approval of the home loan is unlikely");
                    }
                    else
                    {
                        //calls AvailableMoneyWithRent method which calculates available money after deductions
                        availableMoney = HomeLoan.AvailableMoneyWithRepayment(income, tax, totalExpenses, repayment);

                        MessageBox.Show("Available money for the month after deductions is: R" + Math.Round(availableMoney, 2));
                    }

                    //if total expense > 75% of userse gross income
                    sF = income * 0.75;
                    sFIncome = sF;
                    finalExpenses = totalExpenses + tax + repayment;
                    if (finalExpenses > sFIncome)
                    {
                        notifyExceedFunction = MyNotifyExceedFunction;
                        MyNotifyExceedFunction();
                    }
                    break;
                //if user selected "buying a vehicle"
                case 2:
                    double monthlyCost;
                    purchasePrice = Convert.ToDouble(textBoxVehiclePrice.Text);
                    deposit = Convert.ToDouble(textBoxVehicleDeposit.Text);
                    interestRate = Convert.ToDouble(textBoxVehicleInterestRate.Text);
                    months = Convert.ToInt32(60); //5 years * 12
                    insurance = Convert.ToDouble(textBoxInsurance.Text);

                    repayment = Vehicle.HomeLoanRepayment(purchasePrice, deposit, interestRate, months);

                    monthlyCost = Vehicle.GetMonthlyCost(insurance, repayment);

                    MessageBox.Show("The total monthly cost of buying a car is: R" + monthlyCost);

                    availableMoney = Vehicle.AvailableMoneyWithCar(income, tax, totalExpenses, monthlyCost);
                    MessageBox.Show("Available money for the month after deductions is: R" + Math.Round(availableMoney, 2));


                    //if total expense > 75% of userse gross income
                    sF = income * 0.75;
                    sFIncome = sF;
                    finalExpenses = totalExpenses + tax + monthlyCost;
                    if(finalExpenses > sFIncome)
                    {
                        notifyExceedFunction = MyNotifyExceedFunction;
                        MyNotifyExceedFunction();
                    }
                    
                    break;
                //if user selected "Save"
                case 3:
                    /* FUTURE ANUITY FORUMULA
                     * Fv = C x ( ((1+i)^n - 1))/ (i) )
                     * 
                     *              let smallBracket
                     * 
                     * Fv = Future value
                     * C = cash flow per peried
                     * i = interest rate
                     * n = years
                     * 
                     * 
                     */
                    // building of future annuity
                    // Declarations
                    double specifiedAmount; // Fv
                    double yearlyAmount; // C
                    double monthlyAmount; // C / 12
                    double bigBracket; // = (((1+i)^n) - 1 / (i))
                    double smallBracket; // = (1+i) 
                    double smallBracketPowered; // = (1+i)^n
                    double givenIntrestRate = 7; // = i
                    double years = 5; // = n

                    //Allocating values
                    specifiedAmount = Convert.ToDouble(textBoxSpecifiedAmount.Text);
                    years = Convert.ToDouble(textBoxYears.Text);

                    smallBracket = (1 + givenIntrestRate);
                    smallBracketPowered = Math.Pow(smallBracket, years);

                    bigBracket = ((smallBracketPowered) - 1 / (givenIntrestRate));

                    //Future Annuity 
                    yearlyAmount = specifiedAmount / bigBracket;
                    monthlyAmount = yearlyAmount / 12;

                    MessageBox.Show("The total monthly saving amount is: R" + yearlyAmount);

                    availableMoney = Vehicle.AvailableMoneyWithSaving(income, tax, totalExpenses, monthlyAmount);

                    MessageBox.Show("Available money for the month after deductions is: R" + Math.Round(availableMoney, 2));
                    break;
            }

            //Displaying expenses in descending order
            expenses = expenses.OrderBy(x => -x).ToList();
            MessageBox.Show("EXPENSES \n" +
                            String.Join("\n", expenses));
        }

        private void MyNotifyExceedFunction() // For Delegate function
        {
            MessageBox.Show("Your total Expenses exceed 75% of your gross income before deductions!", "NOTICE!");
        }
        
    }
}
