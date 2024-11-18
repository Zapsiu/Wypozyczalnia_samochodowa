using System;
using System.Linq;
using Spectre.Console;
using Wypozyczalnia_samochodowa.Data;
using Wypozyczalnia_samochodowa.Models;
using Wypozyczalnia_samochodowa.Views;

namespace Wypozyczalnia_samochodowa.Controllers
{
    public class CustomerController
    {
        private readonly RentalDbContext _context;
        private readonly CustomerView _customerView;

        public CustomerController(RentalDbContext context)
        {
            _context = context;
            _customerView = new CustomerView(this);
        }

        public Customer LogIn()
        {
            var credentials = _customerView.GetLoginCredentials();
            var customer = _context.Customers.SingleOrDefault(c => c.Username == credentials.Username && c.Password == credentials.Password);

            if (customer == null)
            {
                _customerView.ShowErrorMessage("Invalid username or password.");
                return null;
            }

            _customerView.ShowWelcomeMessage(customer);
            return customer;
        }

        public Customer SignUp()
        {
            var newCustomerDetails = _customerView.GetSignUpDetails();

            if (_context.Customers.Any(c => c.Username == newCustomerDetails.Username))
            {
                _customerView.ShowErrorMessage("Username already exists. Please choose a different one.");
                return null;
            }

            var newCustomer = new Customer
            {
                Imie = newCustomerDetails.FirstName,
                Nazwisko = newCustomerDetails.LastName,
                Username = newCustomerDetails.Username,
                Password = newCustomerDetails.Password,
                IsAdmin = false
            };

            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            _customerView.ShowSuccessMessage("Account created successfully!");

            return newCustomer;
        }

        public void ManageUsers()
        {
            while (true)
            {
                var selectedOption = _customerView.ShowManageUsersMenu();

                switch (selectedOption)
                {
                    case "List Users":
                        DisplayUsers();
                        PauseBeforeReturning();
                        break;
                    case "Edit User":
                        EditUser();
                        PauseBeforeReturning();
                        break;
                    case "Delete User":
                        DeleteUser();
                        PauseBeforeReturning();
                        break;
                    case "Return to Main Menu":
                        return;
                }
            }
        }

        private void DisplayUsers()
        {
            var users = _context.Customers.ToList();

            if (!users.Any())
            {
                _customerView.ShowErrorMessage("No users found.");
            }
            else
            {
                _customerView.DisplayUsers(users);
            }
        }

        private void EditUser()
        {
            int userId = _customerView.GetUserId("edit");
            var user = _context.Customers.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                _customerView.ShowErrorMessage("User not found.");
                return;
            }

            var updatedUser = _customerView.GetUpdatedUserDetails(user);
            if (updatedUser == null) return;

            _context.SaveChanges();
            _customerView.ShowSuccessMessage("User updated successfully.");
        }

        private void DeleteUser()
        {
            int userId = _customerView.GetUserId("delete");
            var user = _context.Customers.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                _customerView.ShowErrorMessage("User not found.");
                return;
            }

            if (_customerView.ConfirmDeletion(user))
            {
                _context.Customers.Remove(user);
                _context.SaveChanges();
                _customerView.ShowSuccessMessage("User deleted successfully.");
            }
            else
            {
                _customerView.ShowWarningMessage("Delete action canceled.");
            }
        }

        public void UpdateProfile(Customer currentUser)
        {
            var updatedProfile = _customerView.GetUpdatedProfileDetails(currentUser);
            if (updatedProfile == null) return;

            _context.SaveChanges();
            _customerView.ShowSuccessMessage("Profile updated successfully.");
        }

        private void PauseBeforeReturning()
        {
            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
            Console.ReadKey(true);
        }
    }
}
