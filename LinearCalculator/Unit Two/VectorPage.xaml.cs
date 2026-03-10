using System;
using System.Linq;
using System.Text;

namespace LinearCalculator.Unit_Two
{
    public partial class VectorPage : ContentPage
    {
        public VectorPage()
        {
            InitializeComponent();
        }

        private double[] ParseVector(string input)
        {
            return input
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();
        }
        /// Addition///
        private void OnAddClicked(object sender, EventArgs e)
        {
            try
            {
                var a = ParseVector(VectorAInput.Text);
                var b = ParseVector(VectorBInput.Text);

                if (a.Length != b.Length)
                {
                    ResultLabel.Text = "Vectors must be same dimension.";
                    return;
                }

                StringBuilder steps = new StringBuilder();
                steps.AppendLine("Vector Addition:");
                steps.AppendLine($"A = {FormatVector(a)}");
                steps.AppendLine($"B = {FormatVector(b)}\n");

                double[] result = new double[a.Length];

                for (int i = 0; i < a.Length; i++)
                {
                    result[i] = a[i] + b[i];
                    steps.AppendLine(
                        $"Component {i + 1}: {ToFraction(a[i])} + {ToFraction(b[i])} = {ToFraction(result[i])}");
                }

                steps.AppendLine($"\nA + B = {FormatVector(result)}");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
        // Subraction/////////////////////////////////////////////////
        private void OnSubtractClicked(object sender, EventArgs e)
        {
            try
            {
                var a = ParseVector(VectorAInput.Text);
                var b = ParseVector(VectorBInput.Text);

                if (a.Length != b.Length)
                {
                    ResultLabel.Text = "Vectors must be same dimension.";
                    return;
                }

                StringBuilder steps = new StringBuilder();
                steps.AppendLine("Vector Subtraction:");
                steps.AppendLine($"A = {FormatVector(a)}");
                steps.AppendLine($"B = {FormatVector(b)}\n");

                double[] result = new double[a.Length];

                for (int i = 0; i < a.Length; i++)
                {
                    result[i] = a[i] - b[i];
                    steps.AppendLine(
                        $"Component {i + 1}: {ToFraction(a[i])} - {ToFraction(b[i])} = {ToFraction(result[i])}");
                }

                steps.AppendLine($"\nA - B = {FormatVector(result)}");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
        // Dot Product//////////////////////////////////////////
        private void OnDotProductClicked(object sender, EventArgs e)
        {
            try
            {
                var a = ParseVector(VectorAInput.Text);
                var b = ParseVector(VectorBInput.Text);

                if (a.Length != b.Length)
                {
                    ResultLabel.Text = "Vectors must be same dimension.";
                    return;
                }

                StringBuilder steps = new StringBuilder();
                steps.AppendLine("Dot Product:");
                steps.AppendLine($"A = {FormatVector(a)}");
                steps.AppendLine($"B = {FormatVector(b)}\n");

                double sum = 0;

                for (int i = 0; i < a.Length; i++)
                {
                    double product = a[i] * b[i];
                    sum += product;

                    steps.AppendLine(
                        $"Component {i + 1}: {ToFraction(a[i])} × {ToFraction(b[i])} = {ToFraction(product)}");
                }

                steps.AppendLine($"\nSum of products = {ToFraction(sum)}");
                steps.AppendLine($"A · B = {ToFraction(sum)}");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
        // Magnitude //////////////////////////////////////////////////////////
        private void OnMagnitudeClicked(object sender, EventArgs e)
        {
            try
            {
                var a = ParseVector(VectorAInput.Text);

                StringBuilder steps = new StringBuilder();
                steps.AppendLine("Magnitude of A:");
                steps.AppendLine($"A = {FormatVector(a)}\n");

                double sum = 0;

                for (int i = 0; i < a.Length; i++)
                {
                    double squared = a[i] * a[i];
                    sum += squared;

                    steps.AppendLine(
                        $"Component {i + 1}: ({ToFraction(a[i])})² = {ToFraction(squared)}");
                }

                steps.AppendLine($"\nSum = {ToFraction(sum)}");
                steps.AppendLine($"|A| = √{ToFraction(sum)}");

                double magnitude = Math.Sqrt(sum);
                steps.AppendLine($"|A| = {ToFraction(magnitude)}");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
        // Scalar//////////////////////////////////////////////////////
        private void OnScalarMultiplyClicked(object sender, EventArgs e)
        {
            try
            {
                var a = ParseVector(VectorAInput.Text);
                double scalar = double.Parse(ScalarInput.Text);

                StringBuilder steps = new StringBuilder();
                steps.AppendLine("Scalar Multiplication:");
                steps.AppendLine($"Scalar = {ToFraction(scalar)}");
                steps.AppendLine($"A = {FormatVector(a)}\n");

                double[] result = new double[a.Length];

                for (int i = 0; i < a.Length; i++)
                {
                    result[i] = a[i] * scalar;

                    steps.AppendLine(
                        $"Component {i + 1}: {ToFraction(scalar)} × {ToFraction(a[i])} = {ToFraction(result[i])}");
                }

                steps.AppendLine($"\nResult = {FormatVector(result)}");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
        // Angle//////////////////////////////////////////////////////////
        private void OnAngleClicked(object sender, EventArgs e)
        {
            try
            {
                var a = ParseVector(VectorAInput.Text);
                var b = ParseVector(VectorBInput.Text);

                if (a.Length != b.Length)
                {
                    ResultLabel.Text = "Vectors must have same dimension.";
                    return;
                }

                StringBuilder steps = new StringBuilder();

                steps.AppendLine("ANGLE BETWEEN VECTORS\n");

                steps.AppendLine($"A = {FormatVector(a)}");
                steps.AppendLine($"B = {FormatVector(b)}\n");

                // Step 1: Dot Product
                double dot = 0;

                steps.AppendLine("Step 1: Dot Product");

                for (int i = 0; i < a.Length; i++)
                {
                    dot += a[i] * b[i];
                    steps.AppendLine($"{ToFraction(a[i])} * {ToFraction(b[i])}");
                }

                steps.AppendLine($"A · B = {ToFraction(dot)}\n");

                // Step 2: Magnitude of A
                double magA = Math.Sqrt(a.Sum(x => x * x));

                steps.AppendLine("Step 2: Magnitude of A");
                steps.AppendLine($"|A| = {ToFraction(magA)}\n");

                // Step 3: Magnitude of B
                double magB = Math.Sqrt(b.Sum(x => x * x));

                steps.AppendLine("Step 3: Magnitude of B");
                steps.AppendLine($"|B| = {ToFraction(magB)}\n");

                // Step 4: Cos(theta)
                double cosTheta = dot / (magA * magB);

                steps.AppendLine("Step 4: Use Formula");
                steps.AppendLine($"cosθ = (A·B) / (|A||B|)");
                steps.AppendLine($"cosθ = {ToFraction(dot)} / ({ToFraction(magA)} * {ToFraction(magB)})");
                steps.AppendLine($"cosθ = {ToFraction(cosTheta)}\n");

                // Step 5: Angle
                double thetaRadians = Math.Acos(cosTheta);
                double thetaDegrees = thetaRadians * (180 / Math.PI);

                steps.AppendLine("Step 5: Find Angle");
                steps.AppendLine($"θ = cos⁻¹({ToFraction(cosTheta)})");
                steps.AppendLine($"θ = {ToFraction(thetaDegrees)}°");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
        // Projection ////////////////////////////////////////////////////////
        private void OnProjectionClicked(object sender, EventArgs e)
        {
            try
            {
                var a = ParseVector(VectorAInput.Text);
                var b = ParseVector(VectorBInput.Text);

                if (a.Length != b.Length)
                {
                    ResultLabel.Text = "Vectors must be same dimension.";
                    return;
                }

                StringBuilder steps = new StringBuilder();

                steps.AppendLine("PROJECTION OF A ONTO B\n");

                steps.AppendLine($"A = {FormatVector(a)}");
                steps.AppendLine($"B = {FormatVector(b)}\n");

                // Step 1: A·B
                double dotAB = 0;

                steps.AppendLine("Step 1: Dot Product A·B");

                for (int i = 0; i < a.Length; i++)
                {
                    dotAB += a[i] * b[i];
                    steps.AppendLine($"{ToFraction(a[i])} * {ToFraction(b[i])}");
                }

                steps.AppendLine($"A·B = {ToFraction(dotAB)}\n");

                // Step 2: B·B
                double dotBB = 0;

                steps.AppendLine("Step 2: Dot Product B·B");

                for (int i = 0; i < b.Length; i++)
                {
                    dotBB += b[i] * b[i];
                    steps.AppendLine($"{ToFraction(b[i])}²");
                }

                steps.AppendLine($"B·B = {ToFraction(dotBB)}\n");

                // Step 3: Scalar
                double scalar = dotAB / dotBB;

                steps.AppendLine("Step 3: Scalar");
                steps.AppendLine($"(A·B)/(B·B) = {ToFraction(dotAB)} / {ToFraction(dotBB)}");
                steps.AppendLine($"Scalar = {ToFraction(scalar)}\n");

                // Step 4: Multiply scalar * B
                double[] projection = new double[b.Length];

                steps.AppendLine("Step 4: Multiply scalar * B");

                for (int i = 0; i < b.Length; i++)
                {
                    projection[i] = scalar * b[i];
                    steps.AppendLine($"{ToFraction(scalar)} * {ToFraction(b[i])} = {ToFraction(projection[i])}");
                }

                steps.AppendLine("\nProjection Vector:");
                steps.AppendLine($"proj_B(A) = {FormatVector(projection)}");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
        

        private string FormatVector(double[] vector)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");

            for (int i = 0; i < vector.Length; i++)
            {
                sb.Append(ToFraction(vector[i]));
                if (i < vector.Length - 1)
                    sb.Append(", ");
            }

            sb.Append(")");
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