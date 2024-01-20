using System;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

string pathToFileClients = "C:\\Users\\tleder\\Desktop\\LambooInvestmentFund\\KlijentiLamboo.txt";
string pathToFileEmployees = "C:\\Users\\tleder\\Desktop\\LambooInvestmentFund\\ZaposleniLamboo.txt";
string pathToFileInvestments = "C:\\Users\\tleder\\Desktop\\LambooInvestmentFund\\InvesticijeLamboo.txt";
string pathToFileStocks = "C:\\Users\\tleder\\Desktop\\LambooInvestmentFund\\DioniceLamboo.txt";
string pathToFileMoving = "C:\\Users\\tleder\\Desktop\\LambooInvestmentFund\\KretnjaDioniceLamboo.txt";

List<Client> clientInformation = LoadInfo(pathToFileClients);
List<Employee> employeeInformation = LoadInformation(pathToFileEmployees);
List<Investment> investmentInformation = LoadInvestment(pathToFileInvestments);
List<Stock> stockInformation = LoadStock(pathToFileStocks);
List<StockMovement> movementInformation = LoadStockMovement(pathToFileMoving);

bool shouldTheProgramBeRunning = true;

while (shouldTheProgramBeRunning)
{
    Console.WriteLine("\nInsert an option...\n" +
        "1. Output of all information\n" +
        "2. Input a new Client\n" +
        "3. Input a new Employee\n" +
        "4. Delete a Client\n" +
        "5. Delete an Employee\n" +
        "6. Save a Client\n" +
        "7. Save an employee\n" +
        "8. Top 5 Clients\n" +
        "9. Worst 5 Clients\n" +
        "10. Top 3 Investments\n"+
        "11. Worst 3 Investments\n" +
        "12. Best Investment of the Month\n" +
        "13. Worst Investment of the Month\n" +
        "14. Stock Fluctuation Month-By-Month\n"+
        "15. Stock Fluctuation Month-By-Month in percentages(%)\n"+
        "16. Analysis of optimal purchase and sale time\n"+
        "17. End Programme\n");

    int input = Convert.ToInt32(Console.ReadLine());
    switch (input)
    {
        case 1:
            Console.WriteLine("Clients: \n");
            OutputClients(clientInformation);
            Console.WriteLine("\nEmployees: \n");
            OutputEmployees(employeeInformation);
            break;
        case 2:
            clientInformation.Add(InsertClient());
            break;
        case 3:
            employeeInformation.Add(InsertEmployee());
            break;
        case 4:
            DeleteItem(clientInformation);
            break;
        case 5:
            DeleteEmployee(employeeInformation);
            break;
        case 6:
            SaveClients(pathToFileClients, clientInformation);
            break;
        case 7:
            SaveEmployee(pathToFileEmployees, employeeInformation);
            break;
        case 8:
            PrintTop5Clients(investmentInformation, clientInformation); 
            break;
        case 9:
            PrintWorst5Clients(investmentInformation, clientInformation);
            break;
        case 10:
            PrintTop3Investments(investmentInformation, clientInformation);
            break;
        case 11:
            PrintTop3WorstInvestments(investmentInformation, clientInformation);
            break;
        case 12:
            PrintBestInvestmentOfTheMonth(investmentInformation);
            break;
        case 13:
            PrintWorstInvestmentOfTheMonth(investmentInformation);
            break;
        case 14:
            AnalyzeStockMovement(stockInformation, movementInformation);
            break;
        case 15:
            AnalyzeStockMovementInPercentages(stockInformation, movementInformation);
            break;
        case 16:
            FindOptimalStockPeriod(movementInformation);
            break;
        case 17:
            shouldTheProgramBeRunning = false;
            break;
        
        default:
            Console.WriteLine("Wrong Input");
            break;
    }
}

void DeleteItem(List<Client> companyInformation)
{

    Console.WriteLine("Insert an Item you wish to delete");
    string deletedName = Console.ReadLine();

    foreach (Client title in companyInformation)
    {
        if (title.name == deletedName)
        {
            Console.WriteLine("{0} found. We're deleting the element", title.ToString());

            companyInformation.Remove(title);
            return;
        }
    }

}
void DeleteEmployee(List<Employee> employeeInformation)
{

    Console.WriteLine("Insert an Item you wish to delete");
    string deletedName = Console.ReadLine();
    foreach (Employee title in employeeInformation)
    {
        if (title.name == deletedName)
        {
            Console.WriteLine("{0} found. We're deleting the element", title.ToString());

            employeeInformation.Remove(title);
            return;
        }
    }

}
void SaveClients(string pathToFile, List<Client> information)
{
    List<string> inputLines = new List<string>();
    foreach (Client handle in information)
    {
        string linija = String.Format("{0};{1};{2}", handle.name, handle.function, handle.investment);

        inputLines.Add(linija);
    }

    File.WriteAllLines(pathToFile, inputLines);
}
void SaveEmployee(string pathToFile, List<Employee> information)
{
    List<string> linijeZaUpis = new List<string>();
    foreach (Employee handle in information)
    {
        string linija = String.Format("{0};{1};{2}", handle.name, handle.function, handle.paycheck);
        linijeZaUpis.Add(linija);
    }

    File.WriteAllLines(pathToFile, linijeZaUpis);
}

void OutputClients(List<Client> humans)
{
    foreach (Client clients in humans)
    {
        Console.WriteLine("{0} ",clients.ToString());
    }
}
void OutputEmployees(List<Employee> people)
{
    foreach (Employee employees in people)
    {
        Console.WriteLine("{0} ", employees.ToString());
    }
}

Client InsertClient()
{
    Console.WriteLine("Insert a name");
    string name = Console.ReadLine();

    Console.WriteLine("Insert the person's function");
    string function = Console.ReadLine();

    Console.WriteLine("Insert the person's investment.");
    string investment = Console.ReadLine();

    return new Client(name, function, investment);

}
Employee InsertEmployee()
{
    Console.WriteLine("Insert a name");
    string name = Console.ReadLine();

    Console.WriteLine("Insert the person's function");
    string function = Console.ReadLine();

    Console.WriteLine("Insert the person's paycheck");
    int paycheck = Convert.ToInt32(Console.ReadLine());

    return new Employee(name, function,Convert.ToInt32(paycheck));
}

List<Client> LoadInfo(string putanja)
{
    List<Client> Lamboo = new List<Client>();

    
    foreach (string linija in File.ReadAllLines(putanja))
    {
   
        string[] Parts = linija.Split(";");

        Lamboo.Add(new Client(Parts[0], Parts[1], Parts[2]));
    }

    return Lamboo;
}
List<Employee> LoadInformation(string putanja)
{
    List<Employee> Lamboo = new List<Employee>();

    foreach (string linija in File.ReadAllLines(putanja))
    {
        string[] Parts = linija.Split(";");
        Lamboo.Add(new Employee(Parts[0], Parts[1], Convert.ToInt32(Parts[2])));
    }

    return Lamboo;
}
List<Investment> LoadInvestment(string putanja)
{
    List<Investment> Lamboo = new List<Investment>();

    foreach (string linija in File.ReadAllLines(putanja))
    {
        string[] Parts = linija.Split(";");
        Lamboo.Add(new Investment(Convert.ToInt32(Parts[0]), Parts[1], Convert.ToInt32(Parts[2]), Convert.ToInt32(Parts[3]), Parts[4], Convert.ToInt32(Parts[5])));
    }

    return Lamboo;
}

List<Stock> LoadStock(string putanja)
{
    List<Stock> Lamboo = new List<Stock>();

    foreach (string linija in File.ReadAllLines(putanja))
    {
        string[] Parts = linija.Split(";");
        Lamboo.Add(new Stock(Parts[0], Convert.ToInt32(Parts[1])));
    }

    return Lamboo;
}

List<StockMovement> LoadStockMovement(string putanja)
{
    List<StockMovement> Lamboo = new List<StockMovement>();

    foreach (string linija in File.ReadAllLines(putanja))
    {
        string[] Parts = linija.Split(";");
        Lamboo.Add(new StockMovement(Parts[0],Parts[1], Convert.ToInt32(Parts[2]), Convert.ToInt32(Parts[3])));
    }

    return Lamboo;
}

void PrintTop5Clients(List<Investment> investmentInformation, List<Client> clientInformation)
{
    // Calculate total investments for each client and store in a list of anonymous types
    var topClients = investmentInformation
        .GroupBy(investment => investment.clientId)
        .OrderByDescending(group => group.Sum(inv => inv.currentPriceOfPurchase * inv.numberOfStocks))
        .Take(5);

    // Print the top 5 clients
    Console.WriteLine("\nTop 5 Clients based on Investments:\n");
    foreach (var topClientGroup in topClients)
    {
        int clientId = topClientGroup.Key;
        Console.WriteLine($"Client ID: {clientId}");
    }
}
void PrintWorst5Clients(List<Investment> investmentInformation, List<Client> clientInformation)
{
    // Calculate total investments for each client and store in a list of anonymous types
    var worstClients = investmentInformation
        .GroupBy(investment => investment.clientId)
        .OrderBy(group => group.Sum(inv => inv.currentPriceOfPurchase * inv.numberOfStocks))
        .Take(5);

    // Print the worst 5 clients
    Console.WriteLine("\nWorst 5 Clients based on Investments:\n");
    foreach (var worstClientGroup in worstClients)
    {
        int clientId = worstClientGroup.Key;
        Console.WriteLine($"Client ID: {clientId}");
    }
}

void PrintTop3Investments(List<Investment> investmentInformation, List<Client> clientInformation)
{
    // Calculate total value for each investment and store in a list of anonymous types
    var topInvestments = investmentInformation
        .OrderByDescending(inv => inv.currentPriceOfPurchase * inv.numberOfStocks)
        .Take(3);

    // Print the top 3 investments
    Console.WriteLine("\nTop 3 Investments:\n");
        foreach (var topInvestment in topInvestments)
        {
         Console.WriteLine($"Client ID: {topInvestment.clientId}, Investment Value: {topInvestment.currentPriceOfPurchase * topInvestment.numberOfStocks:C}");
        }
}

void PrintTop3WorstInvestments(List<Investment> investmentInformation, List<Client> clientInformation)
{
    // Calculate total value for each investment and store in a list of anonymous types
    var worstInvestments = investmentInformation
        .OrderBy(inv => inv.currentPriceOfPurchase * inv.numberOfStocks)
        .Take(3);

    // Print the top 3 worst investments
    Console.WriteLine("\nTop 3 Worst Investments:\n");
    foreach (var worstInvestment in worstInvestments)
    {
        Console.WriteLine($"Client ID: {worstInvestment.clientId}, Investment Value: {worstInvestment.currentPriceOfPurchase * worstInvestment.numberOfStocks:C}");
    }
}

void PrintBestInvestmentOfTheMonth(List<Investment> investmentInformation)
{
    Console.WriteLine("Enter the month (e.g., January):");
    string inputMonth = Console.ReadLine();

    Console.WriteLine("Enter the year:");
    int inputYear;
    if (!int.TryParse(Console.ReadLine(), out inputYear))
    {
        Console.WriteLine("Invalid year input.");
        return;
    }

    // Find the best investment for the specified month and year
    var bestInvestment = investmentInformation
        .Where(inv => inv.monthOfPurchase.Equals(inputMonth, StringComparison.OrdinalIgnoreCase) && inv.yearOfPurchase == inputYear)
        .OrderByDescending(inv => inv.currentPriceOfPurchase * inv.numberOfStocks)
        .FirstOrDefault();

    // Print the best investment of the specified month and year
    Console.WriteLine($"\nBest Investment of {inputMonth}, {inputYear}:\n");
    if (bestInvestment != null)
    {
        Console.WriteLine($"Client ID: {bestInvestment.clientId}, Investment Value: {bestInvestment.currentPriceOfPurchase * bestInvestment.numberOfStocks:C}");
    }
    else
    {
        Console.WriteLine("No investments found for the specified month and year.");
    }
}
void PrintWorstInvestmentOfTheMonth(List<Investment> investmentInformation)
{
    Console.WriteLine("Enter the month (e.g., January):");
    string inputMonth = Console.ReadLine();

    Console.WriteLine("Enter the year:");
    int inputYear;
    if (!int.TryParse(Console.ReadLine(), out inputYear))
    {
        Console.WriteLine("Invalid year input.");
        return;
    }

    // Find the worst investment for the specified month and year
    var worstInvestment = investmentInformation
        .Where(inv => inv.monthOfPurchase.Equals(inputMonth, StringComparison.OrdinalIgnoreCase) && inv.yearOfPurchase == inputYear)
        .OrderBy(inv => inv.currentPriceOfPurchase * inv.numberOfStocks)
        .FirstOrDefault();

    // Print the worst investment of the specified month and year
    Console.WriteLine($"\nWorst Investment of {inputMonth}, {inputYear}:\n");
    if (worstInvestment != null)
    {
        Console.WriteLine($"Client ID: {worstInvestment.clientId}, Investment Value: {worstInvestment.currentPriceOfPurchase * worstInvestment.numberOfStocks:C}");
    }
    else
    {
        Console.WriteLine("No investments found for the specified month and year.");
    }
}

void AnalyzeStockMovement(List<Stock> stocks, List<StockMovement> movements)
{
    Console.WriteLine("Enter the stock symbol (e.g., AAPL):");
    string stockSymbol = Console.ReadLine();

    Console.WriteLine("Enter the starting month:");
    string startMonth = Console.ReadLine();

    Console.WriteLine("Enter the starting year:");
    int startYear;
    if (!int.TryParse(Console.ReadLine(), out startYear))
    {
        Console.WriteLine("Invalid start year input.");
        return;
    }

    Console.WriteLine("Enter the ending month:");
    string endMonth = Console.ReadLine();

    Console.WriteLine("Enter the ending year:");
    int endYear;
    if (!int.TryParse(Console.ReadLine(), out endYear))
    {
        Console.WriteLine("Invalid end year input.");
        return;
    }

    // Filter movements for the specified stock and time frame
    var stockMovements = movements
        .Where(movement =>
            movement.sign == stockSymbol &&
            movement.month.CompareTo(startMonth) >= 0 &&
            movement.month.CompareTo(endMonth) <= 0 &&
            movement.year >= startYear &&
            movement.year <= endYear)
        .OrderBy(movement => movement.year)
        .ThenBy(movement => Array.IndexOf(Enum.GetNames(typeof(Month)), movement.month));

    int initialPrice = 0;
    int finalPrice = 0;

    foreach (var movement in stockMovements)
    {
        if (movement.month == startMonth && movement.year == startYear)
        {
            initialPrice = movement.priceAtTheEndOfTheMonth;
        }

        if (movement.month == endMonth && movement.year == endYear)
        {
            finalPrice = movement.priceAtTheEndOfTheMonth;
        }
    }

    if (initialPrice == 0 || finalPrice == 0)
    {
        Console.WriteLine("Could not find stock movements for the specified time frame.");
        return;
    }

    int change = finalPrice - initialPrice;

    if (change > 0)
    {
        Console.WriteLine($"The stock {stockSymbol} rose by ${change} between {startMonth}, {startYear} and {endMonth}, {endYear}.");
    }
    else if (change < 0)
    {
        Console.WriteLine($"The stock {stockSymbol} fell by {Math.Abs(change)} between {startMonth}, {startYear} and {endMonth}, {endYear}.");
    }
    else
    {
        Console.WriteLine($"The stock {stockSymbol} stagnated between {startMonth}, {startYear} and {endMonth}, {endYear}.");
    }
}

void AnalyzeStockMovementInPercentages(List<Stock> stockInformation, List<StockMovement> movementInformation)
{
    Console.WriteLine("Enter the stock short name (e.g., AAPL):");
    string stockShortName = Console.ReadLine();

    Console.WriteLine("Enter the starting month (e.g., January):");
    string startMonth = Console.ReadLine();

    Console.WriteLine("Enter the starting year:");
    int startYear;
    if (!int.TryParse(Console.ReadLine(), out startYear))
    {
        Console.WriteLine("Invalid year input.");
        return;
    }

    Console.WriteLine("Enter the ending month (e.g., February):");
    string endMonth = Console.ReadLine();

    Console.WriteLine("Enter the ending year:");
    int endYear;
    if (!int.TryParse(Console.ReadLine(), out endYear))
    {
        Console.WriteLine("Invalid year input.");
        return;
    }

    // Find the stock movements for the specified stock between the specified months and years
    var startMovement = movementInformation
        .FirstOrDefault(movement => movement.sign == stockShortName && movement.month.Equals(startMonth, StringComparison.OrdinalIgnoreCase) && movement.year == startYear);

    var endMovement = movementInformation
        .FirstOrDefault(movement => movement.sign == stockShortName && movement.month.Equals(endMonth, StringComparison.OrdinalIgnoreCase) && movement.year == endYear);

    if (startMovement != null && endMovement != null)
    {
        int startPrice = startMovement.priceAtTheEndOfTheMonth;
        int endPrice = endMovement.priceAtTheEndOfTheMonth;

        // Calculate the percentage change
        double percentageChange = ((endPrice - startPrice) / (double)startPrice) * 100;

        Console.WriteLine($"\nStock movement analysis for {stockShortName} from {startMonth}, {startYear} to {endMonth}, {endYear}:\n");

        if (percentageChange > 0)
        {
            Console.WriteLine($"Percentage Growth: {percentageChange:F2}%");
        }
        else if (percentageChange < 0)
        {
            Console.WriteLine($"Percentage Fall: {percentageChange:F2}%");
        }
        else
        {
            Console.WriteLine("Stagnation: 0%");
        }
    }
    else
    {
        Console.WriteLine("No stock movements found for the specified stock and time period.");
    }
}
static void FindOptimalStockPeriod(List<StockMovement> stockMovements)
{
    Console.WriteLine("Enter the stock short name:");
    string stockShortName = Console.ReadLine();

    int buyMonth = 0, buyYear = 0, sellMonth = 0, sellYear = 0;
    int maxProfit = 0;
    int currentProfit = 0;

    for (int i = 0; i < stockMovements.Count; i++)
    {
        var currentMovement = stockMovements[i];

        if (currentMovement.sign.Equals(stockShortName, StringComparison.OrdinalIgnoreCase))
        {
            if (currentMovement.priceAtTheEndOfTheMonth < stockMovements[buyMonth].priceAtTheEndOfTheMonth)
            {
                buyMonth = i;
            }
            else
            {
                currentProfit = currentMovement.priceAtTheEndOfTheMonth - stockMovements[buyMonth].priceAtTheEndOfTheMonth;

                if (currentProfit > maxProfit)
                {
                    maxProfit = currentProfit;
                    sellMonth = i;
                }
            }
        }
    }

    if (maxProfit > 0)
    {
        Console.WriteLine($"Optimal time to buy {stockShortName}: {stockMovements[buyMonth].month} {stockMovements[buyMonth].year}");
        Console.WriteLine($"Optimal time to sell {stockShortName}: {stockMovements[sellMonth].month} {stockMovements[sellMonth].year}");
        Console.WriteLine($"Max Profit: ${maxProfit} /share");
    }
    else
    {
        Console.WriteLine($"No profitable period found for {stockShortName}.");
    }
}

public class Client
{
    public string name;
    public string function;
    public string investment;

    public Client(string Name, string Function, string Investment)
    {
        this.name = Name;
        this.function = Function;
        this.investment = Investment;
    }

    public string ToString()
    {
        return String.Format("Name and Surname: {0}, function: {1}, investment: {2}", name, function, investment);
    }
}

public class Employee
{
    public string name;
    public string function;
    public int paycheck;


    public Employee(string Name, string Function, int Paycheck)
    {
        this.name = Name;
        this.function = Function;
        this.paycheck = Paycheck;
    }
    public string ToString()
    {
        return String.Format("Name and surname: {0}, function {1}, investment{2}", name, function, Convert.ToInt32(paycheck));
    }
}

public class Investment
{
   public int clientId;
   public string sign;
   public int numberOfStocks;
   public int currentPriceOfPurchase;
   public string monthOfPurchase;
   public int yearOfPurchase;

    public Investment(int ClientId,string Sign,int NumberOfStocks,int CurrentPriceOfPurchase,string MonthOfPurchase,int YearOfPurchase)
    {
        this.clientId = ClientId;
        this.sign = Sign;
        this.numberOfStocks = NumberOfStocks;
        this.currentPriceOfPurchase = CurrentPriceOfPurchase;
        this.monthOfPurchase = MonthOfPurchase;
        this.yearOfPurchase = YearOfPurchase; 
    }
    public string ToString()
    {
        return String.Format("Client ID{0}, sign {1}, number of stocks {2}, current price of purchase {3}, month of purchase {4},year of purchase {5}", Convert.ToInt32(clientId), sign, Convert.ToInt32(numberOfStocks), Convert.ToInt32(currentPriceOfPurchase), monthOfPurchase, Convert.ToInt32(yearOfPurchase));
    }
}

public class Stock
{
    public string sign;
    public int currentPrice;

    public Stock(string sign, int currentPrice)
    {
        this.sign = sign;
        this.currentPrice = Convert.ToInt32(currentPrice);
    }
    public string ToString()
    {
        return String.Format("Sign of the stock {0}, price of the stock {1}", sign, Convert.ToInt32(currentPrice));
    }
}

public class StockMovement
{
    public string sign;
    public string month;
    public int year;
    public int priceAtTheEndOfTheMonth;

    public StockMovement(string sign, string month, int year, int priceAtTheEndOfTheMonth)
    {
        this.sign = sign;
        this.month = month;
        this.year = year;
        this.priceAtTheEndOfTheMonth = priceAtTheEndOfTheMonth;
    }
    public string ToString()
    {
        return String.Format("Sign of the stock moving {0}, month of movement {1}, year of movement {2}, price at the end of the month {3}", sign, month, Convert.ToInt32(year), Convert.ToInt32(priceAtTheEndOfTheMonth));
    }
}
public enum Month
{
    NotSet = 0,
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
} 