using System;
using System.Text;

namespace LinearCalculator.Unit_One
{
    public partial class SubstitutionPage : ContentPage
    {
        public SubstitutionPage()
        {
            InitializeComponent();
        }

        private void OnSolveClicked(object sender, EventArgs e)
        {
            try
            {
                var rows = MatrixInput.Text
                    .Trim()
                    .Split(new[] { "\r\n", "\n", "\r" },
                           StringSplitOptions.RemoveEmptyEntries);

                int rowCount = rows.Length;
                var firstRow = rows[0]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                int colCount = firstRow.Length;

                double[,] matrix = new double[rowCount, colCount];

                for (int i = 0; i < rowCount; i++)
                {
                    var values = rows[i]
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < colCount; j++)
                        matrix[i, j] = double.Parse(values[j]);
                }

                StepsLabel.Text = PerformBackSubstitution(matrix);
            }
            catch
            {
                StepsLabel.Text = "Invalid matrix input.";
            }
        }

        private string PerformBackSubstitution(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            StringBuilder steps = new StringBuilder();
            double[] solution = new double[n];

            steps.AppendLine("Back Substitution Steps:\n");

            for (int i = n - 1; i >= 0; i--)
            {
                double sum = matrix[i, cols - 1];

                for (int j = i + 1; j < n; j++)
                {
                    sum -= matrix[i, j] * solution[j];
                    steps.AppendLine(
                        $"Subtracting {matrix[i, j]} * x{j + 1}");
                }

                solution[i] = sum / matrix[i, i];

                steps.AppendLine(
                    $"x{i + 1} = {ToFraction(solution[i])}\n");
            }

            steps.AppendLine("Final Solution:");
            for (int i = 0; i < n; i++)
                steps.AppendLine($"x{i + 1} = {ToFraction(solution[i])}");

            return steps.ToString();
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