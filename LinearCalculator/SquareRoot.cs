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

            public SquareRoot(int value)
            {
                Simplify(value);
            }

            public SquareRoot(Fraction coefficient, int radicand)
            {
                Coefficient = coefficient;
                Radicand = radicand;
            }

            private void Simplify(int value)
            {
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
                if (Radicand == 1)
                    return Coefficient.ToString();

                if (Coefficient.Numerator == 1 && Coefficient.Denominator == 1)
                    return $"√{Radicand}";

                return $"{Coefficient}√{Radicand}";
            }

            public static SquareRoot operator *(SquareRoot a, SquareRoot b)
            {
                Fraction newCoeff = a.Coefficient * b.Coefficient;
                int newRad = a.Radicand * b.Radicand;

                return new SquareRoot(newCoeff, newRad).Simplified();
            }
            private SquareRoot Simplified()
            {
                SquareRoot result = new SquareRoot(1);
                result.Simplify(Radicand);

                result.Coefficient *= this.Coefficient;
                return result;
            }
        }
    }
}
