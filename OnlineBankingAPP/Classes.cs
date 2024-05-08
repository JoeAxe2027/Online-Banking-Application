public class Customer {
    public string AccountNumber { get; private set; }
    public string PIN { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public double Balance { get; set; }
    public AccountType AccountType { get; private set; }
    public LoanType LoanType { get; private set; }
    public double LoanBalance { get; set; }

    public Customer(string firstName, string lastName, string accountNumber, string pin, double balance, AccountType accountType, LoanType loanType, double loanBalance) {
        FirstName = firstName;
        LastName = lastName;
        AccountNumber = accountNumber;
        PIN = pin;
        Balance = balance;
        AccountType = accountType;
        LoanType = loanType;
        LoanBalance = loanBalance;
    }
}
public class Employee : Person {
    public JobTitle JobTitle { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }

    public Employee(string firstName, string lastName, JobTitle jobTitle, string username, string password)
    : base(firstName, lastName) {
        JobTitle = jobTitle;
        Username = username;
        Password = password;
    }

    public override string GetInfo() {
        return $"Name: {FirstName} {LastName} - Title: {JobTitle}";
    }
    
}
public abstract class Person {
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }

    protected Person(string firstName, string lastName) {
        FirstName = firstName;
        LastName = lastName;
    }

    public abstract string GetInfo();  
}

