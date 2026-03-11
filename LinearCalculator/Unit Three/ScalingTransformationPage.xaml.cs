using System;
using System.Text;

namespace LinearCalculator.Unit_Three
{
    public partial class ScalingTransformationPage : ContentPage
    {
        public ScalingTransformationPage()
        {
            InitializeComponent();
        }

        private void OnScalingClicked(object sender, EventArgs e)
        {
            try
            {
                double sx = double.Parse(ScaleX.Text);
                double sy = double.Parse(ScaleY.Text);
                double sz = double.Parse(ScaleZ.Text);

                StringBuilder steps = new StringBuilder();

                steps.AppendLine("3D SCALING TRANSFORMATION\n");

                // Step 1
                steps.AppendLine("Step 1: Identify the scale factors");
                steps.AppendLine($"Scale X = {sx}");
                steps.AppendLine($"Scale Y = {sy}");
                steps.AppendLine($"Scale Z = {sz}\n");

                // Step 2
                steps.AppendLine("Step 2: Use the scaling matrix formula\n");

                steps.AppendLine("[ sx   0   0 ]");
                steps.AppendLine("[ 0   sy   0 ]");
                steps.AppendLine("[ 0    0  sz ]\n");

                // Step 3
                steps.AppendLine("Step 3: Substitute the values\n");

                steps.AppendLine($"[ {sx}   0   0 ]");
                steps.AppendLine($"[ 0   {sy}   0 ]");
                steps.AppendLine($"[ 0   0   {sz} ]\n");

                // Step 4
                steps.AppendLine("Step 4: Final Scaling Matrix\n");

                steps.AppendLine($"S =");
                steps.AppendLine($"[ {sx}   0   0 ]");
                steps.AppendLine($"[ 0   {sy}   0 ]");
                steps.AppendLine($"[ 0   0   {sz} ]");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
    }
}