using System;
using System.Linq;

namespace LinearCalculator.Unit_One
{
    public partial class InversePage : ContentPage
    {
        public InversePage()
        {
            InitializeComponent();
        }

        private void OnInverseClicked(object sender, EventArgs e)
        {
            try
            {
                var rows = MatrixInput.Text
                    .Trim()
                    .Split(new[] { "\r\n", "\n", "\r" },
                           StringSplitOptions.RemoveEmptyEntries);

                int n = rows.Length;
                double[,] matrix = new double[n, n];

                for (int i = 0; i < n; i++)
                {
                    var values = rows[i]
                        .Trim()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (values.Length != n)
                    {
                        StepsLabel.Text = "Matrix must be square.";
                        return;
                    }

                    for (int j = 0; j < n; j++)
                    {
                        if (!TryParseNumber(values[j], out double number))
                        {
                            StepsLabel.Text = "Invalid number format.";
                            return;
                        }

                        matrix[i, j] = number;
                    }
                }

                string steps = "";
                double det = CalculateDeterminant(matrix);

                steps += $"Determinant = {det}\n\n";

                if (det == 0)
                {
                    StepsLabel.Text = "Matrix is not invertible (determinant = 0).";
                    return;
                }

                double[,] adj = Adjugate(matrix);
                double[,] inverse = MultiplyByScalar(adj, 1 / det);

                steps += "Inverse Matrix:\n";
                steps += MatrixToString(inverse);

                StepsLabel.Text = steps;
            }
            catch
            {
                StepsLabel.Text = "Invalid matrix input.";
            }
        }

        private double CalculateDeterminant(double[,] matrix)
        {
            int n = matrix.GetLength(0);

            if (n == 1)
                return matrix[0, 0];

            if (n == 2)
                return matrix[0, 0] * matrix[1, 1]
                       - matrix[0, 1] * matrix[1, 0];

            double det = 0;

            for (int col = 0; col < n; col++)
            {
                det += Math.Pow(-1, col)
                       * matrix[0, col]
                       * CalculateDeterminant(GetMinor(matrix, 0, col));
            }

            return det;
        }

        private double[,] Adjugate(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[,] cofactors = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double[,] minor = GetMinor(matrix, i, j);
                    double sign = Math.Pow(-1, i + j);
                    cofactors[i, j] = sign * CalculateDeterminant(minor);
                }
            }

            return Transpose(cofactors);
        }

        private double[,] Transpose(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[,] result = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    result[j, i] = matrix[i, j];

            return result;
        }

        private double[,] MultiplyByScalar(double[,] matrix, double scalar)
        {
            int n = matrix.GetLength(0);
            double[,] result = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    result[i, j] = matrix[i, j] * scalar;

            return result;
        }

        private double[,] GetMinor(double[,] matrix, int rowToRemove, int colToRemove)
        {
            int n = matrix.GetLength(0);
            double[,] minor = new double[n - 1, n - 1];

            int r = 0;

            for (int i = 0; i < n; i++)
            {
                if (i == rowToRemove)
                    continue;

                int c = 0;

                for (int j = 0; j < n; j++)
                {
                    if (j == colToRemove)
                        continue;

                    minor[r, c] = matrix[i, j];
                    c++;
                }

                r++;
            }

            return minor;
        }

        private bool TryParseNumber(string input, out double result)
        {
            input = input.Trim();

            // Handle fraction input like 3/4
            if (input.Contains("/"))
            {
                var parts = input.Split('/');
                if (parts.Length == 2 &&
                    double.TryParse(parts[0], out double numerator) &&
                    double.TryParse(parts[1], out double denominator) &&
                    denominator != 0)
                {
                    result = numerator / denominator;
                    return true;
                }
            }

            // Otherwise handle normal decimal/integer
            return double.TryParse(input, out result);
        }

        private string MatrixToString(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            string result = "";

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result += ToFraction(matrix[i, j]) + "   ";
                }
                result += "\n";
            }

            return result;
        }

        private string ToFraction(double value)
        {
            if (Math.Abs(value) < 1e-10)
                return "0";

            if (Math.Abs(value % 1) < 1e-10)
                return ((int)Math.Round(value)).ToString();

            int maxDenominator = 1000;
            int bestNumerator = 0;
            int bestDenominator = 1;
            double bestError = double.MaxValue;

            for (int den = 1; den <= maxDenominator; den++)
            {
                int num = (int)Math.Round(value * den);
                double error = Math.Abs(value - (double)num / den);

                if (error < bestError)
                {
                    bestError = error;
                    bestNumerator = num;
                    bestDenominator = den;
                }

                if (error < 1e-10)
                    break;
            }

            int gcd = GCD(Math.Abs(bestNumerator), Math.Abs(bestDenominator));

            bestNumerator /= gcd;
            bestDenominator /= gcd;

            if (bestDenominator == 1)
                return bestNumerator.ToString();

            return $"{bestNumerator}/{bestDenominator}";
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