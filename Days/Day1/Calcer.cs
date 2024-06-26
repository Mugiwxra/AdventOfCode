using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Day1;

public class Calcer
{

    static Dictionary<string, string>  numberWords = new Dictionary<string, string>
    {
        { "zero", "0" },
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" }
    };
    public static int getNum(string randomstring)
    {

        myNum firstNum = new();
        myNum secondNum = new();

        myNum firstChar = getFirstAsChar(randomstring);
        myNum firstString = getFirstAsString(randomstring);

        firstNum = firstChar.firstIndex < firstString.firstIndex ? firstChar : firstString;

        myNum secondString = getLastAsString(randomstring);

        myNum secondChar = getLastAsChar(randomstring);
        secondNum = secondChar.secondIndex > secondString.secondIndex ? secondChar : secondString;


        string firstNumString = firstNum.isChar ? firstNum.ch.ToString() : firstNum.stringNum;
        string secondNumString = secondNum.isChar ? secondNum.ch.ToString() : secondNum.stringNum;

        string wholeNum = firstNumString + secondNumString;
        int.TryParse(wholeNum, out int result);
        //Console.WriteLine(randomstring + "        " + result);
        return result;
    }


    private static myNum getFirstAsChar(string randomstirng)
    {
        myNum num = new();


        foreach (char ch in randomstirng)
        {
            num.firstIndex++;
            randomstirng.ToCharArray();
            if (char.IsDigit(ch))
            {
                num.isChar = true;
                num.ch = ch;
                return num;
            }
        }
        num.isChar = false;
        return num;
    }
    private static myNum getLastAsChar(string randomstirng)
    {
        myNum num = new();
        uint counter = 1;
        num.isChar = false;

        foreach (char ch in randomstirng)
        {
            counter++;
            if (char.IsDigit(ch))
            {
                num.secondIndex = counter;
                num.isChar = true;
                num.ch = ch;

            }
        }
        return num;
    }
    private static myNum getFirstAsString(string randomstring)
    {
        List<string> numStrings = numberWords.Keys.ToList();
        myNum num = new();
        num.isChar = true;

        foreach (string numString in numStrings)
        {
            if (randomstring.Contains(numString))
            {
                if (num.stringNum != null)
                {
                    if (randomstring.IndexOf(numString) + 1 < num.firstIndex + 1)
                    {
                        num.stringNum = convertNum(numString);
                        num.firstIndex = (uint)randomstring.IndexOf(numString) + 1;
                    }
                    else
                    {
                        continue;
                    }

                }
                num.stringNum = convertNum(numString);
                num.firstIndex = (uint)(randomstring.IndexOf(numString) + 1);
                num.isChar = false;
            }
        }
        return num;
    }

    public static int getResultOfDay1()
    {
        string path = @"C:\Users\El-Faour\Documents\Projekte\Junk\AdventOfCodeDay1\Days\Day1\AdventCode1.txt";

        List<string> lines = new List<string>();
        
        lines.AddRange(File.ReadAllLines(path));
        int num = 0;
        foreach (string line in lines)
        {
            num = getNum(line) + num;
        }

        return num;
    }

    private static myNum getLastAsString(string randomstring)
    {
        List<string> numStrings = numberWords.Keys.ToList();
        myNum num = new();
        uint index = 0;
        num.isChar = true;

        foreach (string numString in numStrings)
        {
            if (randomstring.Contains(numString))
            {
                index = getLastIndexStringNum(randomstring, numString) + 1;
                if (num.stringNum != null)
                {
                    if (index > num.secondIndex)
                    {
                        num.stringNum = convertNum(numString);
                        num.secondIndex = index + 1;
                    }
                    else
                    {
                        continue;
                    }

                }
                num.stringNum = convertNum(numString);
                num.secondIndex = index + 1;
                num.isChar = false;
            }
        }
        return num;
    }

    private static uint getLastIndexStringNum(string word, string num)
    {
        bool deleted = false;
        int deletedlength = 0;
        int index = 0;
        int numlength;
        while (word.Contains(num))
        {
            index = word.IndexOf(num);
            if (deleted)
            {

                deletedlength = num.Length + deletedlength;
            }
            word = word.Remove(index, num.Length);
            deleted = true;
        }
        return (uint)(index + deletedlength);
    }

    private static  string convertNum(string word)
    {
        return numberWords[word];
    }
}
internal class myNum
{
    public char ch { get; set; }
    public string stringNum { get; set; }

    public uint firstIndex { get; set; } = uint.MaxValue;
    public uint secondIndex { get; set; } = 1;
    public bool isChar { get; set; }
}