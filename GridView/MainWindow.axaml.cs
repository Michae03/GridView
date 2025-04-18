using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GridView;

public class Person {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string Position { get; set; }
    

    public Person(Func<int> getId, string name = "", string surname= "", int age = 0, string position = "") {
        Id = getId();
        Name = name;
        Surname = surname;
        Age = age;
        Position = position;
    }
}

public partial class MainWindow : Window {
    public ObservableCollection<Person> Workers { get; }

    public int GetNewId() {
        int newId = 0;
        if (Workers.Count != 0) {
            newId = Workers.Last().Id + 1;
        }
        return newId;
    }
    public MainWindow()
    {
        Workers = new ObservableCollection<Person>();
        DataContext = this;
        InitializeComponent();
    }

    private async void Add_OnClick(object? sender, RoutedEventArgs e)
    {
       AddingWindow addingWindow = new AddingWindow();
       await addingWindow.ShowDialog(this);
       if (addingWindow.ShouldAdd) { 
           Workers.Add(new Person(GetNewId, addingWindow.Name, addingWindow.Surname, addingWindow.Age, addingWindow.Position));
       }
      
    }

    private void Delete_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedItem = WorkersDataGrid.SelectedItem as Person;
        if (selectedItem != null)
        {
            Workers.Remove(selectedItem);
        }
    }
}