namespace BlazorAppAndCommand.Pages;

public class RelayCommand : IUICommand
{
    private readonly Action execute;
    private readonly Func<bool> canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return canExecute?.Invoke() ?? false;
    }

    public void Execute(object parameter)
    {
        execute?.Invoke();
    }
}
