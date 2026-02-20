using System;
using System.Text;

namespace LinearCalculator.Unit_One
{
    public partial class EliminationPage : ContentPage
    {
        public EliminationPage()
        {
            InitializeComponent();
        }

        private void OnEliminateClicked(object sender, EventArgs e)
        {
            try
            {
                var rows = MatrixInput.Text
                    .Trim()
                    .Split(new[] { "\r\n", "\n", "\r" },
                           StringSplitOptions.RemoveEmptyEntries);

                int rowCount = rows.Length;
                var firstRowValues = rows[0]
                    .Trim()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                int colCount = firstRowValues.Length;

                double[,] matrix = new double[rowCount, colCount];

                for (int i = 0; i < rowCount; i++)
                {
                    var values = rows[i]
                        .Trim()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (values.Length != colCount)
                    {
                        StepsLabel.Text = "All rows must have same number of columns.";
                        return;
                    }

                    for (int j = 0; j < colCount; j++)
                    {
                        if (!double.TryParse(values[j], out double number))
                        {
                            StepsLabel.Text = "Invalid number format.";
                            return;
                        }

                        matrix[i, j] = number;
                    }
                }

                string steps = PerformGaussianElimination(matrix);

                StepsLabel.Text = steps;
            }
            catch
            {
                StepsLabel.Text = "Invalid matrix input.";
            }
        }

        private string PerformGaussianElimination(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            StringBuilder steps = new StringBuilder();
            int lead = 0;

            for (int r = 0; r < rows; r++)
            {
                if (lead >= cols)
                    break;

                int i = r;

                while (Math.Abs(matrix[i, lead]) < 1e-10)
                {
                    i++;
                    if (i == rows)
                    {
                        i = r;
                        lead++;
                        if (lead == cols)
                            return steps.ToString();
                    }
                }

                // Swap rows if needed
                if (i != r)
                {
                    SwapRows(matrix, i, r);
                    steps.AppendLine($"R{i + 1} ↔ R{r + 1}");
                    steps.AppendLine(MatrixToString(matrix));
                }

                // Normalize pivot row
                double pivot = matrix[r, lead];
                if (Math.Abs(pivot - 1) > 1e-10)
                {
                    MultiplyRow(matrix, r, 1.0 / pivot);
                    steps.AppendLine($"R{r + 1} = (1/{pivot:0.#####}) R{r + 1}");
                    steps.AppendLine(MatrixToString(matrix));
                }

                // Eliminate other rows
                for (int j = 0; j < rows; j++)
                {
                    if (j != r)
                    {
                        double factor = matrix[j, lead];
                        if (Math.Abs(factor) > 1e-10)
                        {
                            AddMultipleOfRow(matrix, j, r, -factor);
                            steps.AppendLine($"R{j + 1} = R{j + 1} - ({factor:0.#####}) R{r + 1}");
                            steps.AppendLine(MatrixToString(matrix));
                        }
                    }
                }

                lead++;
            }

            steps.AppendLine("Final RREF:");
            steps.AppendLine(MatrixToString(matrix));

            return steps.ToString();
        }

        private void SwapRows(double[,] matrix, int r1, int r2)
        {
            int cols = matrix.GetLength(1);
            for (int j = 0; j < cols; j++)
            {
                double temp = matrix[r1, j];
                matrix[r1, j] = matrix[r2, j];
                matrix[r2, j] = temp;
            }
        }

        private void MultiplyRow(double[,] matrix, int row, double scalar)
        {
            int cols = matrix.GetLength(1);
            for (int j = 0; j < cols; j++)
                matrix[row, j] *= scalar;
        }

        private void AddMultipleOfRow(double[,] matrix, int targetRow, int sourceRow, double scalar)
        {
            int cols = matrix.GetLength(1);
            for (int j = 0; j < cols; j++)
                matrix[targetRow, j] += matrix[sourceRow, j] * scalar;
        }

        

        private string MatrixToString(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                sb.Append("| ");
                for (int j = 0; j < cols; j++)
                {
                    sb.Append($"{ToFraction(matrix[i, j]),8} ");
                }
                sb.AppendLine("|");
            }

            sb.AppendLine();
            return sb.ToString();
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