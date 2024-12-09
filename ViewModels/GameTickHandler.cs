using System.Linq;
using System.Collections.Generic;

namespace EcosystemSimulatorProject.ViewModels;

// handles time effects in the simulation
public class GameTickHandler
{
    private readonly MainWindowViewModel _viewModel;
    private readonly GameObjectFactory _factory;

    public GameTickHandler(MainWindowViewModel viewModel)
    {
        _viewModel = viewModel;
        _factory = new GameObjectFactory(viewModel);
    }

    public void HandleTick()
    {
        // Separate lists for each type of GameObject
        var herbivores = _viewModel.GameObjects.OfType<Herbivores>().ToList();
        var carnivores = _viewModel.GameObjects.OfType<Carnivores>().ToList();
        var plants = _viewModel.GameObjects.OfType<Plants>().ToList();
        var organicWastes = _viewModel.GameObjects.OfType<OrganicWaste>().ToList();

        for (int i = _viewModel.GameObjects.Count - 1; i >= 0; i--)
        {
            var gameObject = _viewModel.GameObjects[i];

            if (gameObject.Life <= 0)
            {
                if (gameObject is Plants plant)
                {
                    _factory.NewOrganicWaste(plant.Location);
                }
                _viewModel.GameObjects.Remove(gameObject); // Remove object if life is 0
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
                        _factory.NewOrganicWaste(nearestPlant.Location);
                        _viewModel.GameObjects.Remove(nearestPlant); // Remove plant if life is 0
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

        // Update Organic Wastes
        foreach (var organicWaste in organicWastes)
        {
            organicWaste.Tick();
        }

        // Additional logic for switching velocities every 1000 ticks
        if (_viewModel.CurrentTick % 100 == 0)
        {
            foreach (var gameObject in _viewModel.GameObjects)
            {
                gameObject.Velocity = new Avalonia.Point(-gameObject.Velocity.X, -gameObject.Velocity.Y);
            }
        }
    }

    private Plants? FindNearestPlant(Herbivores herbivore, List<Plants> plants)
    {
        return plants
            .Where(plant => herbivore.DistanceTo(plant.Location) <= herbivore.DetectionRange)
            .OrderBy(plant => herbivore.DistanceTo(plant.Location))
            .FirstOrDefault();
    }
}
