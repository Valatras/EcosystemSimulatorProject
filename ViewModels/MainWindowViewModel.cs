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
        for (int i = GameObjects.Count - 1; i >= 0; i--)
        {
            var gameObject = GameObjects[i];
            gameObject.Tick();
            if (gameObject.Life <= 0)
            {
                GameObjects.RemoveAt(i); // Remove object if life is 0
            }
            else if (CurrentTick % 1000 == 0)
            {
                gameObject.Velocity = new Point(gameObject.Velocity.Y, gameObject.Velocity.X);
                // Switch the values between the first and second values of carnivores.Velocity after 1000 ticks
            }
        }
    }


}
