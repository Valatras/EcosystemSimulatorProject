using System.Collections.ObjectModel;
using System;
using Avalonia;

namespace EcosystemSimulatorProject.ViewModels;

public partial class MainWindowViewModel : GameBase
{
    public int Width { get; } = 800;
    public string Greeting { get; } = "I'm progressing bitch !";
    public int Height { get; } = 450;
    private Carnivores carnivores;
    


    // Liste des objets à afficher
    public ObservableCollection<GameObject> GameObjects { get; } = new();

    public MainWindowViewModel()
    {

        carnivores = new Carnivores(new Point(Width / 2 - 32, Height / 2 - 32));
        GameObjects.Add(carnivores);
    }

    public void OnKeyAPressed()
    {
        // Example functionality: output a message when 'A' is pressed
        Console.WriteLine("The 'A' key was pressed.");
    }



    protected override void Tick()
    {
        carnivores.Tick();

        if (carnivores.Location.X >= Width - 32 || carnivores.Location.X <= 0)/* */
        {
            carnivores.Velocity = new Point(-carnivores.Velocity.X, carnivores.Velocity.Y);
        }
        else if (carnivores.Location.Y >= Height - 32 || carnivores.Location.Y <= 0)
        {
            carnivores.Velocity = new Point(carnivores.Velocity.X, -carnivores.Velocity.Y);
        }



    }


}
