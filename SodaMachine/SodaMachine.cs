﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SodaMachine
{
    /// <summary>
    /// This class will hold the Register and Inventory Lists collections
    /// Fills the lists with objects for the collection
    /// </summary>
    class SodaMachine
    {
        // Member Variables
        public List<Coin> register;
        public List<Can> inventory;
        // Ctor
        public SodaMachine()
        {
            register = new List<Coin>();    // coin object collection
            FillRegister(20, 10, 20, 50);   // fill the register with coins

            inventory = new List<Can>();    // can object collection
            FillSodaMachine(20, 20, 20);    // fills the soda machine inventory
        } 

        // Member Methods
        public void IsPowerOn(bool isPowerOn)                                       //Powers on the soda machine (has no real functionality)
        {
            if (isPowerOn == true) { Console.WriteLine("The Soda Machine is ON"); Thread.Sleep(1000); }
            else { Console.WriteLine("The Soda Machine is OFF"); Thread.Sleep(1000); }
            //return isPowerOn;
        }

        private void FillSodaMachine(int orangesoda, int rootBeer, int cola)         // Adds Sodas to the inventory
        {
            for (int i = 0; i < orangesoda; i++) { inventory.Add(new OrangeSoda()); }  
            for (int i = 0; i < rootBeer; i++) { inventory.Add(new RootBeer()); } 
            for (int i = 0; i < cola; i++) { inventory.Add(new Cola()); } 
        }

        private void FillRegister(int quarters, int dimes, int nickles, int pennies) // Adds coins to the register
        {
            for (int i = 0; i < quarters; i++){ register.Add(new Quarter()); } 
            for (int i = 0; i < dimes; i++)   { register.Add(new Dime()); } 
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
    }
}
