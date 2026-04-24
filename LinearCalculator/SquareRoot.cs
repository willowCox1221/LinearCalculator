using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearCalculator
{
    namespace LinearCalculator
    {
        internal class SquareRoot
        {
            public Fraction Coefficient { get; private set; }
            public int Radicand { get; private set; }
            public bool IsImaginary { get; private set; }

            public SquareRoot(int value)
            {
                Simplify(value);
            }

            public SquareRoot(Fraction coefficient, int radicand, bool isImaginary = false)
            {
                Coefficient = coefficient;
                Radicand = radicand;
                IsImaginary = isImaginary;
            }

            private void Simplify(int value)
            {
                if (value == 0)
                {
                    Coefficient = new Fraction(0);
                    Radicand = 1;
                    IsImaginary = false;
                    return;
                }

                if (value < 0)
                {
                    IsImaginary = true;
                    value = -value; // make positive for simplification
                }
                else
                {
                    IsImaginary = false;
                }

                int outside = 1;
                int inside = value;

                for (int i = 2; i * i <= inside; i++)
                {
                    while (inside % (i * i) == 0)
                    {
                        outside *= i;
                        inside /= (i * i);
                    }
                }

                Coefficient = new Fraction(outside);
                Radicand = inside;
            }
            public override string ToString()
            {
                string basePart;

                if (Radicand == 1)
                    basePart = Coefficient.ToString();
                else if (Coefficient.Numerator == 1 && Coefficient.Denominator == 1)
                    basePart = $"√{Radicand}";
                else
                    basePart = $"{Coefficient}√{Radicand}";

                if (IsImaginary)
                {
                    if (basePart == "1")
                        return "i";

                    return basePart + "i";
                }

                return basePart;
            }

            public static SquareRoot operator *(SquareRoot a, SquareRoot b)
            {
                Fraction newCoeff = a.Coefficient * b.Coefficient;
                int newRad = a.Radicand * b.Radicand;

                bool newImaginary = a.IsImaginary ^ b.IsImaginary; // XOR

                // If both are imaginary → i * i = -1
                if (a.IsImaginary && b.IsImaginary)
                {
                    newCoeff *= new Fraction(-1);
                }

                SquareRoot result = new SquareRoot(newCoeff, newRad, newImaginary);
                return result.Simplified();
            }
            private SquareRoot Simplified()
            {
                SquareRoot temp = new SquareRoot(1);
                temp.Simplify(this.Radicand);

                temp.Coefficient *= this.Coefficient;
                temp.IsImaginary = this.IsImaginary;

                return temp;
            }
        }
    }
}
