using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeMy.Days.Day3;
public class Number
{
    public int startIndex { get; set; } = int.MinValue;
    public int endIndex { get; set; }
    public int number { get; set; }


}

public class Symbol
{
    public int Index { get; set; } = int.MinValue;
    public string symbol { get; set; } = string.Empty;

}