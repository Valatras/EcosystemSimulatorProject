using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Plants : Livings
{

    private double rootRange = 100; // Define the root range for Plants to eat OrganicWaste

    public double RootRange => rootRange; // Getter for rootRange

    public Plants(Point location) : base(location)
    {
        energy = 100; // Initial energy value
        life = 100;   // Initial life value
    }

    void Eat(OrganicWaste organicWaste)
    {
        if (Energy < 20) // Use the generated property instead of the field => Energy instead of energy
        {
            Energy += 100; // Restore plant's energy
        }
    }

}
