﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SodaMachine
{
    /// <summary>
    /// This class holds the machines register and soda inventory
    /// Access is controlled by the public properties
    /// </summary>
    class SodaMachine
    {
        // Member Variables
        public List<Coin> register;
        public List<Can> inventory;
        public bool validPayment;

        private double registerTotalCoins;
        private int[] registerCoinage;

        private int[] avalibleInventory;
        private double stockTotalValue;
        private string canSelection;
        private bool isMachineEmpty;

        // Properties
        public int[] AvalibleInventory
        {
            get { return avalibleInventory; }
        }
        public double StockTotalValue
        {
            get { return stockTotalValue; }
        }

        public double RegisterTotalCoins
        {
            get { return registerTotalCoins; }                    // the get accessors returns the value 
        }

        public int[] RegisterCoinage
        {
            get { return registerCoinage; }                        // the get accessors returns the value 
        }

        // Ctor
        public SodaMachine()
        {
            register = new List<Coin>();    // coin object collection
            FillRegister(20, 10, 20, 50);   // fill the register with coins

            inventory = new List<Can>();    // can object collection
            FillSodaMachine(20, 20, 20);    // fills the soda machine inventory
            
            this.avalibleInventory = TotalSodaInventory();
            this.stockTotalValue = TotalInventoryCost();

            this.registerTotalCoins = RegisterTotal();// sets the avalibleCoinage based on what's in the customer's wallet

            this.registerCoinage = new int[4];                // Initializes the private array
            registerCoinage = RegisterCoainage();             // Adds the array to the public array

        }

        /////////////// UI/LOGIC METHODS ///////////////
        public double UISodaSelection()
        {
            bool askAgain = true;
            double paymentAmount = 0;
            int sodaSelection;
            do
            {
                UserInterface.Clear();
                UserInterface.DisplaySodaSelction();
                sodaSelection = UserInterface.IntInputValidation("Select your soda: ");

                switch (sodaSelection)
                {
                    case 1: /* Root Beer */
                        if (avalibleInventory[0] == 0)   //check inventory
                        {
                            UserInterface.WaitForKey("Not Enough In Stock, pick again:", 500);
                            askAgain = true;
                        } else
                        {
                            canSelection = "Root Beer";
                            paymentAmount = 0.60d; askAgain = false;
                        } break;

                    case 2: /* Orange Soda */
                        if (avalibleInventory[1] == 0)   //check inventory
                        {
                            UserInterface.WaitForKey("Not Enough In Stock, pick again:", 500);
                            askAgain = true;
                        }
                        else 
                        {
                            canSelection = "Orange Soda";
                            paymentAmount = 0.06d; askAgain = false; 
                        } break;

                    case 3: /* Cola */
                        if (avalibleInventory[2] == 0)   //check inventory
                        {
                            UserInterface.WaitForKey("Not Enough In Stock, pick again:", 500);
                            askAgain = true;
                        }
                        else
                        {
                            canSelection = "Cola";
                            paymentAmount = 0.35d; askAgain = false;
                        } break;

                    case 4: /* Exit */; break;

                    default:
                        Console.WriteLine("MOM I BROKE THE UNIVERSE AGAIN!");
                        break;
                }

            } while (askAgain == true);
            
            Math.Round(paymentAmount, 3);
            return paymentAmount;
        }

        public string DispenseSoda(bool validPayment)
        {
            if (validPayment == false) //This double checks the payment integrity
            {   // User will lose money but this is a "security feature" to protect inventory
                
            }
            else
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].Name == canSelection)
                    {
                        inventory.Remove(inventory[i]);
                        UserInterface.Pause($"{inventory[i].Name} Dispensed", 1000);
                        break;
                    }
                }
            }
            
            return canSelection;

        }

        ///// INVNETORY CONTROL AND DISPENSING ///// 

        public bool IsMachineEmpty()
        {
            if (inventory.Count == 0)
            {
                isMachineEmpty = true;
            }
            else
            {
                isMachineEmpty = false;
            }

            return isMachineEmpty;
        }

        public void UIMachineInventory()
        {
            string displayRegister = Math.Round(RegisterTotalCoins, 3).ToString("0.00");
            string displayStockValue = Math.Round(StockTotalValue, 3).ToString("0.00");

            Console.WriteLine("#### WHAT'S IN THE MACHINE??? ####");
            UserInterface.MenuDecorators("star");
            Console.WriteLine($"Root Beer: {AvalibleInventory[0]} \n" +
                                $"Orange Soda: {AvalibleInventory[1]} \n" +
                                $"Cola: {AvalibleInventory[2]}");
            UserInterface.MenuDecorators("starlong");
            Console.WriteLine($"Total Inventory is: ${displayStockValue}");
            UserInterface.MenuDecorators("starlong");
            Console.WriteLine($"Quarters: {RegisterCoinage[0]}");
            Console.WriteLine($"Dimes: {RegisterCoinage[1]}");
            Console.WriteLine($"Nickels: {RegisterCoinage[2]}");
            Console.WriteLine($"Pennies: {RegisterCoinage[3]}");
            UserInterface.MenuDecorators("starlong");

            Console.WriteLine($"Register Coins Total: ${displayRegister}");

            UserInterface.WaitForKey("Press ENTER to return to menu...", 500);
        }

        private int[] TotalSodaInventory()
        {
            avalibleInventory = new int[3];
            
            foreach (Can can in inventory)
            {
                switch (can.Name)
                {
                    case "Root Beer":
                        avalibleInventory[0]++;
                        break;
                    case "Orange Soda":
                        avalibleInventory[1]++;
                        break;
                    case "Cola":
                        avalibleInventory[2]++;
                        break;
                    default:
                        break;
                }
            }
            
            return avalibleInventory;
        }

        private double TotalInventoryCost()
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                stockTotalValue += inventory[i].Cost;
            }
            stockTotalValue = Math.Round(stockTotalValue, 3); 
            return stockTotalValue;
        }

        public void IsPowerOn(bool isPowerOn)                                       //Powers on the soda machine (has no real functionality)
        {
            if (isPowerOn == true) { Console.WriteLine("The Soda Machine is ON"); Thread.Sleep(1000); }
            else { Console.WriteLine("The Soda Machine is OFF"); Thread.Sleep(1000); }
            //return isPowerOn;
        }

        public void UpdateSodaInventory()
        {   /// Updates the available inventory
            avalibleInventory = TotalSodaInventory();
            stockTotalValue = TotalInventoryCost();
        }
        private void FillSodaMachine(int rootBeer, int orangesoda, int cola)         // Adds Sodas to the inventory
        {
            for (int i = 0; i < rootBeer; i++) { inventory.Add(new RootBeer()); }
            for (int i = 0; i < orangesoda; i++) { inventory.Add(new OrangeSoda()); }
            for (int i = 0; i < cola; i++) { inventory.Add(new Cola()); }
            isMachineEmpty = false;
        }

        ///// REGISTER CONTROL AND RECONCILIATION ///// 

        private void FillRegister(int quarters, int dimes, int nickles, int pennies) // Adds coins to the register
        {
            for (int i = 0; i < quarters; i++) { register.Add(new Quarter()); }
            for (int i = 0; i < dimes; i++) { register.Add(new Dime()); }
            for (int i = 0; i < nickles; i++) { register.Add(new Nickle()); }
            for (int i = 0; i < pennies; i++) { register.Add(new Penny()); }
        }

        public double RegisterReconciliation()
        {
            double registerTotal = 0.0;

            for (int i = 0; i < register.Count; i++)
            {
                registerTotal += register[i].Value;
            }
            registerTotal = Math.Round(registerTotal, 3);

            Console.WriteLine($"Register Total:${registerTotal}");
            Thread.Sleep(2000);
            return registerTotal;
        }

        public void PrintInventoryAndRegister()
        {
            Console.WriteLine("Inventory");
            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine($"{inventory[i].Name} at ${inventory[i].Cost}");
            }

            Console.WriteLine("Register");
            for (int i = 0; i < register.Count; i++)
            {
                Console.WriteLine($"{register[i].Name} is ${register[i].Value}");
            }
            Console.ReadLine();
        }

        private double RegisterTotal()
        {
            double CoinsTotal = 0.0;

            for (int i = 0; i < register.Count; i++)
            {
                CoinsTotal += register[i].Value;
            }
            CoinsTotal = Math.Round(CoinsTotal, 3);


            Thread.Sleep(2000);

            return CoinsTotal;
        }

        public void UpdateRegisterCoinage(double addPayment)
        {   /// After the customer/user makes payment we need to 
            /// reconcile what is available for the next transaction         
            registerTotalCoins = RegisterTotal();
            registerCoinage = RegisterCoainage();
        }

        private int[] RegisterCoainage()
        {   /// Creates an array of the number of individual coins
            /// available to the customer/user ex; 7 Quarters
            foreach (Coin coin in register)
            {
                switch (coin.Value)
                {
                    case 0.25: // Quarter
                        registerCoinage[0]++;
                        break;
                    case 0.10: // Dime
                        registerCoinage[1]++;
                        break;
                    case 0.05: // Nickel
                        registerCoinage[2]++;
                        break;
                    case 0.01: // Penny
                        registerCoinage[3]++;
                        break;
                    default:
                        break;
                }
            }

            Thread.Sleep(1000);
            return registerCoinage;
        }


    }
}
