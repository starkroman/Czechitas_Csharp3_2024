var calculator = new StringCalculator();
var result = calculator.Add("");
var result2 = calculator.Add2("-1,-2,-3");
var result3 = calculator.Add3("1\n3,5");
var result4 = calculator.Add4("//;\n2;2");
var result5 = calculator.Add5("//[|||]\n1|||2|||3");
var result6 = calculator.Add6("//[|][%]\n1|2%3");


if (result == 0)
    Console.WriteLine("OK.");
else
    Console.WriteLine("KO!");

if (result2 == -6)
    Console.WriteLine("OK.");
else
    Console.WriteLine("KO!");

if (result3 == 9)
    Console.WriteLine("OK.");
else
    Console.WriteLine("KO!");

if (result4 == 4)
    Console.WriteLine("OK.");
else
    Console.WriteLine("KO!");

if (result5 == 6)
    Console.WriteLine("OK.");
else
    Console.WriteLine("KO!");

if (result6 == 6)
    Console.WriteLine("OK.");
else
    Console.WriteLine("KO!");

public class StringCalculator
{
    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        return 0; // Prozatím vše vrací nulu.
    }

    //2.varianta
    public int Add2(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var numberArray = numbers.Split(',');

        return numberArray.Select(int.Parse).Sum();
    }

    // 3.varianta
    public int Add3(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var numberArray = numbers.Split(new[] { ',', '\n' });

        return numberArray.Select(int.Parse).Sum();
    }

    // 4.varianta
    public int Add4(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        string delimiter = ",";

        if (numbers.StartsWith("//"))
        {
            // Oddělovač je mezi "//" a "\n", takže ho extrahujeme
            var delimiterEndIndex = numbers.IndexOf('\n');

            delimiter = numbers.Substring(2, delimiterEndIndex - 2);

            // Odstraníme část s oddělovačem a necháme jen čísla
            numbers = numbers.Substring(delimiterEndIndex + 1);
        }

        // Rozdělení čísel na základě standardního i vlastního oddělovače
        var numberArray = numbers.Split(new[] { delimiter, "\n" }, StringSplitOptions.None);

        return numberArray.Select(int.Parse).Sum();
    }

    //5.varianta
    public int Add5(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        string delimiter = ",";

        if (numbers.StartsWith("//"))
        {
            // Oddělovač může být mezi [ ] pro libovolnou délku
            var delimiterEndIndex = numbers.IndexOf('\n');
            var delimiterDefinition = numbers.Substring(2, delimiterEndIndex - 2);

            if (delimiterDefinition.StartsWith("[") && delimiterDefinition.EndsWith("]"))
            {
                delimiter = delimiterDefinition.Trim('[', ']');
            }
            else
            {
                delimiter = delimiterDefinition;
            }

            // Odstraníme část s definicí oddělovače a zbytek jsou čísla
            numbers = numbers.Substring(delimiterEndIndex + 1);
        }

        // Rozdělení na základě určeného oddělovače a nových řádků
        var numberArray = numbers.Split(new[] { delimiter, "\n" }, StringSplitOptions.None)
                                 .Select(int.Parse)
                                 .ToList();

        // Vyhledání negativních čísel
        var negativeNumbers = numberArray.Where(n => n < 0).ToList();
        if (negativeNumbers.Any())
        {
            throw new ArgumentException($"Negatives not allowed: {string.Join(",", negativeNumbers)}");
        }

        return numberArray.Sum();
    }

    //6.varinta
    public int Add6(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        List<string> delimiters = new List<string> { "," };  // Standardní čárka jako oddělovač

        if (numbers.StartsWith("//"))
        {
            var delimiterEndIndex = numbers.IndexOf('\n');
            var delimiterDefinition = numbers.Substring(2, delimiterEndIndex - 2);

            // Pokud jsou oddělovače v hranatých závorkách a je jich více
            if (delimiterDefinition.Contains("["))
            {
                var delimiterParts = delimiterDefinition.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                delimiters.AddRange(delimiterParts);
            }
            else
            {
                delimiters.Add(delimiterDefinition);
            }

            // Odstraníme část s definicí oddělovače a zbytek jsou čísla
            numbers = numbers.Substring(delimiterEndIndex + 1);
        }

        // Rozdělení na základě více oddělovačů a nových řádků
        var numberArray = numbers.Split(delimiters.Concat(new[] { "\n" }).ToArray(), StringSplitOptions.None)
                                 .Select(int.Parse)
                                 .ToList();

        // Vyhledání negativních čísel
        var negativeNumbers = numberArray.Where(n => n < 0).ToList();
        if (negativeNumbers.Any())
        {
            throw new ArgumentException($"Negatives not allowed: {string.Join(",", negativeNumbers)}");
        }

        return numberArray.Sum();
    }
}
