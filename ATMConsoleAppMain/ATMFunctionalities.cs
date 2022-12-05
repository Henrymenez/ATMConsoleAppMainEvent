namespace ATMConsoleAppMain
{
    public class ATMFunctionalities
    {
      
        event Action<string> BlockCardHolderNotification;
        event Action<string> BlockCardHolder;


        public void printOptions(string language)
        {

            if (language == "1")
            {
                Console.WriteLine("Please choose from one of these following options..!");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Show Balance");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Exit");
            }
            else if (language == "2")
            {
                Console.WriteLine("Biko horo n'otu n'ime nhoro ndi a..!");
                Console.WriteLine("1. Nkwunye ego");
                Console.WriteLine("2. Wepu");
                Console.WriteLine("3. Gosi Balance");
                Console.WriteLine("4. Nyefee Ego");
                Console.WriteLine("5. Uzo opupu");
            }
            else if (language == "3")
            {
                Console.WriteLine("Abeg Wetin you go wan do..!");
                Console.WriteLine("1. put money");
                Console.WriteLine("2.  commot money");
                Console.WriteLine("3. you wan see balance");
                Console.WriteLine("4. send money give person");
                Console.WriteLine("5. commot app");
            }
            else
            {
                Console.WriteLine("Please choose from one of these following options..!");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Show Balance");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Exit");
            }
        }

        public void Run()
        {
            Console.Title = "ATM";
            List<CardHolder> CardHolders = new List<CardHolder>();
            CardHolders.Add(new CardHolder("45327722", 1234, "John Jonas", 150.32));
            CardHolders.Add(new CardHolder("7657725", 4254, "Matt Smith", 700.12));
            CardHolders.Add(new CardHolder("89327711", 2458, "Shawn Wallas", 54.32));
            AddBlockCardHolderNotification(HandleBlockCardHolderNotification);
            AddBlockCardHolder(HandleBlockCardHolder);
            Console.WriteLine("Select Language Option");
            Console.WriteLine("1. English");
            Console.WriteLine("2. Igbo");
            Console.WriteLine("3. pidgin");
            string Language = Console.ReadLine();

            LanguageOptions languageOptions = new LanguageOptions();
            languageOptions.welcome(Language);


            string debitCardNumber = "";
            CardHolder CurrentUser;
       

            while (true)
            {
                try
                {
                    debitCardNumber = Console.ReadLine();
                    //check against db
                    CurrentUser = CardHolders.FirstOrDefault(a => a.GetCardNumber() == debitCardNumber);
                    if (CurrentUser != null)
                    {
                        break;
                    }
                    else
                    {
                        languageOptions.cardNotRecognized(Language);
                    }
                }
                catch
                {
                    languageOptions.cardNotRecognized(Language);
                }
            }
            languageOptions.InsertPinNumber(Language);
            int userPin = 0;
                 int cardPinTrials = 1;
            while (true)
            {
                try
                {
                    userPin = int.Parse(Console.ReadLine());
                    //check against db


                    if(cardPinTrials == 2)
                    {
             OnBlockCardHolderNotification("You have just one more trial before you card gets blocked");
                        Console.WriteLine("-------------------------------");
                    }


                    if (cardPinTrials == 3)
                    {
                        CurrentUser.SetIsBlocked(true);
                        OnBlockCardHolder("Card Pin trial passed, you have been blocked");
                        Console.WriteLine("-------------------------------");
                       
                    }
                    if (CurrentUser.GetIsBlocked())
                    {
                        Environment.Exit(0);
                    }

                    if (CurrentUser.GetCardPin() == userPin)
                    {
                        break;
                    }
                    else
                    {
                        cardPinTrials++;
                        languageOptions.cardPinNotRecognized(Language);
                    }
                }
                catch
                {
                   cardPinTrials++;
                    languageOptions.cardPinNotRecognized(Language);
                   
                }
            }

            languageOptions.WelcomeUser(Language, CurrentUser.GetFullName());
            int option = 0;

            do
            {
                printOptions(Language);
                try
                {
                    option = int.Parse(Console.ReadLine());
                }
                catch
                {

                }
                if (option == 1)
                {
                    CurrentUser.deposit(CurrentUser, Language);
                }
                else if (option == 2)
                {
                    CurrentUser.withdraw(CurrentUser, Language);
                }
                else if (option == 3)
                {
                    languageOptions.DisplayBalance(Language, CurrentUser.GetUserBalance());
                }
                else if (option == 4)
                {
                    languageOptions.TransferInsertCard(Language);
                    string TransferUserDebitCardNumber = "";
                    CardHolder TransferUser;

                    while (true)
                    {
                        try
                        {
                            TransferUserDebitCardNumber = Console.ReadLine();
                            //check against db
                            TransferUser = CardHolders.FirstOrDefault(a => a.GetCardNumber() == TransferUserDebitCardNumber);
                            if (TransferUser != null)
                            {
                                break;
                            }
                            else
                            {
                                languageOptions.cardNotRecognized(Language);
                            }
                        }
                        catch
                        {
                            languageOptions.cardNotRecognized(Language);
                        }
                    }
                    CurrentUser.transfer(CurrentUser, TransferUser,Language);
                }
                else if (option == 5)
                {
                    break;
                }

            } while (option != 5);
            languageOptions.HaveANiceDay(Language);
        }

        private void AddBlockCardHolderNotification(Action<string> method)
        {
            BlockCardHolderNotification += method;
        }

      private void  AddBlockCardHolder(Action<string> method)
        {
            BlockCardHolder += method;
        }

        private void OnBlockCardHolderNotification(string message)
        {
            BlockCardHolderNotification?.Invoke(message);
        }

        private void OnBlockCardHolder(string message)
        {
            BlockCardHolder?.Invoke(message);
        }

        private void HandleBlockCardHolderNotification(string message)
        {
            Console.WriteLine("=> {0}", message);
        }

        private void HandleBlockCardHolder(string message)
        {
            Console.WriteLine("=> {0}", message);
        }
    }

}
