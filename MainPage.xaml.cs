using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BugTest;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }
}

public class MainPageViewModel : INotifyPropertyChanged
{
    private bool _isBusy = true;
    private bool _hasContent = false;
    private bool _isRefreshing = false;
    
    public bool IsBusy
    {
        get => _isBusy;
        set { _isBusy = value; OnPropertyChanged(); }
    }
    
    public bool HasContent
    {
        get => _hasContent;
        set { _hasContent = value; OnPropertyChanged(); }
    }
    
    public bool IsRefreshing
    {
        get => _isRefreshing;
        set { _isRefreshing = value; OnPropertyChanged(); }
    }
    
    public ObservableCollection<string> Items { get; } = new();

    public MainPageViewModel()
    {
        // Simulate loading
        LoadDataAsync();
    }

    private async void LoadDataAsync()
    {
        IsBusy = true;
        HasContent = false;
        
        // Simulate network delay
        await Task.Delay(1000);
        
        // Add items
        for (var i = 1; i <= 20; i++)
        {
            Items.Add($"Item {i}");
        }
        
        IsBusy = false;
        HasContent = true;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
