using System;
using System.Linq;
using System.Text;

namespace LinearCalculator.Unit_Two
{
    public partial class CrossProductPage : ContentPage
    {
        public CrossProductPage()
        {
            InitializeComponent();
        }

        private double[] ParseVector(string input)
        {
            return input
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();
        }

        private void OnCrossProductClicked(object sender, EventArgs e)
        {
            try
            {
                var a = ParseVector(VectorAInput.Text);
                var b = ParseVector(VectorBInput.Text);

                if (a.Length != 3 || b.Length != 3)
                {
                    ResultLabel.Text = "Cross product requires 3D vectors.";
                    return;
                }

                StringBuilder steps = new StringBuilder();

                steps.AppendLine("CROSS PRODUCT AND MAGNITUDE");
                steps.AppendLine($"A = {FormatVector(a)}");
                steps.AppendLine($"B = {FormatVector(b)}\n");

                // Cross product components
                double iComp = (a[1] * b[2]) - (a[2] * b[1]);
                double jComp = -((a[0] * b[2]) - (a[2] * b[0]));
                double kComp = (a[0] * b[1]) - (a[1] * b[0]);

                steps.AppendLine("Step 1: Compute Cross Product Components\n");

                steps.AppendLine($"i = ({ToFraction(a[1])}*{ToFraction(b[2])}) - ({ToFraction(a[2])}*{ToFraction(b[1])}) = {ToFraction(iComp)}");
                steps.AppendLine($"j = -[({ToFraction(a[0])}*{ToFraction(b[2])}) - ({ToFraction(a[2])}*{ToFraction(b[0])})] = {ToFraction(jComp)}");
                steps.AppendLine($"k = ({ToFraction(a[0])}*{ToFraction(b[1])}) - ({ToFraction(a[1])}*{ToFraction(b[0])}) = {ToFraction(kComp)}\n");

                double[] cross = { iComp, jComp, kComp };

                steps.AppendLine($"A × B = {FormatVector(cross)}\n");

                // Magnitude
                steps.AppendLine("Step 2: Compute Magnitude\n");

                double sumSquares =
                    iComp * iComp +
                    jComp * jComp +
                    kComp * kComp;

                steps.AppendLine($"|A × B| = √(({ToFraction(iComp)})² + ({ToFraction(jComp)})² + ({ToFraction(kComp)})²)");
                steps.AppendLine($"|A × B| = √({ToFraction(sumSquares)})");

                double magnitude = Math.Sqrt(sumSquares);

                steps.AppendLine($"|A × B| = {ToFraction(magnitude)}\n");

                // Area interpretations
                steps.AppendLine("Step 3: Geometric Meaning\n");

                steps.AppendLine($"Area of Parallelogram = {ToFraction(magnitude)}");

                double triangleArea = magnitude / 2.0;
                steps.AppendLine($"Area of Triangle = {ToFraction(triangleArea)}");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }

        private string FormatVector(double[] vector)
        {
            return $"({ToFraction(vector[0])}, {ToFraction(vector[1])}, {ToFraction(vector[2])})";
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