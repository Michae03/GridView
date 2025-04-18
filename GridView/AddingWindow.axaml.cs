using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace GridView;

public partial class AddingWindow : Window
{
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public int Age { get; set; } = 0;
    public string Position { get; set; } = "";
    public AddingWindow() {
        InitializeComponent();
    }
    public bool ShouldAdd {get; set;} = false;

    private void Confirm_OnClick(object? sender, RoutedEventArgs e) { 
        Name = NameBox.Text;
        Surname = SurnameBox.Text;
        if (int.TryParse(AgeBox.Text, out int age)) {
            Age = age;
        }else {
            Age = 0;
        }

        if (PositionBox.SelectedItem is ComboBoxItem item)
        {
            Position = item.Tag.ToString(); 
        }
        else {
            Position = "";
        }
        ShouldAdd = true;
        Close();
        
    }

    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        ShouldAdd = false;
        Close();
    }
}