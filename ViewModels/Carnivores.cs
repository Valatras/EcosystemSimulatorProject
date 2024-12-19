using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Carnivores : Animals
{
    public double width;
    public double height;
    public double DetectionRange => detectionRange; // define the detection range for Herbivores as Animals' one since it hasa protected field.


    public Carnivores(Point location) : base(location)
    {
        energy = 100; // Initial energy value
        life = 100;   // Initial life value
        detectionRange = 500; // Detection range for Carnivores
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

    public void Hunt(Herbivores herbivore)
    {
        herbivore.Life -= 10; // Reduce herbivore's life
        if (herbivore.Life <= 0)
        {
            Console.WriteLine("Herbivore killed");
        }
    }

    public void Eat(Meat meat)
    {
        Console.WriteLine("Carnivore is eating meat");
        if (Energy < 100) // Use the generated property instead of the field => Energy instead of energy
        {
            Energy += 60; // Restore carnivore's energy
        }
    }


}
