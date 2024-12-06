using System.Collections.ObjectModel;
using System;
using Avalonia;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace EcosystemSimulatorProject.ViewModels;


public partial class MainWindowViewModel : GameBase
{
    // Commands for the buttons in MainWindow.axaml

    public ICommand AddCarnivore { get; }
    public ICommand AddHerbivore { get; }
    public ICommand AddPlant { get; }

    //fields

    public int Timer { get; set; }
    public int Width { get; } = 800;
    public int Height { get; } = 450;

    private Carnivores carnivores;
    


    // Liste des objets à afficher
    public ObservableCollection<GameObject> GameObjects { get; } = new();

    public MainWindowViewModel()
    {
        //New object added in the center
        carnivores = new Carnivores(new Point(Width / 2 - 32, Height / 2 - 32));
        GameObjects.Add(carnivores);
        AddCarnivore = new RelayCommand(NewCarnivore);
        AddHerbivore = new RelayCommand(NewHerbivore);
        AddPlant = new RelayCommand(NewPlant);
    }

    private void NewCarnivore()
    {
        var carnivore = new Carnivores(new Point(Width / 2 - 32, Height / 2 - 32));
        GameObjects.Add(carnivore);

    }

    private void NewHerbivore()
    {
        var herbivore = new Herbivores(new Point(Width / 2 - 32, Height / 2 - 32));
        GameObjects.Add(herbivore);
    }

    private void NewPlant()
    {
        var plant = new Plants(new Point(Width / 2 - 32, Height / 2 - 32));
        GameObjects.Add(plant);
    }

    

    public void OnKeyAPressed()
    {
        // Example functionality: output a message when 'A' is pressed
        Console.WriteLine("The 'A' key was pressed.");
    }



    protected override void Tick()
    {
        carnivores.Tick();

        // Randomize the Carnivores.Velocity value after each carnivores.Tick()

        if (carnivores.Location.X >= Width - 32 || carnivores.Location.X <= 0)
        {
            carnivores.Velocity = new Point(-carnivores.Velocity.X, carnivores.Velocity.Y);
        }
        else if (carnivores.Location.Y >= Height - 32 || carnivores.Location.Y <= 0)
        {
            carnivores.Velocity = new Point(carnivores.Velocity.X, -carnivores.Velocity.Y);
        }

        // Switch the values between the first and second values of carnivores.Velocity after 1000 ticks
        if (CurrentTick % 1000 == 0)
        {
            carnivores.Velocity = new Point(carnivores.Velocity.Y, carnivores.Velocity.X);
            Timer++;
        }

    }


}
