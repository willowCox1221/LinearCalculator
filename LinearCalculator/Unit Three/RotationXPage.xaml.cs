using System;
using System.Text;

namespace LinearCalculator.Unit_Three
{
    public partial class RotationXPage : ContentPage
    {
        public RotationXPage()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            try
            {
                double degrees = double.Parse(AngleInput.Text);

                StringBuilder steps = new StringBuilder();

                steps.AppendLine("ROTATION ABOUT X-AXIS\n");

                steps.AppendLine($"Angle = {degrees}°");

                double radians = degrees * Math.PI / 180;

                steps.AppendLine($"Convert to radians:");
                steps.AppendLine($"{degrees} * π/180 = {radians:F3}\n");

                double cos = Math.Cos(radians);
                double sin = Math.Sin(radians);

                steps.AppendLine($"cos({degrees}) = {cos:F3}");
                steps.AppendLine($"sin({degrees}) = {sin:F3}\n");

                steps.AppendLine("Rotation Matrix Rₓ(θ):\n");

                steps.AppendLine($"[ 1      0        0 ]");
                steps.AppendLine($"[ 0   {cos:F3}   {-sin:F3} ]");
                steps.AppendLine($"[ 0   {sin:F3}    {cos:F3} ]");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
    }
}