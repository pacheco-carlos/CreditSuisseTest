using CreditSuisseTest;
using System.Globalization;

Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us", false);

IList<Trade> trades = new List<Trade>();

//Getting the reference date
Console.WriteLine("Enter the Reference Date (mm/dd/yyy): ");
string referenceDateInputed = Console.ReadLine();

if (string.IsNullOrEmpty(referenceDateInputed))
{
	return;
}

int[] date = referenceDateInputed.Split('/').Select(x => int.Parse(x)).ToArray();
DateTime referenceDate = new DateTime(date[2], date[0], date[1]);

//Getting the number of trades
Console.WriteLine("Number of Trades: ");
int numberOfTrades = Convert.ToInt32(Console.ReadLine());

//Getting the trades
for (var i = 1; i <= numberOfTrades; i++)
{
	Trade trade = new Trade();

	Console.WriteLine("Inform the trade number " + i + " (Value ClienteSector NextPaymentDate): ");
	var line = Console.ReadLine();

	string[] inputTrade = line.Split(' ').Select(x => x).ToArray();

	trade.Value = Convert.ToDouble(inputTrade[0]);
	trade.ClientSector = Convert.ToString(inputTrade[1]);

	int[] lineDate = Convert.ToString(inputTrade[2]).Split('/').Select(x => int.Parse(x)).ToArray();
	DateTime dateFormated = new DateTime(lineDate[2], lineDate[0], lineDate[1]);

	trade.NextPaymentDate = dateFormated;

	trades.Add(trade);
}

foreach (var tr in trades)
{
	if (tr.NextPaymentDate < referenceDate.AddDays(30))
	{
		Console.WriteLine("EXPIRED");
	}
	else if (tr.Value > 1000000 && tr.ClientSector == "Private")
	{
		Console.WriteLine("HIGHRISK");
	}
	else if (tr.Value > 1000000 && tr.ClientSector == "Public")
	{
		Console.WriteLine("MEDIUMRISK");
	}
}