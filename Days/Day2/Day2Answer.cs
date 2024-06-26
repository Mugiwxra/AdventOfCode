using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using AdventOfCode;

namespace AdventOfCode.Days.Day2;

public class Day2Part1 : Runner
{
    public  int getResultOfDay2()
    {
        int sumOfIDs = 0;
        string path = @"C:\Users\El-Faour\Documents\Projekte\Junk\AdventOfCodeDay1\Days\Day2\Games.txt";

        List<string> lines = new List<string>();

        lines.AddRange(File.ReadAllLines(path));

        foreach (string line in lines)
        {
           sumOfIDs+= sliceLine(line);
        }
        return sumOfIDs;
    }

    public  bool Round(string cube)
    {
        //Cube _cube = new();
       

        string[] colors= cube.Split(',');
        foreach (string color in colors)
        {
         
        string[] CubeParts = color.Trim().Split(" ");


            switch (CubeParts[1])
            {
                case "green":
                    if (int.Parse(CubeParts[0]) > 13)
                        return false;
                    break;
                case "red":
                    if (int.Parse(CubeParts[0]) > 12) 
                        return false;
                    break;
                case "blue":
                    if (int.Parse(CubeParts[0]) > 14)
                        return false;
                    break;
            }

        }
        return true;
    }

    public int Run()
    {
        var res = getResultOfDay2();
        Console.WriteLine(res);
        return res;
    }

    public int sliceLine(string line)
    {
        string patternPart1 = @"(\s?\d+\s\w+,?)+";
        string[] parts = line.Split(':');
        int  ID= int.Parse(parts[0].Split(' ')[1]);   

        MatchCollection matches = Regex.Matches(parts[1], patternPart1);
        
        foreach (Match match in matches)
        {

            if (!Round(match.Value)) 
                return 0;
        }
         
        
        return ID;
    }

    
}


public class Day2Part2 : Runner
{
  
    public int getResultOfDay2()
    {
        int sum = 0;
        string path = @"C:\Users\El-Faour\Documents\Projekte\Junk\AdventOfCodeDay1\Days\Day2\Games.txt";

        List<string> lines = new List<string>();

        lines.AddRange(File.ReadAllLines(path));

        foreach (string line in lines)
        {
            sum += sliceLine(line);
        }
        return sum;
    }

    public int Run()
    {
        var res = getResultOfDay2();
        Console.WriteLine(res);
        return res;
    }

    public int sliceLine(string line)
    {
        string pattern = @"\d+\s\w+";
        string[] parts = line.Split(':');
        int ID = int.Parse(parts[0].Split(' ')[1]);
        int maxGreen = 0;
        int maxRed = 0;
        int maxBlue = 0;

        MatchCollection matches = Regex.Matches(parts[1], pattern);

        foreach (Match match in matches)
        {
            string[] CubeParts = match.ToString().Split(" ");
            int num = int.Parse(CubeParts[0]);

            switch (CubeParts[1])
            {
                case "green":
                    if (num > maxGreen) maxGreen = num;
                    break;
                case "red":
                    if (num > maxRed) maxRed = num;
                    break;
                case "blue":
                    if (num > maxBlue) maxBlue= num;
                    break;
            }

        }


        return maxBlue*maxGreen*maxRed;
    }


}
