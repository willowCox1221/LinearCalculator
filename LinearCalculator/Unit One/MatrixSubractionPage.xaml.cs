using System;
using System.Collections.Generic;

namespace LinearCalculator;

public partial class MatrixSubtractionPage : ContentPage
{
    public MatrixSubtractionPage()
    {
        InitializeComponent();
    }

    private void OnSubtractClicked(object sender, EventArgs e)
    {
        try
        {
            double[,] A = ParseMatrix(MatrixAInput.Text);
            double[,] B = ParseMatrix(MatrixBInput.Text);

            List<string> steps = new();

            int rowsA = A.GetLength(0);
            int colsA = A.GetLength(1);
            int rowsB = B.GetLength(0);
            int colsB = B.GetLength(1);

            steps.Add("Step 1: Check dimensions");
            steps.Add($"Matrix A is {rowsA}x{colsA}");
            steps.Add($"Matrix B is {rowsB}x{colsB}");

            if (rowsA != rowsB || colsA != colsB)
                throw new Exception("Matrices must have the same dimensions.");

            steps.Add("Dimensions match ✓\n");
            steps.Add("Step 2: Subtract corresponding entries\n");

            double[,] result = new double[rowsA, colsA];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsA; j++)
                {
                    result[i, j] = A[i, j] - B[i, j];

                    steps.Add(
                        $"C[{i + 1},{j + 1}] = {A[i, j]} - {B[i, j]} = {result[i, j]}"
                    );
                }
            }

            steps.Add("\nFinal Result:\n");
            steps.Add(MatrixToString(result));

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