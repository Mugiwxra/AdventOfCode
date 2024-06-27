using AdventOfCode;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
namespace AdventOfCodeMy.Days;

public class Day24Part1 : Runner
{
    string filePath = @"C:\Users\El-Faour\Documents\Projekte\Junk\AdventOfCode\Days\Day24\Day24Input.txt";
    List<string> lines = [];
    decimal lowerBound = 200000000000000;
    decimal upperBound = 400000000000000;
    private int _intersectionsInBoundsCounter = 0;


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
            linearFunktion.koordinateAtStart = koordinate;
            LinearFunktions.Add(linearFunktion);
        }
        
        for(int i =0; i<LinearFunktions.Count;i++)
        {
            for (int j=i+1; j<LinearFunktions.Count;j++)
            {
                Intersection? Intersection = calcIntersections(LinearFunktions[i], LinearFunktions[j]);
                if(Intersection!=null)
                {
                    //Console.WriteLine($"Punkt A: ({LinearFunktions[i].koordinateAtStart.xPosition}|{LinearFunktions[i].koordinateAtStart.yPosition}) Punkt B: ({LinearFunktions[j].koordinateAtStart.xPosition}|{LinearFunktions[j].koordinateAtStart.yPosition})" +
                    //    $"Schnittpunkt: ({Intersection.xValue}|{Intersection.yValue})");
                    if (isIntersectionInBounds(Intersection, LinearFunktions[i], LinearFunktions[j]) )
                        _intersectionsInBoundsCounter++;
                }
            }
        }


        return _intersectionsInBoundsCounter;
    }

    private bool isIntersectionInBounds(Intersection Intersection, FunktionAttributes aLine, FunktionAttributes bLine)
    {
        if(Intersection.xValue <lowerBound || Intersection.xValue>upperBound)
        {
            return false;
        }

        if (Intersection.yValue < lowerBound || Intersection.yValue > upperBound)
        {
            return false;
        }

        //Positive slope
        if(aLine.mValue>0 && Intersection.xValue <aLine.koordinateAtStart?.xPosition)
        {
            //Intersection in the past
            return false;
        }
        
        if (aLine.mValue < 0 && Intersection.xValue > aLine.koordinateAtStart?.xPosition)
        {
            //Intersection in the past

            return false;
        }
       
        //Positive slope
        if (bLine.mValue > 0 && Intersection.xValue < bLine.koordinateAtStart?.xPosition)
        {
            //Intersection in the past
            return false;
        }

        if (bLine.mValue < 0 && Intersection.xValue > bLine.koordinateAtStart?.xPosition)
        {
            //Intersection in the past

            return false;
        }
        return true;

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
    
    private Intersection? calcIntersections(FunktionAttributes gradeA, FunktionAttributes gradeB)
    {
        Intersection koordinate = new();


        //m1x+b1=m2x+b2
        //m1x +b1 -b2 =mx2 -m1x
        //b1-b2=m2x-m1x=(m2-m1)x
        //(b1-b2)/(m2-m1)=x

        
        if (gradeA.mValue == gradeB.mValue) 
            return null;


        koordinate.xValue = (gradeA.bValue - gradeB.bValue) / (gradeB.mValue - gradeA.mValue);
        // y = mx+b
        koordinate.yValue = gradeA.mValue * koordinate.xValue + gradeA.bValue;

        return koordinate;

    }

    private List<Koordinate> calculateKoordinations (Koordinate koordinate)
    {
        List<Koordinate> koordinates = [];

        koordinates.Add(new Koordinate
        {
            xPosition = koordinate.xPosition,
            yPosition = koordinate.yPosition,
            xVelocity = koordinate.xVelocity,
            yVelocity = koordinate.yVelocity
        });

       

        koordinate.xPosition = koordinate.xPosition + koordinate.xVelocity;
        koordinate.yPosition = koordinate.yPosition + koordinate.yVelocity;

        //bool checkLowerBound = koordinate.xPosition < lowerBound || koordinate.yPosition < lowerBound;
        //bool checkUpperBound = koordinate.xPosition > upperBound || koordinate.yPosition > upperBound;

        //if(checkLowerBound||checkLowerBound)
        //{
        //    return koordinates;
        //}


        koordinates.Add(new Koordinate
        {
            xPosition = koordinate.xPosition,
            yPosition = koordinate.yPosition,
            xVelocity = koordinate.xVelocity,
            yVelocity = koordinate.yVelocity
        });



        return koordinates;
    }


    







}
