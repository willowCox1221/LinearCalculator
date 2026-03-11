using System;
using System.Text;

namespace LinearCalculator.Unit_Three
{
    public partial class RotationYPage : ContentPage
    {
        public RotationYPage()
        {
            InitializeComponent();
        }

        private void OnRotationClicked(object sender, EventArgs e)
        {
            try
            {
                double degrees = double.Parse(AngleInput.Text);

                StringBuilder steps = new StringBuilder();

                steps.AppendLine("ROTATION ABOUT Y-AXIS\n");

                // Step 1
                steps.AppendLine($"Step 1: Angle = {degrees}°\n");

                // Step 2
                double radians = degrees * Math.PI / 180;

                steps.AppendLine("Step 2: Convert to radians");
                steps.AppendLine($"{degrees} × π / 180 = {radians:F3}\n");

                // Step 3
                double cos = Math.Cos(radians);
                double sin = Math.Sin(radians);

                steps.AppendLine("Step 3: Calculate trig values");
                steps.AppendLine($"cos({degrees}) = {cos:F3}");
                steps.AppendLine($"sin({degrees}) = {sin:F3}\n");

                // Step 4
                steps.AppendLine("Step 4: Use Y-axis rotation formula\n");

                steps.AppendLine("[ cosθ   0   sinθ ]");
                steps.AppendLine("[  0     1    0  ]");
                steps.AppendLine("[ -sinθ  0   cosθ ]\n");

                // Step 5
                steps.AppendLine("Step 5: Substitute values\n");

                steps.AppendLine($"[ {cos:F3}   0   {sin:F3} ]");
                steps.AppendLine($"[ 0     1    0 ]");
                steps.AppendLine($"[ {-sin:F3}   0   {cos:F3} ]");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
    }
}