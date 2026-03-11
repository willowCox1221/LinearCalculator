using System;
using System.Text;

namespace LinearCalculator.Unit_Three
{
    public partial class OrthonormalCheckerPage : ContentPage
    {
        public OrthonormalCheckerPage()
        {
            InitializeComponent();
        }

        private void OnCheckOrthonormalClicked(object sender, EventArgs e)
        {
            try
            {
                double a1 = double.Parse(A1.Text);
                double a2 = double.Parse(A2.Text);
                double a3 = double.Parse(A3.Text);

                double b1 = double.Parse(B1.Text);
                double b2 = double.Parse(B2.Text);
                double b3 = double.Parse(B3.Text);

                StringBuilder steps = new StringBuilder();

                steps.AppendLine("ORTHONORMAL CHECK\n");

                steps.AppendLine($"A = ({a1}, {a2}, {a3})");
                steps.AppendLine($"B = ({b1}, {b2}, {b3})\n");

                // Step 1 Dot Product
                steps.AppendLine("Step 1: Check Orthogonality");
                steps.AppendLine("A · B = a₁b₁ + a₂b₂ + a₃b₃");

                double dot = (a1 * b1) + (a2 * b2) + (a3 * b3);

                steps.AppendLine($"A · B = ({a1}*{b1}) + ({a2}*{b2}) + ({a3}*{b3})");
                steps.AppendLine($"A · B = {ToFraction(dot)}\n");

                // Step 2 Magnitude A
                steps.AppendLine("Step 2: Magnitude of A");

                double magA = Math.Sqrt(a1 * a1 + a2 * a2 + a3 * a3);

                steps.AppendLine($"|A| = √({a1}² + {a2}² + {a3}²)");
                steps.AppendLine($"|A| = {ToFraction(magA)}\n");

                // Step 3 Magnitude B
                steps.AppendLine("Step 3: Magnitude of B");

                double magB = Math.Sqrt(b1 * b1 + b2 * b2 + b3 * b3);

                steps.AppendLine($"|B| = √({b1}² + {b2}² + {b3}²)");
                steps.AppendLine($"|B| = {ToFraction(magB)}\n");

                // Step 4 Final Check
                steps.AppendLine("Step 4: Check Conditions");

                bool orthogonal = Math.Abs(dot) < 1e-10;
                bool unitA = Math.Abs(magA - 1) < 1e-10;
                bool unitB = Math.Abs(magB - 1) < 1e-10;

                if (orthogonal && unitA && unitB)
                {
                    steps.AppendLine("Vectors ARE orthonormal.");
                }
                else
                {
                    steps.AppendLine("Vectors are NOT orthonormal.");
                }

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }

        private string ToFraction(double value)
        {
            if (Math.Abs(value) < 1e-10)
                return "0";

            if (Math.Abs(value % 1) < 1e-10)
                return ((int)Math.Round(value)).ToString();

            int denominator = 1000;
            int numerator = (int)Math.Round(value * denominator);

            int gcd = GCD(Math.Abs(numerator), denominator);

            numerator /= gcd;
            denominator /= gcd;

            return $"{numerator}/{denominator}";
        }

        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}