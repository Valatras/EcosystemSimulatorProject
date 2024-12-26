using Avalonia;

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

    public void NewCarnivore()
    {
        var carnivore = new Carnivores(new Point(_viewModel.WindowWidth / 2, _viewModel.WindowHeight / 2));
        _viewModel.GameObjects.Add(carnivore);
    }

    public void NewHerbivore()
    {
        var herbivore = new Herbivores(new Point(_viewModel.WindowWidth / 2, _viewModel.WindowHeight / 2));
        _viewModel.GameObjects.Add(herbivore);
    }

    public void NewPlant()
    {
        var plant = new Plants(new Point(_viewModel.WindowWidth / 2 , _viewModel.WindowHeight / 2 ));
        _viewModel.GameObjects.Add(plant);
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
