using System.Linq;
using System.Collections.Generic;
using Avalonia;
using System;

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
                else if (gameObject is Animals animal)
                {
                    _factory.NewMeat(animal.Location);
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
                    herbivore.Eat();
                    nearestPlant.Life -= 10; // Reduce plant's life

                }
            }

            // Find the nearest herbivore of the opposite gender for reproduction
            if (herbivore.Energy > 70)
            {
                var nearestMate = FindNearestMate(herbivore, herbivores);
                if (nearestMate != null && nearestMate.Energy > 70)
                {
                    herbivore.MoveTowards(nearestMate);

                    // Herbivores reproduce if they are at the same location
                    if (herbivore.IsAtLocation(nearestMate.Location))
                    {
                        _factory.NewHerbivore(herbivore.Location); // Create a new herbivore at the same location
                        herbivore.Energy -= 20;
                        nearestMate.Energy -= 20;
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
                    carnivore.Eat();
                    _viewModel.GameObjects.Remove(nearestMeat); // Remove meat if life is 0
                }
            }
            else if (nearestHerbivore != null)
            {
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

            // Find the nearest carnivore of the opposite gender for reproduction
            if (carnivore.Energy > 70)
            {
                var nearestMate = FindNearestMate(carnivore, carnivores);
                if (nearestMate != null && nearestMate.Energy > 70)
                {
                    carnivore.MoveTowards(nearestMate);

                    // Carnivores reproduce if they are at the same location
                    if (carnivore.IsAtLocation(nearestMate.Location))
                    {
                        _factory.NewCarnivore(carnivore.Location); // Create a new carnivore at the same location
                        carnivore.Energy -= 20;
                        nearestMate.Energy -= 20;
                    }
                }
            }

            carnivore.Tick();
        }

        // Update Plants
        foreach (var plant in plants)
        {
            plant.Tick();
            var nearestOrganicWaste = FindNearestOrganicWaste(plant, organicWastes);
            if (nearestOrganicWaste != null && plant.Energy < 30)
            {
                plant.Eat();
                _viewModel.GameObjects.Remove(nearestOrganicWaste); // Remove organic waste if eaten
            }
            if (plant.Energy > 80 && _viewModel.CurrentTick % 500 == 0)
            {
                _factory.NewPlant(GetRandomLocationWithinRange(plant.Location, plant.ReproductionRange)); // Create a new plant if energy is high enough
                plant.Energy -= 50;
            }
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
            foreach (var animal in herbivores.Concat<Animals>(carnivores)) // Concatenates the two lists of animals.
            {
                animal.Velocity = new Avalonia.Point(-animal.Velocity.X, -animal.Velocity.Y);
            }
        }
        if (_viewModel.CurrentTick % 200 == 0)
        {
            foreach (var animal in herbivores.Concat<Animals>(carnivores)) // Concatenates the two lists of animals.
            {
                _factory.NewOrganicWaste(animal.Location);
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

    private static OrganicWaste? FindNearestOrganicWaste(Plants plants, List<OrganicWaste> organicWastes)
    {
        return organicWastes
            .Where(organicWaste => plants.DistanceTo(organicWaste.Location) <= plants.RootRange) // the Where method is used to filter the plants within the detection range by checking the distance between each plant and the herbivore.
            .OrderBy(organicWaste => plants.DistanceTo(organicWaste.Location))
            .FirstOrDefault();
    }

    // Additional methods for finding mates
    private static Herbivores? FindNearestMate(Herbivores herbivore, List<Herbivores> herbivores)
    {
        return herbivores
            .Where(h => h != herbivore && h.Gender != herbivore.Gender && herbivore.DistanceTo(h.Location) <= herbivore.DetectionRange)
            .OrderBy(h => herbivore.DistanceTo(h.Location))
            .FirstOrDefault();
    }

    private static Carnivores? FindNearestMate(Carnivores carnivore, List<Carnivores> carnivores)
    {
        return carnivores
            .Where(c => c != carnivore && c.Gender != carnivore.Gender && carnivore.DistanceTo(c.Location) <= carnivore.DetectionRange)
            .OrderBy(c => carnivore.DistanceTo(c.Location))
            .FirstOrDefault();
    }
    private static Point GetRandomLocationWithinRange(Point origin, double range) // To creat a new Plant within a certain range of the parent plant.
    {
        var random = new Random();
        double angle = random.NextDouble() * Math.PI * 2;
        double radius = random.NextDouble() * range;
        double x = origin.X + radius * Math.Cos(angle);
        double y = origin.Y + radius * Math.Sin(angle);
        return new Point(x, y);
    }
}


// MEAL TIME AS CURRENT TIME SO I CAN SAY TO POOP AFTER 5000 TICKS STARTING FROM MEAL TIME. 