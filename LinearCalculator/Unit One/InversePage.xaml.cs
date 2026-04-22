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
                StepsContainer.Children.Clear();

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
                        StepsContainer.Children.Add(
                            CreateStep("Error", "Matrix must be square.", Colors.Red));
                        return;
                    }

                    for (int j = 0; j < n; j++)
                    {
                        if (!TryParseNumber(values[j], out double number))
                        {
                            StepsContainer.Children.Add(
                                CreateStep("Error", "Invalid number format.", Colors.Red));
                            return;
                        }

                        matrix[i, j] = number;
                    }
                }

                // STEP 1
                StepsContainer.Children.Add(
                    CreateStep("STEP 1: Matrix A", MatrixToString(matrix))
                );

                // STEP 2 - Determinant
                double det = CalculateDeterminant(matrix);

                string detSteps = $"det(A) = {ToFraction(det)}";

                if (n == 2)
                {
                    detSteps =
                        $"det(A) = ({ToFraction(matrix[0, 0])})({ToFraction(matrix[1, 1])}) - " +
                        $"({ToFraction(matrix[0, 1])})({ToFraction(matrix[1, 0])})\n\n" +
                        $"det(A) = {ToFraction(det)}";
                }

                StepsContainer.Children.Add(
                    CreateStep("STEP 2: Determinant", detSteps)
                );

                // STEP 3 - Check invertibility
                if (det == 0)
                {
                    StepsContainer.Children.Add(
                        CreateStep("STEP 3: Invertibility",
                        "det(A) = 0 → Matrix is NOT invertible.", Colors.Red));
                    return;
                }
                else
                {
                    StepsContainer.Children.Add(
                        CreateStep("STEP 3: Invertibility",
                        "det(A) ≠ 0 → Matrix is invertible.", Colors.Green));
                }

                // STEP 4 - Cofactor Matrix
                int size = matrix.GetLength(0);
                double[,] cofactors = new double[size, size];
                string cofactorSteps = "";

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        double[,] minor = GetMinor(matrix, i, j);
                        double sign = Math.Pow(-1, i + j);
                        double minorDet = CalculateDeterminant(minor);

                        cofactors[i, j] = sign * minorDet;

                        cofactorSteps += $"C{i + 1}{j + 1} = {((i + j) % 2 == 0 ? "+" : "-")} det(minor) = {ToFraction(cofactors[i, j])}\n";
                    }
                }

                cofactorSteps += "\nMatrix:\n" + MatrixToString(cofactors);

                StepsContainer.Children.Add(
                    CreateStep("STEP 4: Cofactor Matrix", cofactorSteps)
                );

                // STEP 5 - Adjugate
                double[,] adj = Transpose(cofactors);

                StepsContainer.Children.Add(
                    CreateStep("STEP 5: Adjugate (Transpose)", MatrixToString(adj))
                );

                // STEP 6 - Scale
                StepsContainer.Children.Add(
                    CreateStep("STEP 6: Scale by 1/det",
                    $"1 / det = 1 / {ToFraction(det)}")
                );

                // STEP 7 - Final Answer
                double[,] inverse = MultiplyByScalar(adj, 1 / det);

                StepsContainer.Children.Add(
                    CreateStep("STEP 7: Inverse Matrix", MatrixToString(inverse))
                );
            }
            catch
            {
                StepsContainer.Children.Add(
                    CreateStep("Error", "Invalid matrix input.", Colors.Red));
            }
        }


        // Gets determinant
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


        private View CreateStep(string title, string content, Color? color = null)
        {
            return new Frame
            {
                Padding = 12,
                CornerRadius = 12,
                BorderColor = Colors.LightGray,
                BackgroundColor = Colors.White,
                Content = new VerticalStackLayout
                {
                    Children =
            {
                new Label
                {
                    Text = title,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Purple
                },
                new Label
                {
                    Text = content,
                    FontSize = 14,
                    FontFamily = "Courier New",
                    TextColor = color ?? Colors.Black
                }
            }
                }
            };
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
                result += "| ";
                for (int j = 0; j < n; j++)
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