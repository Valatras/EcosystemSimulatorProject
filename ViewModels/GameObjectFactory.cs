using Avalonia;
using Avalonia.Controls;

namespace EcosystemSimulatorProject.ViewModels;

// Entity creation methods called by MainWindowViewModel.
public class GameObjectFactory
{
    private readonly MainWindowViewModel _viewModel;

    public GameObjectFactory(MainWindowViewModel viewModel)
    {
        // the GameObjectFactory class is dependent on the MainWindowViewModel class. so "_viewModel" from GameObjectFactory is assigned to "viewModel" from MainWindowViewModel.
        _viewModel = viewModel;
    }

    public void NewCarnivore(Point? location)
    {
       
        if (location == null)
        {
            var carnivore = new Carnivores(new Point(_viewModel.WindowWidth / 2, _viewModel.WindowHeight / 2));
            _viewModel.GameObjects.Add(carnivore);
        }
        else if (location.HasValue)
        {
            var carnivore = new Carnivores(new Point(location.Value.X +20 , location.Value.Y+20));
            _viewModel.GameObjects.Add(carnivore);
        }
    }

    public void NewHerbivore(Point? location)
    {

        if (location == null)
        {
            var herbivore = new Herbivores(new Point(_viewModel.WindowWidth / 2, _viewModel.WindowHeight / 2));
            _viewModel.GameObjects.Add(herbivore);
        }
        else if (location.HasValue) { 
            var herbivore = new Herbivores(new Point(location.Value.X + 20, location.Value.Y + 20));
            _viewModel.GameObjects.Add(herbivore);
        }
        
        
    }

    public void NewPlant(Point? location)
    {

        if (location == null)
        {
            var plant = new Plants(new Point(_viewModel.WindowWidth / 2, _viewModel.WindowHeight / 2));
            _viewModel.GameObjects.Add(plant);
        }
        else if (location.HasValue)
        {
            var plant = new Plants(new Point(location.Value.X, location.Value.Y));
            _viewModel.GameObjects.Add(plant);
        }


    }


    public void NewOrganicWaste(Point location)
    {
        var organicWaste = new OrganicWaste(location);
        _viewModel.GameObjects.Add(organicWaste);
    }
    public void NewMeat(Point location)
    {
        var meat = new Meat(location);
        _viewModel.GameObjects.Add(meat);
    }
}
