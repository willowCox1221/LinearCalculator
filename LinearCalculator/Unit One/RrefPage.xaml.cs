using System;
using System.Collections.Generic;

namespace LinearCalculator;

public partial class RrefPage : ContentPage
{
    public RrefPage()
    {
        InitializeComponent();
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        try
        {
            Fraction[,] matrix = ParseMatrix(MatrixInput.Text);
            List<string> steps = new();

            ComputeRref(matrix, steps);

            // 🔥 NEW PART
            string analysis = AnalyzeSolution(matrix);
            string solution = GetSolution(matrix);

            steps.Add("Final RREF:");
            steps.Add(MatrixToString(matrix));
            steps.Add(analysis);
            steps.Add(solution);

            StepsLabel.Text = string.Join("\n\n", steps);
        }
        catch (Exception ex)
        {
            StepsLabel.Text = $"Error: {ex.Message}";
        }
    }

    // ---------------- MATRIX PARSING ----------------
    private Fraction[,] ParseMatrix(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new Exception("Matrix input is empty.");

        var rows = input
            .Trim()
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        int rowCount = rows.Length;

        var firstRowValues = rows[0]
            .Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

        int colCount = firstRowValues.Length;

        Fraction[,] matrix = new Fraction[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            var values = rows[i]
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length != colCount)
                throw new Exception("All rows must have same number of columns.");

            for (int j = 0; j < colCount; j++)
                matrix[i, j] = ParseFraction(values[j]);
        }

        return matrix;
    }

    private Fraction ParseFraction(string text)
    {
        if (text.Contains("/"))
        {
            var parts = text.Split('/');
            return new Fraction(int.Parse(parts[0]), int.Parse(parts[1]));
        }

        return new Fraction(int.Parse(text));
    }

    // ---------------- RREF LOGIC ----------------
    private void ComputeRref(Fraction[,] matrix, List<string> steps)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        int lead = 0;

        for (int r = 0; r < rows; r++)
        {
            if (lead >= cols)
                return;

            int i = r;
            while (matrix[i, lead].IsZero())
            {
                i++;
                if (i == rows)
                {
                    i = r;
                    lead++;
                    if (lead == cols)
                        return;
                }
            }

            if (i != r)
            {
                SwapRows(matrix, i, r);
                steps.Add($"Swap R{i + 1} ↔ R{r + 1}\n{MatrixToString(matrix)}");
            }

            Fraction pivot = matrix[r, lead];
            if (pivot.Numerator != pivot.Denominator)
            {
                ScaleRow(matrix, r, new Fraction(1) / pivot);
                steps.Add($"R{r + 1} = R{r + 1} / {pivot}\n{MatrixToString(matrix)}");
            }

            for (int j = 0; j < rows; j++)
            {
                if (j != r)
                {
                    Fraction factor = matrix[j, lead];
                    if (!factor.IsZero())
                    {
                        AddRowMultiple(matrix, j, r, -factor);
                        steps.Add($"R{j + 1} = R{j + 1} - ({factor})R{r + 1}\n{MatrixToString(matrix)}");
                    }
                }
            }

            lead++;
        }
    }

    private string AnalyzeSolution(Fraction[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        int variables = cols - 1;

        int pivotCount = 0;
        bool inconsistent = false;

        for (int i = 0; i < rows; i++)
        {
            bool allZero = true;

            for (int j = 0; j < variables; j++)
            {
                if (!matrix[i, j].IsZero())
                {
                    allZero = false;
                    break;
                }
            }

            // 0 0 0 | b  (b ≠ 0)
            if (allZero && !matrix[i, cols - 1].IsZero())
                inconsistent = true;

            if (!allZero)
                pivotCount++;
        }

        if (inconsistent)
            return "No solution (inconsistent system)";

        if (pivotCount == variables)
            return "Unique solution";

        return "Infinite solutions (free variables)";
    }

    private string GetSolution(Fraction[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        int variables = cols - 1;

        string[] result = new string[variables];
        bool[] isPivot = new bool[variables];

        // Detect pivot columns
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < variables; j++)
            {
                if (matrix[i, j].Numerator == matrix[i, j].Denominator) // equals 1
                {
                    isPivot[j] = true;
                    break;
                }
            }
        }

        // Assign parameters
        string[] paramNames = { "t", "s", "u", "v" };
        int paramIndex = 0;

        for (int j = 0; j < variables; j++)
        {
            if (!isPivot[j])
            {
                result[j] = paramNames[paramIndex++];
            }
        }

        // Solve pivot variables
        for (int i = 0; i < rows; i++)
        {
            int pivotCol = -1;

            for (int j = 0; j < variables; j++)
            {
                if (matrix[i, j].Numerator == matrix[i, j].Denominator)
                {
                    pivotCol = j;
                    break;
                }
            }

            if (pivotCol == -1) continue;

            string expr = matrix[i, cols - 1].ToString();

            for (int j = pivotCol + 1; j < variables; j++)
            {
                if (!matrix[i, j].IsZero())
                {
                    expr += $" - ({matrix[i, j]}){result[j]}";
                }
            }

            result[pivotCol] = expr;
        }

        // Format nicely
        string[] varNames = { "x", "y", "z", "w" };
        List<string> lines = new();

        for (int i = 0; i < variables; i++)
        {
            lines.Add($"{varNames[i]} = {result[i]}");
        }

        return string.Join("\n", lines);
    }

    // ---------------- ROW OPERATIONS ----------------
    private void SwapRows(Fraction[,] m, int r1, int r2)
    {
        int cols = m.GetLength(1);
        for (int i = 0; i < cols; i++)
            (m[r1, i], m[r2, i]) = (m[r2, i], m[r1, i]);
    }

    private void ScaleRow(Fraction[,] m, int row, Fraction factor)
    {
        int cols = m.GetLength(1);
        for (int i = 0; i < cols; i++)
            m[row, i] *= factor;
    }

    private void AddRowMultiple(Fraction[,] m, int target, int source, Fraction factor)
    {
        int cols = m.GetLength(1);
        for (int i = 0; i < cols; i++)
            m[target, i] += factor * m[source, i];
    }

    // ---------------- DISPLAY ----------------
    private string MatrixToString(Fraction[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        string result = "";

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
                result += $"{matrix[i, j],8} ";

            result += "\n";
        }

        return result;
    }
}