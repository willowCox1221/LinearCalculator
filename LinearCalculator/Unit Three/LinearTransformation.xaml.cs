using System;
using System.Text;

namespace LinearCalculator.Unit_Three
{
    public partial class LinearTransformation : ContentPage
    {
        public LinearTransformation()
        {
            InitializeComponent();
        }

        private void OnLinearTransformaionClicked(object sender, EventArgs e)
        {
            try
            {
                double a = double.Parse(A11.Text);
                double b = double.Parse(A12.Text);
                double c = double.Parse(A21.Text);
                double d = double.Parse(A22.Text);

                double x = double.Parse(VectorX.Text);
                double y = double.Parse(VectorY.Text);

                StringBuilder steps = new StringBuilder();

                steps.AppendLine("LINEAR TRANSFORMATION\n");

                steps.AppendLine("Matrix A:");
                steps.AppendLine($"[{a} {b}]");
                steps.AppendLine($"[{c} {d}]\n");

                steps.AppendLine("Vector v:");
                steps.AppendLine($"[{x}]");
                steps.AppendLine($"[{y}]\n");

                steps.AppendLine("Step 1: Multiply Matrix by Vector\n");

                double result1 = a * x + b * y;
                double result2 = c * x + d * y;

                steps.AppendLine($"Row1: ({a}*{x}) + ({b}*{y}) = {result1}");
                steps.AppendLine($"Row2: ({c}*{x}) + ({d}*{y}) = {result2}\n");

                steps.AppendLine("Result:");

                steps.AppendLine($"T(v) = [{result1}]");
                steps.AppendLine($"       [{result2}]");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
    }
}