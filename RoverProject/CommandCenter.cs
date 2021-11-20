/// <summary>
/// ICommand interface defines the functions to interact with a Rover
/// </summary>
public class CommandCenter : ICommand<Rover>
{
    public bool InitRoverPosition(int x, int y, Direction dir, Rover rover)
    {
        rover.CurrentPosition = (x, y, dir);
        return true;
    }

    public bool SendCommands(string commandList, Rover rover)
    {
        foreach (char c in commandList)
        {
            //se il comando non è eseguibile ritorna non proseguo l'esecuzione dei comandi in coda
            if (!GiveCommand(c, rover))
                return false;
        }
        return true;
    }

    List<char> _allowedCommands = new List<char>() { 'f', 'b', 'l', 'r' };

    public bool IsCommandValid(char c) => _allowedCommands.Contains(c);

    /// <summary>
    /// Rover command manager.
    /// if the command exist among the ones allowed for this command manager is executed
    /// </summary>
    /// <param name="command">command value</param>
    /// <returns>
    /// true: the command have been executed
    /// false : the command is not valid or have not been executed
    /// </returns>
    public bool GiveCommand(char command, Rover rover)
    {
        if (!_allowedCommands.Contains(command))
            return false;
        bool result = true;
        if (command == 'l')
            rover.TurnLeft();
        else if (command == 'r')
            rover.TurnRight();
        else if (command == 'f')
            result = rover.MoveForward();
        else if (command == 'b')
            result = rover.MoveBackward();
        return result;
    }
}