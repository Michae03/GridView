using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace GridView;

public partial class DirectoryWindow : Window
{
    public string directory { get; set; } = "";
    public DirectoryWindow()
    {
        InitializeComponent();
    }

    private void Accept_OnClick(object? sender, RoutedEventArgs e)
    {
        directory = Dir.Text;
        //Console.Out.WriteLine(directory);
        Close();
    }

    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        directory = "";
        Close();
    }
}