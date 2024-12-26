using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;

namespace EcosystemSimulatorProject.ViewModels;

//this file is a bridge between the View and the Models' behaviors

public partial class MainWindowViewModel : GameBase
{
    // Commands for the buttons in MainWindow.axaml
    public ICommand AddCarnivore { get; }
    public ICommand AddHerbivore { get; }
    public ICommand AddPlant { get; }

    // Fields
    public int Timer { get; set; }

    private double _windowWidth;
    private double _windowHeight;

    public double WindowWidth
    {
        get => _windowWidth;
        set => SetProperty(ref _windowWidth, value);
    }

    public double WindowHeight
    {
        get => _windowHeight;
        set => SetProperty(ref _windowHeight, value);
    }

    // List of objects to display
    public ObservableCollection<GameObject> GameObjects { get; } = [];

    private readonly GameObjectFactory _gameObjectFactory;
    private readonly GameTickHandler _gameTickHandler;

    public MainWindowViewModel()

    {
        _gameObjectFactory = new GameObjectFactory(this);
        _gameTickHandler = new GameTickHandler(this);

        AddCarnivore = new RelayCommand(() => _gameObjectFactory.NewCarnivore(null));
        AddHerbivore = new RelayCommand(() => _gameObjectFactory.NewHerbivore(null));
        AddPlant = new RelayCommand(() => _gameObjectFactory.NewPlant(null));
    }

    protected override void Tick()
    {
        _gameTickHandler.HandleTick();
    }
}
