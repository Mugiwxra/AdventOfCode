using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeMy.Days;

public class Koordinate
{
    public decimal xPosition { get; set; }
    public decimal yPosition { get; set; }
    public decimal xVelocity { get; set; }
    public decimal yVelocity { get; set; }
}

public class FunktionAttributes
{
    public decimal mValue { get; set; }
    public decimal bValue { get; set; }
    public Koordinate? koordinateAtStart { get; set; }   
}


public class Intersection
{
    public decimal xValue { get; set; }
    public decimal yValue { get; set; }
}
