using System;
using ATMConsoleAppMain;

public class CardHolder
{
    private string CardNumber { get; set; }
    private int CardPin { get; set; }
    private string FullName { get; set; }
    private double UserBalanace { get; set; }
    private bool IsBlocked { get; set; }
    public CardHolder(string cardNumber, int cardPin, string fullName, double userBalanace)
    {
        CardNumber = cardNumber;
        CardPin = cardPin;
        FullName = fullName;
        UserBalanace = userBalanace;
        IsBlocked = false;
    }

    public int GetCardPin()
    { return CardPin; }

    public double GetUserBalance()
    {
        return UserBalanace;
    }
    public string GetCardNumber()
    { return CardNumber; }

    public string GetFullName() { return FullName; }

    public bool GetIsBlocked()
    {
        return IsBlocked;
    }

    public void SetIsBlocked(bool option)
    {
        IsBlocked = option;
    }

    LanguageOptions languageOptions = new LanguageOptions();
    public void deposit(CardHolder CurrentUser, string language)
    {
        try
        {
            languageOptions.LikeToDeposit(language);
            double deposit = Math.Abs(Double.Parse(Console.ReadLine()));
            CurrentUser.UserBalanace = deposit + CurrentUser.UserBalanace;
            languageOptions.ThanksToDeposit(language, CurrentUser.UserBalanace);
        }
        catch
        {
            languageOptions.exceptionMessage(language);

        }


    }

    public void withdraw(CardHolder CurrentUser, string language)
    {
        try
        {
            languageOptions.LikeToWithdraw(language);
            double withdrawal = Math.Abs(Double.Parse(Console.ReadLine()));
            if (withdrawal > CurrentUser.UserBalanace)
            {
                languageOptions.NotEnoughBalance(language);
            }
            else
            {
                double NewBalance = CurrentUser.UserBalanace - withdrawal;
                CurrentUser.UserBalanace = NewBalance;
                languageOptions.ThanksForWithdrawing(language, withdrawal);
            }
        }
        catch
        {
            languageOptions.exceptionMessage(language);
            // Console.WriteLine("Something went wrong. Please try again");
        }


    }

    public void transfer(CardHolder CurrentUser, CardHolder TransferUser, string language)
    {
        try
        {
            languageOptions.LikeToTransfer(language);
            double transfer = Math.Abs(Double.Parse(Console.ReadLine()));
            if (transfer > CurrentUser.UserBalanace)
            {
                languageOptions.NotEnoughBalance(language);
            }
            else
            {

                double NewBalance = CurrentUser.UserBalanace - transfer;
                double TransferUserNewBalance = TransferUser.UserBalanace + transfer;

                CurrentUser.UserBalanace = NewBalance;
                TransferUser.UserBalanace = TransferUserNewBalance;
                languageOptions.ThankForTransfer(language, transfer, TransferUser.FullName);
                languageOptions.DisplayBalance(language, CurrentUser.UserBalanace);
            }
        }
        catch
        {
            languageOptions.exceptionMessage(language);
    
        }

    }
}
