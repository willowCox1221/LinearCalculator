using LinearCalculator.Unit_One;


namespace LinearCalculator.Unit_One;

public partial class CramersRule : ContentPage
{
    public CramersRule()
    {
        InitializeComponent();
    }

    private void OnCramersClicked(object sender, EventArgs e)
    {

        if (ModePicker.SelectedIndex == 0)
        {
            Solve2x2();
        }
        else
        {
            Solve3x3();
        }
    }

    private void Solve2x2()
    {
        try
        {
            double a = double.Parse(EntryA.Text);
            double b = double.Parse(EntryB.Text);
            double eVal = double.Parse(EntryE.Text);

            double c = double.Parse(EntryC.Text);
            double d = double.Parse(EntryD.Text);
            double fVal = double.Parse(EntryF.Text);

            // Determinant
            double D = (a * d) - (c * b);

            if (D == 0)
            {
                ResultLabel.Text = "No unique solution.";
                StepsLabel.Text = "";
                return;
            }

            // Dx and Dy
            double Dx = (eVal * d) - (fVal * b);
            double Dy = (a * fVal) - (c * eVal);

            double x = Dx / D;
            double y = Dy / D;

            ResultLabel.Text = $"x = {x:F2}, y = {y:F2}";

            // 🧾 Build Steps
            string steps = "";

            steps += "Step 1: Find D = (ad - bc)\n";
            steps += $"D = ({a} * {d}) - ({c} * {b}) = {D}\n\n";

            steps += "Step 2: Find Dx = (ed - fb)\n";
            steps += $"Dx = ({eVal} * {d}) - ({fVal} * {b}) = {Dx}\n\n";

            steps += "Step 3: Find Dy = (af - ce)\n";
            steps += $"Dy = ({a} * {fVal}) - ({c} * {eVal}) = {Dy}\n\n";

            steps += "Step 4: Solve\n";
            steps += $"x = Dx / D = {Dx} / {D} = {x:F2}\n";
            steps += $"y = Dy / D = {Dy} / {D} = {y:F2}";

            StepsLabel.Text = steps;
        }
        catch
        {
            ResultLabel.Text = "Invalid input.";
            StepsLabel.Text = "";
        }
    }

    private void Solve3x3()
    {
        try
        {
            double a = double.Parse(A.Text);
            double b = double.Parse(B.Text);
            double c = double.Parse(C.Text);
            double d = double.Parse(Dval.Text);

            double e = double.Parse(E.Text);
            double f = double.Parse(F.Text);
            double g = double.Parse(G.Text);
            double h = double.Parse(H.Text);

            double i = double.Parse(I.Text);
            double j = double.Parse(J.Text);
            double k = double.Parse(K.Text);
            double l = double.Parse(L.Text);

            double Det3x3(double a1, double b1, double c1,
                          double d1, double e1, double f1,
                          double g1, double h1, double i1)
            {
                return a1 * (e1 * i1 - f1 * h1)
                     - b1 * (d1 * i1 - f1 * g1)
                     + c1 * (d1 * h1 - e1 * g1);
            }

            double D = Det3x3(a, b, c, e, f, g, i, j, k);

            if (D == 0)
            {
                ResultLabel.Text = "No unique solution.";
                StepsLabel.Text = "";
                return;
            }

            double Dx = Det3x3(d, b, c, h, f, g, l, j, k);
            double Dy = Det3x3(a, d, c, e, h, g, i, l, k);
            double Dz = Det3x3(a, b, d, e, f, h, i, j, l);

            double x = Dx / D;
            double y = Dy / D;
            double z = Dz / D;

            ResultLabel.Text = $"x={x:F2}, y={y:F2}, z={z:F2}";

            string steps = "";

            steps += "System:\n";
            steps += $"{a}x + {b}y + {c}z = {d}\n";
            steps += $"{e}x + {f}y + {g}z = {h}\n";
            steps += $"{i}x + {j}y + {k}z = {l}\n\n";

            steps += $"D = {D}\n";
            steps += $"Dx = {Dx}\n";
            steps += $"Dy = {Dy}\n";
            steps += $"Dz = {Dz}\n\n";

            steps += $"x = {Dx}/{D} = {x:F2}\n";
            steps += $"y = {Dy}/{D} = {y:F2}\n";
            steps += $"z = {Dz}/{D} = {z:F2}";

            StepsLabel.Text = steps;
        }
        catch
        {
            ResultLabel.Text = "Invalid input.";
            StepsLabel.Text = "";
        }
    }


    private void OnModeChanged(object sender, EventArgs e)
    {
        if (ModePicker.SelectedIndex == 0) // 2x2
        {
            TwoByTwoLayout.IsVisible = true;
            ThreeByThreeLayout.IsVisible = false;
        }
        else // 3x3
        {
            TwoByTwoLayout.IsVisible = false;
            ThreeByThreeLayout.IsVisible = true;
        }
    }
}