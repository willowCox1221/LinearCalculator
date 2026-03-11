using System;
using System.Text;

namespace LinearCalculator.Unit_Three
{
    public partial class VerticalShearPage : ContentPage
    {
        public VerticalShearPage()
        {
            InitializeComponent();
        }

        private void OnShearClicked(object sender, EventArgs e)
        {
            try
            {
                double k = double.Parse(ShearValue.Text);

                StringBuilder steps = new StringBuilder();

                steps.AppendLine("VERTICAL SHEAR TRANSFORMATION\n");

                steps.AppendLine("Step 1: Standard Basis Vectors\n");

                steps.AppendLine("v₁ = [1]");
                steps.AppendLine("     [0]\n");

                steps.AppendLine("v₂ = [0]");
                steps.AppendLine("     [1]\n");

                steps.AppendLine("Step 2: Apply Mapping Rule\n");

                steps.AppendLine($"v₁ → v₁ + {k}v₂\n");

                steps.AppendLine($"= [1] + {k}[0]");
                steps.AppendLine($"  [0]   [{k}]\n");

                steps.AppendLine($"= [1]");
                steps.AppendLine($"  [{k}]\n");

                steps.AppendLine("Step 3: Second Column Unchanged\n");

                steps.AppendLine("v₂ = [0]");
                steps.AppendLine("     [1]\n");

                steps.AppendLine("Step 4: Build Transformation Matrix\n");

                steps.AppendLine($"[ 1  0 ]");
                steps.AppendLine($"[ {k}  1 ]");

                ResultLabel.Text = steps.ToString();
            }
            catch
            {
                ResultLabel.Text = "Invalid input.";
            }
        }
    }
}