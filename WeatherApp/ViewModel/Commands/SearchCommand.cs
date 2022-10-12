using System;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands;

public class SearchCommand : ICommand
{
    public WeatherVM VM { get; set; }

    public SearchCommand(WeatherVM vm)
    {
        VM = vm;
    }

    public bool CanExecute(object parameter)
    {
        string query = parameter as string;

        if(string.IsNullOrEmpty(query))
            return false;

        return true;
    }

    public void Execute(object parameter)
    {
        VM.MakeQuery();
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}

