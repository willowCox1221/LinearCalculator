using System;
using System.Text;

namespace LinearCalculator.Unit_Two { 
    public partial class UnitVectorPage : ContentPage
    {
        public UnitVectorPage()
        {
            InitializeComponent();
        }

        private void OnUnitVectorClicked(object sender, EventArgs e)
        {
            try
            {
                double x = double.Parse(VectorX.Text);
                double y = double.Parse(VectorY.Text);
                double z = double.Parse(VectorZ.Text);

                StringBuilder steps = new StringBuilder();

                steps.AppendLine("UNIT VECTOR STEPS\n");

                // Step 1
                steps.AppendLine($"Step 1: Original Vector");
                steps.AppendLine($"v = ({x}, {y}, {z})\n");

                // Step 2 - Magnitude formula
                steps.AppendLine("Step 2: Find Magnitude");
                steps.AppendLine("|v| = √(x² + y² + z²)");
                steps.AppendLine($"|v| = √({x}² + {y}² + {z}²)");

                double magnitude = Math.Sqrt(x * x + y * y + z * z);

                steps.AppendLine($"|v| = √({x * x + y * y + z * z})");
                steps.AppendLine($"|v| = {magnitude:F3}\n");

                if (magnitude == 0)
                {
                    ResultLabel.Text = "Unit Vector: Undefined (zero vector)";
                    return;
                }

                // Step 3 - Divide components
                steps.AppendLine("Step 3: Divide Each Component by Magnitude");

                double ux = x / magnitude;
                double uy = y / magnitude;
                double uz = z / magnitude;

                steps.AppendLine($"({x}/{ToFraction(magnitude)}, {y}/{ToFraction(magnitude)}, {z}/{ToFraction(magnitude)})\n");

                // Step 4 - Final answer
                steps.AppendLine("Step 4: Unit Vector");
                steps.AppendLine($"û = ({ToFraction(ux)}, {ToFraction(uy)}, {ToFraction(uz)})");

                MagnitudeLabel.Text = $"Magnitude: {magnitude:F3}";
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