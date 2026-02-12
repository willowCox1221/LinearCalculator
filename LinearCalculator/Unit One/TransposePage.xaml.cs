using System;
using System.Collections.Generic;


namespace LinearCalculator.Unit_One;

public partial class TransposePage : ContentPage
{
    public TransposePage()
    {
        InitializeComponent();
    }

    private void OnTransposeClicked(object sender, EventArgs e)
    {
        try
        {
            double[,] matrix = ParseMatrix(MatrixInput.Text);

            List<string> steps = new();

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            steps.Add($"Original matrix is {rows}x{cols}");
            steps.Add("Step 1: Swap rows and columns\n");

            double[,] transpose = new double[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    transpose[j, i] = matrix[i, j];
                    steps.Add($"Move element ({i + 1},{j + 1}) → ({j + 1},{i + 1})");
                }
            }

            steps.Add("\nFinal Transposed Matrix:\n");
            steps.Add(MatrixToString(transpose));

            StepsLabel.Text = string.Join("\n", steps);
        }
        catch (Exception ex)
        {
            StepsLabel.Text = $"Error: {ex.Message}";
        }
    }

    private double[,] ParseMatrix(string input)
    {
        var rows = input
            .Trim()
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        int rowCount = rows.Length;

        var firstRow = rows[0]
            .Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

        int colCount = firstRow.Length;

        double[,] matrix = new double[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            var values = rows[i]
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length != colCount)
                throw new Exception("All rows must have same number of columns.");

            for (int j = 0; j < colCount; j++)
                matrix[i, j] = double.Parse(values[j]);
        }

        return matrix;
    }

    private string MatrixToString(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        string result = "";

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
                result += $"{matrix[i, j],6} ";

            result += "\n";
        }

        return result;
    }
}