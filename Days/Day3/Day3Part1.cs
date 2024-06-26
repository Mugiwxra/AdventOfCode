using System.Text.RegularExpressions;
using AdventOfCode;
namespace AdventOfCodeMy.Days.Day3;

public class Day3Part1 : Runner
{

    public int Run()
    {
        MatchCollection matchesNums;
        
        MatchCollection matchesSymbsPre;
        MatchCollection matchesSymbs;
        MatchCollection matchesSymbsPost;



        List<Symbol> foundSymbsPost = new();
        List<Symbol> foundSymbsPre = new();
        List<Symbol> foundSymbs=new();
        string output = @"C:\Users\El-Faour\Documents\Projekte\Junk\AdventOfCodeDay1\Days\Day3\output.txt";
        string path = @"C:\Users\El-Faour\Documents\Projekte\Junk\AdventOfCodeDay1\Days\Day3\Day3Input.txt";

        List<string> lines = [];

        string patternNumsOnly= @"\d+";
        string patternSymbols= @"[^.|\n|\d]";


        lines.AddRange(File.ReadAllLines(path));
        int sum = 0;
        using (StreamWriter writer = new StreamWriter(output, true))
        {
            for (int i = 0; i < lines.Count; i++)
            {
                matchesNums = Regex.Matches(lines[i], patternNumsOnly);
                List<Number> foundNums = handlerNums(matchesNums, lines[i]);


                matchesSymbs = Regex.Matches(lines[i], patternSymbols);
                foundSymbs = handlerSymbs(matchesSymbs, lines[i]);

                if (i < lines.Count - 1)
                {
                    matchesSymbsPost = Regex.Matches(lines[i + 1], patternSymbols);
                    foundSymbsPost = handlerSymbs(matchesSymbsPost, lines[i + 1]);
                }

                if (i != 0)
                {
                    matchesSymbsPre = Regex.Matches(lines[i - 1], patternSymbols);
                    foundSymbsPre = handlerSymbs(matchesSymbsPre, lines[i - 1]);
                }

                List<Symbol> allSymbs = [];
                allSymbs.AddRange(foundSymbs);
                allSymbs.AddRange(foundSymbsPre);
                allSymbs.AddRange(foundSymbsPost);

                foreach (Number foundNum in foundNums)
                {
                    int indexStart = foundNum.startIndex - 1;
                    int indexEnd = foundNum.endIndex;

                    foreach (Symbol symb in allSymbs)
                    {
                        writer.WriteLine($"Number => {foundNum.number} Start => {indexStart}  End => {indexEnd}  Symbolindex => {symb.Index} Symbol => {symb.symbol}");
                    }

                    if (allSymbs.Any(x => x.Index >= indexStart && x.Index <= indexEnd))
                    {
                        //var sy =allSymbs.FirstOrDefault(x => x.startIndex >= indexStart && x.startIndex <= indexEnd);
                        //writer.WriteLine($"This Number {foundNum.number} has contact to this symbol {sy?.symbol}");
                        sum += foundNum.number;
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

    private List<Number> handlerNums(MatchCollection nums,string line)
    {
        int deletedindex = 0;
        List<Number> numbers = [];
        foreach(Match num in nums)
        {
            Number number = new();
            number.number = int.Parse(num.Value);
            number.startIndex= line.IndexOf(num.Value);
            number.endIndex = number.startIndex + num.Length;
            string points = new string('.', num.Value.ToString().Length);

            line = ReplaceFirst(line,num.Value, points);
            
            numbers.Add(number);
        }
        return numbers;
    }

    private List<Symbol> handlerSymbs(MatchCollection symbols, string line)
    {
        int deletedindex = 0;
        List<Symbol> _symbols = [];
        foreach (Match x in symbols)
        {
            Symbol symbol = new();
            symbol.Index = line.IndexOf(x.Value)+deletedindex;
            symbol.symbol=x.Value;
            _symbols.Add(symbol);
            line = ReplaceFirst( line,x.Value, ".");

        }
        return _symbols;
    }
   


}
