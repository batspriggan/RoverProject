// See https://aka.ms/new-console-template for more information
//You’re part of the team that explores Mars by sending remotely controlled vehicles to the surface of the planet.
//Develop an API that translates the commands sent from earth to instructions that are understood by the rover.

//Requirements

//                • You are given the initial starting point (x, y) of a rover and the direction (N, S, E, W) it is facing.
//                • The rover receives a character array of commands.
//                • Implement commands that move the rover forward/backward (f, b).
//                • Implement commands that turn the rover left/right (l, r).
//                • Implement wrapping from one edge of the grid to another. (planets are spheres after all)
//                • Implement obstacle detection before each move to a new square.If a given sequence of commands encounters
//                  an obstacle, the rover moves up to the last possible point, aborts the sequence and reports the obstacle.

public class RoverApiInterface : IApiInterface
{
    CommandCenter _commandCenter = new CommandCenter();
    ISurface? _planetSurface;
    Rover? _rover;
    public (int, int, Direction) CurrentRoverPosition { get => _rover!.CurrentPosition; }

    public bool SendCommands(string  commands)
    {
        if (_commandCenter != null)
            return _commandCenter.SendCommands(commands, _rover!);
        return false;
    }

    public void SetInitialRoverPosition(int x, int y, Direction dir)
    {
        _rover!.CurrentPosition = (x, y, dir);
    }

    public void Init(ISurface planet)
    {
        _planetSurface = planet;
        _rover = new Rover(_planetSurface);
        _commandCenter = new CommandCenter();
    }
}