using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Plants : Livings
{


    public double RootRange => detectionRange; // Getter for rootRange (range for Plants to eat OrganicWaste)
    public double ReproductionRange => contactRange; // Getter for reproductionRange (range for Plants to throw seeds)



    public Plants(Point location) : base(location)
    {
        energy = 100; // Initial energy value
        life = 100;   // Initial life value
        detectionRange = 400; // Define the root range for Plants to eat OrganicWaste
        contactRange = 600; // Define the contact range for Plants to be eaten by Herbivores
    }

    public void Eat()
    {
        Energy = 100;
    }

}
