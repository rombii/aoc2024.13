const long Shift = 10000000000000; // Change this to 0 for part 1

using var inputReader = new StreamReader(Path.Join(Directory.GetCurrentDirectory(), "input.txt"));
long total = 0;

while (ReadMachine(inputReader, out var buttonA, out var buttonB, out var prizeX, out var prizeY))
{
    var price = Solve(buttonA, buttonB, prizeX, prizeY);
    if (price > 0)
    {
        total += price;
    }
}

Console.WriteLine($"Answer: {total}");
return;

long Solve((int X, int Y) buttonA, (int X, int Y) buttonB, long prizeX, long prizeY)
{
    var prizeXDouble = Shift + prizeX;
    var prizeYDouble = Shift + prizeY;

    var buttonAX = (double)buttonA.X;
    var buttonAY = (double)buttonA.Y;
    var buttonBX = (double)buttonB.X;
    var buttonBY = (double)buttonB.Y;

    long b = (long)Math.Round((prizeYDouble - (prizeXDouble / buttonAX) * buttonAY) / (buttonBY - (buttonBX / buttonAX) * buttonAY));
    long a = (long)Math.Round((prizeXDouble - b * buttonBX) / buttonAX);

    var actualX = a * buttonAX + b * buttonBX;
    var actualY = a * buttonAY + b * buttonBY;

    double TOLERANCE = 0.000001;
    if (Math.Abs(actualX - prizeXDouble) < TOLERANCE && Math.Abs(actualY - prizeYDouble) < TOLERANCE && a >= 0 && b >= 0)
    {
        return a * 3 + b;
    }
    
    return -1;
}

bool ReadMachine(StreamReader reader, out (int X, int Y) buttonA, out (int X, int Y) buttonB, out long prizeX, out long prizeY)
{
    buttonA = default;
    buttonB = default;
    prizeX = 0;
    prizeY = 0;

    var line = reader.ReadLine();
    if (line == null)
    {
        return false;
    }

    if (string.IsNullOrWhiteSpace(line))
    {
        line = reader.ReadLine();
    }

    var buttonALine = line;
    var buttonBLine = reader.ReadLine();
    var prizeLine = reader.ReadLine();

    var parts = buttonALine.Split(":")[1].Split(',');
    var buttonAX = int.Parse(parts[0].Trim().Substring(2));
    var buttonAY = int.Parse(parts[1].Trim().Substring(2));

    parts = buttonBLine.Split(":")[1].Split(',');
    var buttonBX = int.Parse(parts[0].Trim().Substring(2));
    var buttonBY = int.Parse(parts[1].Trim().Substring(2));

    parts = prizeLine.Split(":")[1].Split(',');
    prizeX = int.Parse(parts[0].Trim().Substring(2));
    prizeY = int.Parse(parts[1].Trim().Substring(2));

    buttonA = (buttonAX, buttonAY);
    buttonB = (buttonBX, buttonBY);

    return true;
}