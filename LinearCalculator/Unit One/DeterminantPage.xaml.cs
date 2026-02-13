using System;
using System.Linq;

namespace LinearCalculator.Unit_One
{
    public partial class DeterminantPage : ContentPage
    {
        public DeterminantPage()
        {
            InitializeComponent();
        }

        private void OnDeterminantClicked(object sender, EventArgs e)
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
                        .Split(new[] { ' ' },
                               StringSplitOptions.RemoveEmptyEntries);

                    if (values.Length != n)
                    {
                        StepsLabel.Text = "Matrix must be square.";
                        return;
                    }

                    for (int j = 0; j < n; j++)
                    {
                        if (!double.TryParse(values[j], out double number))
                        {
                            StepsLabel.Text = "Invalid number format.";
                            return;
                        }

                        matrix[i, j] = number;
                    }
                }

                string steps = "";
                double det = CalculateDeterminant(matrix, ref steps, 0);
                StepsLabel.Text = steps + $"\nFinal Determinant = {det}";
            }
            catch
            {
                StepsLabel.Text = "Invalid matrix input.";
            }
        }

        private double CalculateDeterminant(double[,] matrix, ref string steps, int level)
        {
            int n = matrix.GetLength(0);
            string indent = new string(' ', level * 4);

            if (n == 1)
            {
                steps += indent + $"Determinant of 1x1 matrix = {matrix[0, 0]}\n";
                return matrix[0, 0];
            }

            if (n == 2)
            {
                double result = matrix[0, 0] * matrix[1, 1]
                                - matrix[0, 1] * matrix[1, 0];

                steps += indent + "For 2x2 matrix:\n";
                steps += indent + $"({matrix[0, 0]}×{matrix[1, 1]}) − ({matrix[0, 1]}×{matrix[1, 0]})\n";
                steps += indent + $"{matrix[0, 0] * matrix[1, 1]} − {matrix[0, 1] * matrix[1, 0]} = {result}\n\n";

                return result;
            }

            double det = 0;
            steps += indent + $"Expanding along first row:\n";

            for (int col = 0; col < n; col++)
            {
                double sign = Math.Pow(-1, col);
                double element = matrix[0, col];

                steps += indent + $"Element a(1,{col + 1}) = {element}\n";

                double[,] minor = GetMinor(matrix, 0, col);

                double minorDet = CalculateDeterminant(minor, ref steps, level + 1);

                double term = sign * element * minorDet;

                steps += indent + $"Cofactor term = {sign} × {element} × {minorDet} = {term}\n\n";

                det += term;
            }

            steps += indent + $"Determinant at this level = {det}\n\n";

            return det;
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
    }
}