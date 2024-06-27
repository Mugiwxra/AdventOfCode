using System.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Days.Day1;
using System.Text.RegularExpressions;
using System.Diagnostics;
using AdventOfCode.Days.Day2;
using AdventOfCodeMy.Days.Day3;
using AdventOfCode;
using AdventOfCodeMy.Days;
//Console.WriteLine(Calcer.getResultOfDay1());


/*
    ^ - Starts with
    $ - Ends with
    [] - Range
    () - Group
    . - Single character once
    + - one or more characters in a row
    ? - optional preceding character match
    \ - escape character  
    \n - New line
    \d -  Digit
    \s - White space
    \S - won-white spase
    \w - alphanumeric/underscore chacracter (word chars)
    \W - non-word chars
    {x/y} - Repeat low (x) to high (y) (no "y" means at least x, no ",y" means that many)
    (x|y) - Alternative - x or y

    [^x] - Anything but x (where x is whatever character you want)
 */

Runner run = new Day24Part1();
Console.WriteLine(run.Run());
