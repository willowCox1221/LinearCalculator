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
            double[,] matrix = ParseMatrix(MatrixInput.Text);
            List<string> steps = new();

            ComputeRref(matrix, steps);

            StepsLabel.Text = string.Join("\n\n", steps);
        }
        catch (Exception ex)
        {
            StepsLabel.Text = $"Error: {ex.Message}";
        }
    }

    // ---------------- MATRIX PARSING ----------------
    private double[,] ParseMatrix(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new Exception("Matrix input is empty.");

        // Split into rows (handles Windows + Unix line endings)
        var rows = input
            .Trim()
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        int rowCount = rows.Length;

        // Split first row by ANY whitespace
        var firstRowValues = rows[0]
            .Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

        int colCount = firstRowValues.Length;

        double[,] matrix = new double[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            var values = rows[i]
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length != colCount)
                throw new Exception("All rows must have the same number of columns.");

            for (int j = 0; j < colCount; j++)
                matrix[i, j] = double.Parse(values[j]);
        }

        return matrix;
    }

    // ---------------- RREF LOGIC ----------------
    private void ComputeRref(double[,] matrix, List<string> steps)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        int lead = 0;

        for (int r = 0; r < rows; r++)
        {
            if (lead >= cols)
                return;

            int i = r;
            while (matrix[i, lead] == 0)
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

            double pivot = matrix[r, lead];
            if (pivot != 1)
            {
                ScaleRow(matrix, r, 1 / pivot);
                steps.Add($"R{r + 1} = R{r + 1} / {pivot}\n{MatrixToString(matrix)}");
            }

            for (int j = 0; j < rows; j++)
            {
                if (j != r)
                {
                    double factor = matrix[j, lead];
                    if (factor != 0)
                    {
                        AddRowMultiple(matrix, j, r, -factor);
                        steps.Add($"R{j + 1} = R{j + 1} - ({factor})R{r + 1}\n{MatrixToString(matrix)}");
                    }
                }
            }

            lead++;
        }
    }

    // ---------------- ROW OPERATIONS ----------------
    private void SwapRows(double[,] m, int r1, int r2)
    {
        int cols = m.GetLength(1);
        for (int i = 0; i < cols; i++)
            (m[r1, i], m[r2, i]) = (m[r2, i], m[r1, i]);
    }

    private void ScaleRow(double[,] m, int row, double factor)
    {
        int cols = m.GetLength(1);
        for (int i = 0; i < cols; i++)
            m[row, i] *= factor;
    }

    private void AddRowMultiple(double[,] m, int target, int source, double factor)
    {
        int cols = m.GetLength(1);
        for (int i = 0; i < cols; i++)
            m[target, i] += factor * m[source, i];
    }

    // ---------------- DISPLAY ----------------
    private string MatrixToString(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        string result = "";

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
                result += $"{Math.Round(matrix[i, j], 2),6} ";

            result += "\n";
        }

        return result;
    }
}