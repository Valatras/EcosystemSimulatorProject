using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;

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

    // Liste des objets à afficher
    public ObservableCollection<GameObject> GameObjects { get; } = new();

    public MainWindowViewModel()
    {
        AddCarnivore = new RelayCommand(NewCarnivore);
        AddHerbivore = new RelayCommand(NewHerbivore);
        AddPlant = new RelayCommand(NewPlant);
    }

    private void NewCarnivore()
    {
        var carnivore = new Carnivores(new Point(Width / 2, Height / 2 ));
        GameObjects.Add(carnivore);
    }

    private void NewHerbivore()
    {
        var herbivore = new Herbivores(new Point(Width / 2 , Height / 2 ));
        GameObjects.Add(herbivore);
    }

    private void NewPlant()
    {
        var plant = new Plants(new Point(Width / 2 - 32, Height / 2 - 32));
        GameObjects.Add(plant);
    }

    private Plants? FindNearestPlant(Herbivores herbivore, List<Plants> plants)
    {
        return plants
            .Where(plant => herbivore.DistanceTo(plant.Location) <= herbivore.DetectionRange)
            .OrderBy(plant => herbivore.DistanceTo(plant.Location))
            .FirstOrDefault();
    }


    protected override void Tick()
    {
        // Separate lists for each type of GameObject
        var herbivores = GameObjects.OfType<Herbivores>().ToList();
        var carnivores = GameObjects.OfType<Carnivores>().ToList();
        var plants = GameObjects.OfType<Plants>().ToList();

        for (int i = GameObjects.Count - 1; i >= 0; i--)
        {
            var gameObject = GameObjects[i];

            if (gameObject.Life <= 0)
            {
                GameObjects.Remove(gameObject); // Remove object if life is 0
            }
        }

        // Update Herbivores
        foreach (var herbivore in herbivores)
        {
            // Find the nearest plant for each herbivore
            var nearestPlant = FindNearestPlant(herbivore, plants);

            if (nearestPlant != null)
            {
                herbivore.MoveTowards(nearestPlant);

                // Herbivore eats the plant if it's close enough
                if (herbivore.IsAtLocation(nearestPlant.Location))
                {
                    herbivore.Eat(nearestPlant);
                    if (nearestPlant.Life <= 0)
                    {
                        GameObjects.Remove(nearestPlant); // Remove plant if life is 0
                    }
                }
            }

            herbivore.Tick();
        }
    


        // Update Carnivores
        foreach (var carnivore in carnivores)
        {
            carnivore.Tick();
        }

        // Update Plants
        foreach (var plant in plants)
        {
            plant.Tick();
        }

        // Additional logic for switching velocities every 1000 ticks
        if (CurrentTick % 100 == 0)
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Velocity = new Point(-gameObject.Velocity.X, -gameObject.Velocity.Y);
            }
        }
    }
}
