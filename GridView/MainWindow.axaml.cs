using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
namespace GridView;

public class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public Person(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
}
public partial class MainWindow : Window
{
    public ObservableCollection<Person> Workers { get;}
    public MainWindow()
    {
        var people = new List<Person> 
        {
            new Person("Neil", "Armstrong"),
            new Person("Buzz", "Lightyear"),
            new Person("James", "Kirk")
        };
        Workers = new ObservableCollection<Person>(people);
        InitializeComponent();
    }
}