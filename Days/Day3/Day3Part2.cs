using System.Text.RegularExpressions;
using AdventOfCode;
namespace AdventOfCodeMy.Days.Day3;

public class Day3Part2 : Runner
{

    public int Run()
    {
        MatchCollection matchesNumsPre;
        MatchCollection matchesNums;
        MatchCollection matchesNumsPost;

        MatchCollection matchesSymbs;
        List<Symbol> foundSymbs= new();



        List<Number> foundNumbersPost = new();
        List<Number> foundNumbersPre = new();
        List<Number> foundNumbers = new();
        string output = @"C:\Users\El-Faour\Documents\Projekte\Junk\AdventOfCodeDay1\Days\Day3\output.txt";
        string path = @"C:\Users\El-Faour\Documents\Projekte\Junk\AdventOfCodeDay1\Days\Day3\Day3Input.txt";

        List<string> lines = [];

        string patternNumsOnly = @"\d+";
        string patternSymbols = @"[*]";


        lines.AddRange(File.ReadAllLines(path));
        int sum = 0;
        using (StreamWriter writer = new StreamWriter(output, true))
        {
            for (int i = 0; i < lines.Count; i++)
            {

                matchesSymbs = Regex.Matches(lines[i], patternSymbols);
                foundSymbs = handlerSymbs(matchesSymbs, lines[i]);


                matchesNums = Regex.Matches(lines[i], patternNumsOnly);
                foundNumbers = handlerNums(matchesNums, lines[i]);

                if (i < lines.Count - 1)
                {
                    matchesNumsPost = Regex.Matches(lines[i + 1], patternNumsOnly);
                    foundNumbersPost = handlerNums(matchesNumsPost, lines[i + 1]);
                }

                if (i != 0)
                {
                    matchesNumsPre = Regex.Matches(lines[i - 1], patternNumsOnly);
                    foundNumbersPre = handlerNums(matchesNumsPre, lines[i - 1]);
                }

                List<Number> allNums = [];
                allNums.AddRange(foundNumbers);
                allNums.AddRange(foundNumbersPre);
                allNums.AddRange(foundNumbersPost);

                foreach (Symbol foundSymbol in foundSymbs)
                {
                    int index = foundSymbol.Index;
                    
                    if(allNums.Any(x => index >= x.startIndex && index <= x.endIndex))
                    {

                        List<Number> connectedNums=allNums.Where(x => index >= x.startIndex && index <= x.endIndex).ToList();
                        if(connectedNums.Count()!=2)
                        {
                            continue;
                        }
                        else
                        {
                            sum += connectedNums[0].number * connectedNums[1].number;
                        }
                    }


                }

            }
        }

        return sum;
    }
    public static string ReplaceFirst(string text, string search, string replace)
    {
        int index = text.IndexOf(search);
        if (index < 0)
        {
            return text; // Falls der Suchstring nicht gefunden wurde, das Original zurückgeben
        }
        return text.Substring(0, index) + replace + text.Substring(index + search.Length);
    }

    private List<Number> handlerNums(MatchCollection nums, string line)
    {
        int deletedindex = 0;
        List<Number> numbers = [];
        foreach (Match num in nums)
        {
            Number symbol = new();
            symbol.number = int.Parse(num.Value);
            symbol.startIndex = line.IndexOf(num.Value)-1;
            symbol.endIndex = symbol.startIndex + num.Length+1;
            deletedindex = num.Value.ToString().Length;
            string points = new string('.', num.Value.ToString().Length);

            line = ReplaceFirst(line, num.Value, points);

            numbers.Add(symbol);
        }
        return numbers;
    }

    private List<Symbol> handlerSymbs(MatchCollection symbols, string line)
    {
        List<Symbol> _symbols = [];
        foreach (Match x in symbols)
        {
            Symbol symbol = new();
            symbol.Index = line.IndexOf(x.Value);
            symbol.symbol = x.Value;
            _symbols.Add(symbol);
            line = ReplaceFirst(line, x.Value, ".");

        }
        return _symbols;
    }



}
