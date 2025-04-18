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

    private void Confirm_OnClick(object? sender, RoutedEventArgs e) { 
        Name = NameBox.Text;
        Surname = SurnameBox.Text;
        if (int.TryParse(AgeBox.Text, out int age)) {
            Age = age;
        }else {
            Age = 0;
        }

        if (PositionBox.SelectedItem != null)
        {
            //DO ZROBIENIA JAK WRÓCE
        }
        else {
            Position = "";
        }
       
        Close();
        
    }
}