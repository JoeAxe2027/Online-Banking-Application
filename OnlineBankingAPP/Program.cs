class Program {
    private static List<Customer> customers;
    private static List<Employee> employees;
    private static string customersFilePath = "customer_data.csv";
    private static string employeesFilePath = "employee_data.csv";

    static void Main(string[] args) {
        customers = DataHelper.LoadCustomers(customersFilePath);
        employees = DataHelper.LoadEmployees(employeesFilePath);

        bool running = true;
        while (running) {
            Console.WriteLine("\nWelcome to your Online Banking Application!\n");
            Console.WriteLine("1. Account Login");
            Console.WriteLine("2. Create Account");
            Console.WriteLine("3. Administrator Login");
            Console.WriteLine("4. Quit");
            Console.Write("Select Option: ");

            string choice = Console.ReadLine();
            switch (choice) {
                case "1":
                    AccountLogin();
                    break;
                case "2":
                    CreateAccount();
                    break;
                case "3":
                    AdministratorLogin();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        DataHelper.SaveCustomers(customersFilePath, customers);
    }

    static void AccountLogin() {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Enter PIN: ");
        string pin = Console.ReadLine();

        var customer = customers.FirstOrDefault(c => c.AccountNumber == accountNumber && c.PIN == pin);
        if (customer != null) {
            Console.WriteLine("Login successful.");
            AccountServicesMenu(customer);
        } else {
            Console.WriteLine("Login failed. Check your credentials.");
        }
    }

    static void CreateAccount() {
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter PIN (4 digits): ");
        string pin = Console.ReadLine();
        Console.WriteLine("Choose account type (Savings, Checking): ");

        AccountType accountType;
        if (!Enum.TryParse<AccountType>(Console.ReadLine(), true, out accountType)) {
            Console.WriteLine("Invalid account type entered.");
            return;
        }

        string accountNumber = DataHelper.GenerateAccountNumber();
        Customer newCustomer = new Customer(firstName, lastName, accountNumber, pin, 100, accountType, LoanType.None, 0);
        customers.Add(newCustomer);
        Console.WriteLine("Account created successfully. Your account number is " + accountNumber);
    }

    static void AccountServicesMenu(Customer customer) {
        bool exitMenu = false;
        while (!exitMenu) {
            Console.WriteLine("\nAccount Services:");
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. Make Loan Payment");
            Console.WriteLine("5. Balance Inquiry");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Choose an option: ");

            string option = Console.ReadLine();
            switch (option) {
                case "1":
                    PerformWithdraw(customer);
                    break;
                case "2":
                    PerformDeposit(customer);
                    break;
                case "3":
                    PerformTransfer(customer);
                    break;
                case "4":
                    MakeLoanPayment(customer);
                    break;
                case "5":
                    Console.WriteLine($"Your current balance is ${customer.Balance:F2}");
                    break;
                case "6":
                    exitMenu = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void PerformWithdraw(Customer customer) {
        Console.Write("Enter amount to withdraw: ");
        double amount = Double.Parse(Console.ReadLine());
        if (customer.Balance >= amount) {
            customer.Balance -= amount;
            Console.WriteLine($"Withdrawal successful. New balance: ${customer.Balance:F2}");
        } else {
            Console.WriteLine("Insufficient funds.");
        }
    }

    static void PerformDeposit(Customer customer) {
        Console.Write("Enter amount to deposit: ");
        double amount = Double.Parse(Console.ReadLine());
        customer.Balance += amount;
        Console.WriteLine($"Deposit successful. New balance: ${customer.Balance:F2}");
    }

    static void PerformTransfer(Customer customer) {
        Console.Write("Enter amount to transfer: ");
        double amount = Double.Parse(Console.ReadLine());
        Console.Write("Enter recipient's account number: ");
        string recipientAccountNumber = Console.ReadLine();

        var recipient = customers.FirstOrDefault(c => c.AccountNumber == recipientAccountNumber);
        if (recipient != null && customer.Balance >= amount) {
            customer.Balance -= amount;
            recipient.Balance += amount;
            Console.WriteLine($"Transfer successful. Your new balance: ${customer.Balance:F2}");
        } else if (recipient == null) {
            Console.WriteLine("Recipient account not found.");
        } else {
            Console.WriteLine("Insufficient funds.");
        }
    }

    static void MakeLoanPayment(Customer customer) {
        Console.Write("Enter amount to pay on your loan: ");
        double amount = Double.Parse(Console.ReadLine());
        if (customer.LoanBalance > 0 && customer.Balance >= amount) {
            customer.LoanBalance -= amount;
            customer.Balance -= amount;
            Console.WriteLine($"Loan payment successful. New loan balance: ${customer.LoanBalance:F2}");
        } else if (customer.LoanBalance <= 0) {
            Console.WriteLine("No loan balance available.");
        } else {
            Console.WriteLine("Insufficient funds.");
        }
    }

static void AdministratorLogin() {
    Console.Write("Enter username: ");
    string username = Console.ReadLine();
    Console.Write("Enter password: ");
    string password = Console.ReadLine();

    var employee = employees.FirstOrDefault(e => e.Username == username && e.Password == password);
    if (employee != null) {
        Console.WriteLine("Login successful.");
        AdminMenu(employee);
    } else {
        Console.WriteLine("Login failed.");
    }
}

static void AdminMenu(Employee employee) {
    Console.WriteLine($"Admin menu for {employee.GetInfo()}:");
    Console.WriteLine("1. View Reports");
    Console.WriteLine("2. Exit");
    string choice = Console.ReadLine();
    switch (choice) {
        case "1":
            Console.WriteLine("Displaying Reports...");
            break;
        case "2":
            Console.WriteLine("Exiting...");
            break;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

}
