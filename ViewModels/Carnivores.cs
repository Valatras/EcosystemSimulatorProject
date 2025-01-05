using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Carnivores : Animals
{
    public string Gender => gender;

    public Carnivores(Point location) : base(location)
    {
        energy = 100; // Initial energy value
        life = 100;   // Initial life value
        detectionRange = 500; // Detection range for Carnivores
        contactRange = 40; // Contact range for Carnivores
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

    public void Hunt(Herbivores herbivore)
    {
        herbivore.Life -= 10; // Reduce herbivore's life
    }

    public void Eat()
    {
        if (Energy < 100) // Use the generated property instead of the field => Energy instead of energy
        {
            Energy += 60; // Restore carnivore's energy
        }
    }


}
