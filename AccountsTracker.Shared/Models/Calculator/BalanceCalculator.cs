using AccountsTracker.Models.PersonModels;
using AccountsTracker.Shared.Models.Calculator;
using AccountsTracker.Shared.Settings;
using Microsoft.Extensions.Options;

namespace AccountsTracker.Models.Calculator
{
    public class BalanceCalculator
    {
        private readonly SavingPercentages _savingPercentages;

        public List<PersonalAccount> HouseholdIncomes { get; set; }

        private decimal TotalIncome { get; set; }


        public BalanceCalculator(List<PersonalAccount> householdIncomes, SavingPercentages savingPercentages)
        {
            HouseholdIncomes = householdIncomes;
            _savingPercentages = savingPercentages;
            HouseholdIncomes.ForEach(i => TotalIncome += i.IncomeAfterPensionDeduction);
            CalcPercentOfIncome();
            CalculateHouseholdBillsAmount();
            CalculateDisposableIncome();
            CalcMoneyForSavings();

            householdIncomes = HouseholdIncomes;

        }

        private void CalcPercentOfIncome()
        {
            HouseholdIncomes.ForEach(i => i.PercentageOfTotalIncome = (double)(i.IncomeAfterPensionDeduction / TotalIncome));
        }

        private void CalculateHouseholdBillsAmount()
        {
            HouseholdIncomes.ForEach(i => i.AmountToPayHouseholdBills = Math.Round(((decimal)i.PercentageOfTotalIncome * i.SharedTransfers.HouseholdBills)));
        }

        private void CalculateDisposableIncome()
        {
            foreach(var personalAccount in HouseholdIncomes)
            {
                var balanceBeforeDisposableIncome = personalAccount.IncomeAfterPensionDeduction -
                                                    personalAccount.AmountToPayHouseholdBills -
                                                    personalAccount.PersonalTransfers.JointGiftPot -
                                                    personalAccount.PersonalTransfers.IndividualGiftPot;

                if (balanceBeforeDisposableIncome < 270 && balanceBeforeDisposableIncome > 0)
                {
                    personalAccount.PersonalTransfers.DisposableIncome = balanceBeforeDisposableIncome;
                }else if(balanceBeforeDisposableIncome <= 0)
                {
                    personalAccount.PersonalTransfers.DisposableIncome = 0;
                }
            }
        }

        private void CalcMoneyForSavings()
        {
            foreach (var personalAccount in HouseholdIncomes)
            {
                var remainingBalance = personalAccount.IncomeAfterPensionDeduction;
                remainingBalance -= personalAccount.AmountToPayHouseholdBills;

                if (personalAccount.Person.PersonalOutgoings != null)
                {
                    personalAccount.Person.PersonalOutgoings.ForEach(i => remainingBalance -= i.Amount);
                }

                remainingBalance -= personalAccount.PersonalTransfers.JointGiftPot;
                remainingBalance -= personalAccount.PersonalTransfers.IndividualGiftPot;
                remainingBalance -= personalAccount.PersonalTransfers.DisposableIncome;

                personalAccount.SavingAmounts = new SavingAmounts(remainingBalance, _savingPercentages);
            }
            


        }
    }
}
