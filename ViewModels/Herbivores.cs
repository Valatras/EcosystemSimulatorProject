using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Linq;
using static EcosystemSimulatorProject.ViewModels.MainWindowViewModel;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Herbivores : Animals
{

    public string Gender => gender;



    public Herbivores(Point location) : base(location)
    {
        energy = 100; // Initial energy value for GameObject
        life = 100;   // Initial life value
        detectionRange = 400; // Detection range for Herbivores
        contactRange = 40; // Contact range for Herbivores

    }

    private Point _velocity;
    public override Point Velocity
    {
        get => _velocity;
        set => SetProperty(ref _velocity, value);
    }

    public override void Tick()
    {
        if (Energy > 30 && Life < 100)
        {
            Life += 0.2;
        }
        base.Tick();
    }

    public void Eat()
    {
        
        if (Energy < 100) // Use the generated property instead of the field => Energy instead of energy
        {
            Energy += 20; // Restore herbivore's energy
        }
    }
}
