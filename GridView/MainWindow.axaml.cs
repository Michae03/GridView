using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Text.Json;


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
    public Person(int id, string name = "", string surname= "", int age = 0, string position = "") {
        Id = id;
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

    private async void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        DirectoryWindow directoryWindow = new DirectoryWindow();
        await directoryWindow.ShowDialog(this);
        var directory = directoryWindow.directory;
        if (directory != "")
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectRoot, "csv_files" ,directory + ".csv");
            /*
            Console.Out.WriteLine(fullPath);
            Console.Out.WriteLine("\n");
            for (int i = 0; i < Workers.Count; i++)
            {
                Console.Out.WriteLine(Workers[i].Id + "," + Workers[i].Name + "," + Workers[i].Surname + "," + Workers[i].Age + "," + Workers[i].Position);
            }
            */
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
             writer.WriteLine("Id,Name,Surname,Age,Position");
             for (int i = 0; i < Workers.Count; i++)
             {
                 writer.WriteLine(Workers[i].Id + "," + Workers[i].Name + "," + Workers[i].Surname + "," + Workers[i].Age + "," + Workers[i].Position);
             }
            }
        }

        directoryWindow.directory = "";
    }

    private async void Load_OnClick(object? sender, RoutedEventArgs e)
    {
        Workers.Clear();
        DirectoryWindow directoryWindow = new DirectoryWindow();
        await directoryWindow.ShowDialog(this);
        var directory = directoryWindow.directory;
        string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
        string fullPath = Path.Combine(projectRoot, "csv_files" ,directory + ".csv");
        string line;
        if (!File.Exists(fullPath))
        {
            return;
        }
        using (StreamReader reader = new StreamReader(fullPath))
        {
            reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
                var record = line.Split(',');
                Workers.Add(new Person(int.Parse(record[0]), record[1], record[2], int.Parse(record[3]), record[4]));
            }
        }
    }
    private void SaveToJson(string filePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true 
        };

        string jsonString = JsonSerializer.Serialize(Workers, options);
        File.WriteAllText(filePath, jsonString);
    }
    private async void SaveJson_OnClick(object? sender, RoutedEventArgs e)
    {
        DirectoryWindow directoryWindow = new DirectoryWindow();
        await directoryWindow.ShowDialog(this);
        var directory = directoryWindow.directory;
        if (directory != "")
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectRoot, "json_files", directory + ".json");
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
            SaveToJson(fullPath);
        }

        directoryWindow.directory = "";
    }

}