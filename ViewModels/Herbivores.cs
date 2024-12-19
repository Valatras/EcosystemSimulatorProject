using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Linq;
using static EcosystemSimulatorProject.ViewModels.MainWindowViewModel;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Herbivores : Animals
{
    public double width;
    public double height;
    

    public double DetectionRange => detectionRange; // define the detection range for Herbivores as Animals' one since it hasa protected field.

    public Herbivores(Point location) : base(location)
    {
        energy = 100; // Initial energy value for GameObject
        life = 100;   // Initial life value
        detectionRange = 400; // Detection range for Herbivores
    }

    private Point _velocity;
    public override Point Velocity
    {
        get => _velocity;
        set => SetProperty(ref _velocity, value);
    }

    public Rect Bounds;

    public override void Tick()
    {
        if (Energy > 30 && Life < 100)
        {
            Life += 0.2;
        }
        base.Tick();
    }

    public void Eat(Plants plant)
    {
        plant.Life -= 10; // Reduce plant's life
        if (Energy < 100) // Use the generated property instead of the field => Energy instead of energy
        {
            Energy += 20; // Restore herbivore's energy
        }
    }
}
