using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Carnivores : GameObject {
    [ObservableProperty]
    private Point velocity = new Point(8.0, 5.0);
    public double Diameter => 50; // or any default diameter you want

    public Carnivores(Point location) : base(location) {

    }

    
    public Rect Bounds => new Rect(Location.X, Location.Y, Diameter, Diameter);

    public void Tick() {
        Location = Location + Velocity;
    }

    
}