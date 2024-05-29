
public interface IUICommand
{
    bool CanExecute(object parameter);

    void Execute(object parameter);
}

