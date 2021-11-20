/// <summary>
/// ICommand interface defines the functions to interact with a rover
/// </summary>
public interface ICommand<T> where T: class, IRover<ISurface>
{
    bool SendCommands(string commandList, T rover);

    bool InitRoverPosition(int x, int y, Direction dir, T rover);
    bool IsCommandValid(char c);
}
