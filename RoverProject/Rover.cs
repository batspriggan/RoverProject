public class Rover : IRover<ISurface>
{
    internal int _x, _y;
    internal Direction _direction;
    ///Depending on direction we have a dictionary of a factory multiplied to determine where we are moving to
    /// in case of forward direction
    internal Dictionary<Direction, (int xfactor, int yfactor)> _moveFactor = new Dictionary<Direction, (int, int)>() 
    { 
        { Direction.N, (0, 1) }, 
        { Direction.E, (1, 0) }, 
        { Direction.S, (0, -1) }, 
        { Direction.W, (-1, 0) } 
    };

    public ISurface Surface { get; init; }

    public Rover(ISurface surface) => Surface = surface;
    

    public (int, int, Direction) CurrentPosition
    {
        get => (_x, _y, _direction);
        set
        {
            _x = value.Item1;
            _y = value.Item2;
            _direction = value.Item3;
        }
    }

    private void FixBoundariesCoordinates()
    {
        if (_x < 0)
            _x = Surface.Length;
        else if (_x > Surface.Length)
            _x = 0;
        if (_y < 0)
            _y = Surface.Width;
        else if (_y > Surface.Width)
            _y = 0;
    }

    internal bool MakeMove(bool forward)
    {
        int _prevx = _x;
        int _prevy = _y;
        // we make move based on forward parameter and vector factor
        // if forward is false we need to move negating the factors got from the _moveFactor
        int negateMovement = forward ? 1 : -1; 
        var move = _moveFactor[_direction];
        _x += negateMovement * move.xfactor;
        _y += negateMovement * move.yfactor;

        FixBoundariesCoordinates();

        if (Surface.ThereIsAnObstacle(_x, _y))
        {
            Surface.AddObstacle(_x, _y);
            _x = _prevx;
            _y = _prevy;
            return false;
        }
        return true;
    }

    public bool MoveBackward() => MakeMove(false);
    public bool MoveForward() => MakeMove(true);

    public void TurnLeft()
    {
        _direction = DirectionSwitcher.TurnLeft(_direction);
    }

    public void TurnRight()
    {
        _direction = DirectionSwitcher.TurnRight(_direction);
    }
}
