using AdventOfCode;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
namespace AdventOfCodeMy.Days;

public class Day4Part1 : Runner
{
    string filePath = @"C:\Users\49176\Documents\Projekte\AdventOfCode\Days\Day24\Day4Input.txt";
    List<string> lines = [];
    decimal lowerBound = 7;
    decimal upperBound = 27;
    public int Run()
    {
        List<Koordinate> koordinates = new List<Koordinate>();

        lines.AddRange(File.ReadAllLines(filePath));


        string regexPattern = @"\d+|-\d+";

        foreach (var line in lines)
        {
            MatchCollection matches = Regex.Matches(line, regexPattern);

            Koordinate koordinate = new();

            koordinate.xPosition = decimal.Parse(matches[0].Value);
            koordinate.yPosition = decimal.Parse(matches[1].Value);
            koordinate.xVelocity = decimal.Parse(matches[3].Value);
            koordinate.yVelocity = decimal.Parse(matches[4].Value);
            koordinates.Add(koordinate);

        }
        List<FunktionAttributes> LinearFunktions = [];
        
        foreach (Koordinate koordinate in koordinates)
        {
            List<Koordinate> calculatedKoordinations = calculateKoordinations(koordinate);
            if (calculatedKoordinations.Count < 2) continue;

            FunktionAttributes linearFunktion = calcLinearFunktion(calculatedKoordinations[0], calculatedKoordinations[1]);
            LinearFunktions.Add(linearFunktion);
        }
        
        for(int i =0; i<LinearFunktions.Count;i++)
        {
            for (int j=i+1; j<LinearFunktions.Count;j++)
            {
               Koordinate Intersection = calcIntersections(LinearFunktions[i], LinearFunktions[j]);

            }
        }


        return 0;
    }


    private FunktionAttributes calcLinearFunktion(Koordinate aPoint, Koordinate bPoint)
    {
        FunktionAttributes Linear = new();

     //y=mx+b
     //y-mb=b   

        decimal gradient = (aPoint.yPosition-bPoint.yPosition) / (aPoint.xPosition - bPoint.xPosition);
        decimal bVal = aPoint.yPosition - gradient * aPoint.xPosition;

        Linear.mValue = gradient;
        Linear.bValue = bVal;

        return Linear;
    }
    
    private Koordinate calcIntersections(FunktionAttributes gradeA, FunktionAttributes gradeB)
    {
        Koordinate koordinate = new();


        //m1x+b1=m2x+b2
        //m1*x+b1-b2=m2*x
        //b1-b2 = (m1- m2)*x
        //(b1-b2)/(m1-m2)=x
        if (gradeA.mValue == gradeB.mValue) return null;


        koordinate.xPosition = (gradeA.bValue - gradeB.bValue) / (gradeA.mValue - gradeB.mValue);


        // y = mx+b
        koordinate.yPosition = gradeA.mValue * koordinate.xPosition + gradeA.bValue;

        return koordinate;

    }

    private List<Koordinate> calculateKoordinations (Koordinate koordinate)
    {
        List<Koordinate> koordinates = [];
        decimal xPositionAtT0 = koordinate.xPosition;
        decimal yPositionAtT0 = koordinate.yPosition;

        //If one of those is true, bounds got exceeded
        while (true)
        {
            bool checkLowerBound = koordinate.xPosition < lowerBound || koordinate.yPosition < lowerBound;
            bool checkUpperBound = koordinate.xPosition > upperBound || koordinate.yPosition > upperBound;
            
            if (checkLowerBound || checkLowerBound) break;


            koordinates.Add(new Koordinate
            {
                xPosition = koordinate.xPosition,
                yPosition = koordinate.yPosition,
                xVelocity = koordinate.xVelocity,
                yVelocity = koordinate.yVelocity
            });


            koordinate.xPosition = koordinate.xPosition + koordinate.xVelocity;
            koordinate.yPosition = koordinate.yPosition + koordinate.yVelocity;



        }

    
       
        return koordinates;
    }


    







}
