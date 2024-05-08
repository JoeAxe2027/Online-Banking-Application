using System.Globalization;



public static class DataHelper {
    public static List<Customer> LoadCustomers(string filePath) {
        List<Customer> customers = new List<Customer>();
        if (!File.Exists(filePath)) {
            Console.WriteLine("Error: Customer file not found.");
            return customers;
        }
        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines) {
            try {
                var data = line.Split(',');
                customers.Add(new Customer(
                    data[2], data[3], data[0], data[1],
                    double.Parse(data[4], CultureInfo.InvariantCulture),
                    Enum.Parse<AccountType>(data[5], true),
                    Enum.Parse<LoanType>(data[6], true),
                    double.Parse(data[7], CultureInfo.InvariantCulture)
                ));
            } catch (Exception ex) {
                Console.WriteLine($"Error parsing customer data: {ex.Message}");
            }
        }
        return customers;
    }

    public static void SaveCustomers(string filePath, List<Customer> customers) {
        List<string> lines = new List<string>();
        foreach (var customer in customers) {
            lines.Add($"{customer.AccountNumber},{customer.PIN},{customer.FirstName},{customer.LastName},{customer.Balance.ToString(CultureInfo.InvariantCulture)},{customer.AccountType},{customer.LoanType},{customer.LoanBalance.ToString(CultureInfo.InvariantCulture)}");
        }
        File.WriteAllLines(filePath, lines);
    }

    public static List<Employee> LoadEmployees(string filePath) {
    List<Employee> employees = new List<Employee>();
    if (!File.Exists(filePath)) {
        Console.WriteLine("Error: Employee file not found.");
        return employees;
    }
    var lines = File.ReadAllLines(filePath);
    foreach (var line in lines) {
        try {
            var data = line.Split(',');
            employees.Add(new Employee(
                data[0], data[1], Enum.Parse<JobTitle>(data[2], true), data[3], data[4]
            ));
        } catch (Exception ex) {
            Console.WriteLine($"Error parsing employee data: {ex.Message}");
        }
    }
    return employees;
}

public static void SaveEmployees(string filePath, List<Employee> employees) {
    List<string> lines = new List<string>();
    foreach (var employee in employees) {
        lines.Add($"{employee.FirstName},{employee.LastName},{employee.JobTitle},{employee.Username},{employee.Password}");
    }
    File.WriteAllLines(filePath, lines);
}


    public static string GenerateAccountNumber() {
        Random random = new Random();
        string prefix = "183977";
        string randomNumber = new string(Enumerable.Repeat("0123456789", 10)
                                      .Select(s => s[random.Next(s.Length)]).ToArray());
        return prefix + randomNumber;
    }
}
