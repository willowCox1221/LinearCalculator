using System;
using System.Text;
using Microsoft.Maui.Controls.Shapes;

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
                StepsContainer.Children.Clear();

                var rows = MatrixInput.Text
                    .Trim()
                    .Split(new[] { "\r\n", "\n", "\r" },
                           StringSplitOptions.RemoveEmptyEntries);

                int rowCount = rows.Length;
                int colCount = rows[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

                double[,] matrix = new double[rowCount, colCount];

                for (int i = 0; i < rowCount; i++)
                {
                    var values = rows[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < colCount; j++)
                    {
                        matrix[i, j] = double.Parse(values[j]);
                    }
                }

                // Initial
                StepsContainer.Children.Add(
                    CreateStep("Initial Matrix", MatrixToString(matrix))
                );

                GaussianElimination(matrix);

                // Final matrix
                StepsContainer.Children.Add(
                    CreateStep("Final RREF", MatrixToString(matrix))
                );

                // ✅ NOW matrix exists here
                string resultType = AnalyzeSolution(matrix);

                StepsContainer.Children.Add(
                    CreateStep("Solution Type", resultType)
                );
            }
            catch
            {
                StepsContainer.Children.Add(
                    CreateStep("Error", "Invalid input.")
                );
            }
        }

        private string AnalyzeSolution(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int pivotCount = 0;

            for (int i = 0; i < rows; i++)
            {
                bool allZero = true;

                for (int j = 0; j < cols - 1; j++)
                {
                    if (Math.Abs(matrix[i, j]) > 1e-10)
                    {
                        allZero = false;
                        pivotCount++;
                        break;
                    }
                }

                // ❌ No solution case
                if (allZero && Math.Abs(matrix[i, cols - 1]) > 1e-10)
                {
                    return "No Solution ❌ (Inconsistent system)";
                }
            }

            // ♾️ Infinite solutions
            if (pivotCount < cols - 1)
            {
                return "Infinite Solutions ♾️ (Free variables exist)";
            }

            // ✅ Unique solution
            return "Unique Solution ✅";
        }

        private (double[,], string) GaussianElimination(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            string steps = "";

            for (int pivot = 0; pivot < rows; pivot++)
            {
                // 🔹 Step 1: Pivoting (swap if needed)
                if (Math.Abs(matrix[pivot, pivot]) < 1e-10)
                {
                    for (int i = pivot + 1; i < rows; i++)
                    {
                        if (Math.Abs(matrix[i, pivot]) > 1e-10)
                        {
                            SwapRows(matrix, pivot, i);
                            steps += $"Swap R{pivot + 1} ↔ R{i + 1}\n";
                            steps += MatrixToString(matrix) + "\n";
                            break;
                        }
                    }
                }

                // 🔹 Step 2: Make pivot = 1
                double pivotVal = matrix[pivot, pivot];
                if (Math.Abs(pivotVal) > 1e-10)
                {
                    DivideRow(matrix, pivot, pivotVal);
                    steps += $"R{pivot + 1} → R{pivot + 1} / {ToFraction(pivotVal)}\n";
                    steps += MatrixToString(matrix) + "\n";
                }

                // 🔹 Step 3: Eliminate below
                for (int i = pivot + 1; i < rows; i++)
                {
                    double factor = matrix[i, pivot];
                    if (Math.Abs(factor) > 1e-10)
                    {
                        SubtractRows(matrix, i, pivot, factor);
                        steps += $"R{i + 1} → R{i + 1} - ({ToFraction(factor)})R{pivot + 1}\n";
                        steps += MatrixToString(matrix) + "\n";
                    }
                }
            }

            // 🔹 Step 4: Backward elimination (RREF)
            for (int pivot = rows - 1; pivot >= 0; pivot--)
            {
                for (int i = pivot - 1; i >= 0; i--)
                {
                    double factor = matrix[i, pivot];
                    if (Math.Abs(factor) > 1e-10)
                    {
                        SubtractRows(matrix, i, pivot, factor);
                        steps += $"R{i + 1} → R{i + 1} - ({ToFraction(factor)})R{pivot + 1}\n";
                        steps += MatrixToString(matrix) + "\n";
                    }
                }
            }

            return (matrix, steps);

        }

        private View CreateStep(string title, string matrixText)
        {
            return new Frame
            {
                CornerRadius = 10,
                Padding = 10,
                BorderColor = Colors.LightGray,
                BackgroundColor = Colors.White,
                Content = new VerticalStackLayout
                {
                    Children =
            {
                new Label
                {
                    Text = title,
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Green
                },
                new Label
                {
                    Text = matrixText,
                    FontFamily = "Courier New",
                    FontSize = 14
                }
            }
                }
            };
        }
        private void SwapRows(double[,] m, int r1, int r2)
        {
            int cols = m.GetLength(1);
            for (int j = 0; j < cols; j++)
            {
                (m[r1, j], m[r2, j]) = (m[r2, j], m[r1, j]);
            }
        }

        private void DivideRow(double[,] m, int row, double divisor)
        {
            int cols = m.GetLength(1);
            for (int j = 0; j < cols; j++)
            {
                m[row, j] /= divisor;
            }
        }

        private void SubtractRows(double[,] m, int target, int source, double factor)
        {
            int cols = m.GetLength(1);
            for (int j = 0; j < cols; j++)
            {
                m[target, j] -= factor * m[source, j];
            }
        }

        private string MatrixToString(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            string result = "";

            for (int i = 0; i < rows; i++)
            {
                result += "| ";
                for (int j = 0; j < cols; j++)
                {
                    result += $"{ToFraction(matrix[i, j]),6}";
                }
                result += " |\n";
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