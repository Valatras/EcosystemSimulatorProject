using System.Linq;
using System.Collections.Generic;
using Avalonia;

namespace EcosystemSimulatorProject.ViewModels;

// handles time effects in the simulation
public class GameTickHandler(MainWindowViewModel viewModel)
{
    private readonly MainWindowViewModel _viewModel = viewModel;
    private readonly GameObjectFactory _factory = new(viewModel);

    public void HandleTick()
    {
        // Separate lists for each type of GameObject
        var herbivores = _viewModel.GameObjects.OfType<Herbivores>().ToList();
        var carnivores = _viewModel.GameObjects.OfType<Carnivores>().ToList();
        var plants = _viewModel.GameObjects.OfType<Plants>().ToList();
        var organicWastes = _viewModel.GameObjects.OfType<OrganicWaste>().ToList();
        var meats = _viewModel.GameObjects.OfType<Meat>().ToList();

        for (int i = _viewModel.GameObjects.Count - 1; i >= 0; i--)
        {
            var gameObject = _viewModel.GameObjects[i];

            if (gameObject.Life <= 0)
            {
                if (gameObject is Plants plant)
                {
                    _factory.NewOrganicWaste(plant.Location);
                }
                else if (gameObject is Meat meat)
                {
                    _factory.NewOrganicWaste(meat.Location);
                }
                else if (gameObject is Herbivores herbivore)
                {
                    _factory.NewMeat(herbivore.Location);
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
            
            
            // Find the nearest herbivore for each carnivore
            var nearestHerbivore = FindNearestHerbivore(carnivore, herbivores);
            var nearestMeat = FindNearestMeat(carnivore, meats);

            if (nearestMeat != null)
            {
                carnivore.MoveTowards(nearestMeat);

                // Carnivore eats the meat if it's close enough
                if (carnivore.IsAtLocation(nearestMeat.Location))
                {
                    carnivore.Eat(nearestMeat);
                    
                    
                    _viewModel.GameObjects.Remove(nearestMeat); // Remove meat if life is 0
                    
                }
            } else if (nearestHerbivore != null) {
                carnivore.MoveTowards(nearestHerbivore);

                // Carnivore hunts the herbivore if it's close enough
                if (carnivore.IsAtLocation(nearestHerbivore.Location))
                {
                    carnivore.Hunt(nearestHerbivore);
                    if (nearestHerbivore.Life <= 0)
                    {
                        _factory.NewMeat(nearestHerbivore.Location);
                        _viewModel.GameObjects.Remove(nearestHerbivore); // Remove herbivore if life is 0
                    }
                }
            }

                
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

        foreach (var meat in meats)
        {
            meat.Tick();
        }

        // Additional logic for switching velocities every 1000 ticks
        if (_viewModel.CurrentTick % 100 == 0)
        {
            // Additional logic for switching velocities every 1000 ticks
            if (_viewModel.CurrentTick % 100 == 0)
            {
                foreach (var animal in herbivores.Concat<Animals>(carnivores)) // Concatenates the two lists of animals.
                {
                    animal.Velocity = new Avalonia.Point(-animal.Velocity.X, -animal.Velocity.Y);
                }
            }
        }
    }

    private static Plants? FindNearestPlant(Herbivores herbivore, List<Plants> plants)
    {
        return plants
            .Where(plant => herbivore.DistanceTo(plant.Location) <= herbivore.DetectionRange) // the Where method is used to filter the plants within the detection range by checking the distance between each plant and the herbivore.
            .OrderBy(plant => herbivore.DistanceTo(plant.Location))
            .FirstOrDefault();
    }

    private static Herbivores? FindNearestHerbivore(Carnivores carnivore, List<Herbivores> herbivores)
    {
        return herbivores
            .Where(herbivore => carnivore.DistanceTo(herbivore.Location) <= carnivore.DetectionRange) // the Where method is used to filter the plants within the detection range by checking the distance between each plant and the herbivore.
            .OrderBy(herbivore => carnivore.DistanceTo(herbivore.Location))
            .FirstOrDefault();
    }
    private static Meat? FindNearestMeat(Carnivores carnivore, List<Meat> meats)
    {
        return meats
            .Where(meat => carnivore.DistanceTo(meat.Location) <= carnivore.DetectionRange) // the Where method is used to filter the plants within the detection range by checking the distance between each plant and the herbivore.
            .OrderBy(meat => carnivore.DistanceTo(meat.Location))
            .FirstOrDefault();
    }
}
