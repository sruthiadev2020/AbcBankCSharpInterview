﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = new Account(Account.AccountType.CHECKING);
            Account savingsAccount = new Account(Account.AccountType.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(new Account(Account.AccountType.SAVINGS));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccounts()
        {
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.OpenAccount(new Account(Account.AccountType.CHECKING));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.OpenAccount(new Account(Account.AccountType.CHECKING));
            oscar.OpenAccount(new Account(Account.AccountType.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TransferBetweenAccounts()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            Account checkingAccount = new Account(Account.AccountType.CHECKING);
            john.OpenAccount(checkingAccount);
            bank.AddCustomer(john);

            checkingAccount.Deposit(1000.0);

            Account savingsAccount = new Account(Account.AccountType.SAVINGS);
            john.OpenAccount(savingsAccount);
            bank.AddCustomer(john);

            savingsAccount.Deposit(1200.0);

            checkingAccount.TransferBetweenAccounts(savingsAccount, 200.0);

            Assert.AreEqual("Statement for John\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $1,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $800.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $1,200.00\n" +
                    "  deposit $200.00\n" +
                    "Total $1,400.00\n" +
                    "\n" +
                    "Total In All Accounts $2,200.00", john.GetStatement());

        }
    }
}
