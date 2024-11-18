using System;
using System.Collections.Generic;
using Spectre.Console;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa.Views
{
    public class CustomerView
    {
        private readonly CustomerController _customerController;

        public CustomerView(CustomerController customerController)
        {
            _customerController = customerController;
        }

        public (string Username, string Password) GetLoginCredentials()
        {
            AnsiConsole.Clear();
            string username = AnsiConsole.Ask<string>("Enter username:");
            string password = AnsiConsole.Ask<string>("Enter password:");
            return (username, password);
        }

        public (string FirstName, string LastName, string Username, string Password) GetSignUpDetails()
        {
            AnsiConsole.Clear();
            string firstName = AnsiConsole.Ask<string>("Enter first name:");
            string lastName = AnsiConsole.Ask<string>("Enter last name:");
            string username = AnsiConsole.Ask<string>("Enter username:");
            string password = AnsiConsole.Ask<string>("Enter password:");
            return (firstName, lastName, username, password);
        }

        public string ShowManageUsersMenu()
        {
            AnsiConsole.Clear();
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Manage Users")
                    .AddChoices(new[] { "List Users", "Edit User", "Delete User", "Return to Main Menu" })
            );
        }

        public void DisplayUsers(List<Customer> users)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Available Customers[/]");
            if (users == null || users.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No users found.[/]");
                return;
            }

            foreach (var user in users)
            {
                AnsiConsole.MarkupLine($"- ID: {user.Id}, Name: {user.Imie} {user.Nazwisko}, Username: {user.Username}, Admin: {user.IsAdmin}");
            }
        }

        public int GetUserId(string action)
        {
            return AnsiConsole.Ask<int>($"Enter the ID of the user you want to {action}:");
        }

        public Customer GetUpdatedUserDetails(Customer user)
        {
            try
            {
                user.Imie = AnsiConsole.Ask<string>("Enter new first name:", user.Imie);
                user.Nazwisko = AnsiConsole.Ask<string>("Enter new last name:", user.Nazwisko);
                user.Username = AnsiConsole.Ask<string>("Enter new username:", user.Username);
                user.IsAdmin = AnsiConsole.Confirm("Set admin status?");
                return user;
            }
            catch
            {
                ShowErrorMessage("Failed to update user. Invalid input.");
                return null;
            }
        }

        public Customer GetUpdatedProfileDetails(Customer currentUser)
        {
            try
            {
                currentUser.Imie = AnsiConsole.Ask<string>("Enter new first name:", currentUser.Imie);
                currentUser.Nazwisko = AnsiConsole.Ask<string>("Enter new last name:", currentUser.Nazwisko);
                currentUser.Username = AnsiConsole.Ask<string>("Enter new username:", currentUser.Username);
                currentUser.Password = AnsiConsole.Ask<string>("Enter new password:", currentUser.Password);
                return currentUser;
            }
            catch
            {
                ShowErrorMessage("Failed to update profile. Invalid input.");
                return null;
            }
        }

        public bool ConfirmDeletion(Customer user)
        {
            return AnsiConsole.Confirm($"Are you sure you want to delete {user.Imie} {user.Nazwisko}?");
        }

        public void ShowSuccessMessage(string message)
        {
            AnsiConsole.MarkupLine($"[green]{message}[/]");
        }

        public void ShowErrorMessage(string message)
        {
            AnsiConsole.MarkupLine($"[red]{message}[/]");
        }

        public void ShowWarningMessage(string message)
        {
            AnsiConsole.MarkupLine($"[yellow]{message}[/]");
        }

        public void ShowWelcomeMessage(Customer customer)
        {
            AnsiConsole.MarkupLine($"[green]Welcome, {customer.Imie} {customer.Nazwisko}![/]");
        }
    }
}
