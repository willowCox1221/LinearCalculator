using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearCalculator;

public struct Fraction
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Fraction(int numerator, int denominator = 1)
    {
        if (denominator == 0)
            throw new DivideByZeroException();

        // keep denominator positive
        if (denominator < 0)
        {
            numerator = -numerator;
            denominator = -denominator;
        }

        int gcd = GCD(Math.Abs(numerator), denominator);
        Numerator = numerator / gcd;
        Denominator = denominator / gcd;
    }

    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // Operators
    public static Fraction operator +(Fraction a, Fraction b) =>
        new(a.Numerator * b.Denominator + b.Numerator * a.Denominator,
            a.Denominator * b.Denominator);

    public static Fraction operator -(Fraction a, Fraction b) =>
        new(a.Numerator * b.Denominator - b.Numerator * a.Denominator,
            a.Denominator * b.Denominator);

    public static Fraction operator *(Fraction a, Fraction b) =>
        new(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

    public static Fraction operator /(Fraction a, Fraction b) =>
        new(a.Numerator * b.Denominator, a.Denominator * b.Numerator);

    public override string ToString() =>
        Denominator == 1 ? Numerator.ToString() : $"{Numerator}/{Denominator}";
}