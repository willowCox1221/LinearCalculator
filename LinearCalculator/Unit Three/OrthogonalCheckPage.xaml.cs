using System;
using System.Text;

namespace LinearCalculator.Unit_Three
{
    public partial class OrthogonalCheckPage : ContentPage
    {
        public OrthogonalCheckPage()
        {
            InitializeComponent();
        }

        private void OnCheckOrthogonalClicked(object sender, EventArgs e)
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

                steps.AppendLine("ORTHOGONAL CHECK\n");

                steps.AppendLine($"A = ({a1}, {a2}, {a3})");
                steps.AppendLine($"B = ({b1}, {b2}, {b3})\n");

                steps.AppendLine("Step 1: Use Dot Product Formula");
                steps.AppendLine("A · B = a₁b₁ + a₂b₂ + a₃b₃\n");

                steps.AppendLine($"A · B = ({a1} * {b1}) + ({a2} * {b2}) + ({a3} * {b3})");

                double dot = (a1 * b1) + (a2 * b2) + (a3 * b3);

                steps.AppendLine($"A · B = {ToFraction(dot)}\n");

                steps.AppendLine("Step 2: Check If Dot Product = 0");

                if (Math.Abs(dot) < 1e-10)
                {
                    steps.AppendLine("Since A · B = 0, the vectors ARE orthogonal.");
                }
                else
                {
                    steps.AppendLine("Since A · B ≠ 0, the vectors are NOT orthogonal.");
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