public enum Direction
{
    N,
    E,
    S,
    W
}
public static class DirectionSwitcher
{
    public static Direction TurnLeft(Direction direction) => direction switch
    {
        Direction.N => Direction.W,
        Direction.W => Direction.S,
        Direction.S => Direction.E,
        Direction.E => Direction.N,
        _ => throw new NotImplementedException(),
    };

    public static Direction TurnRight(Direction direction) => direction switch
    {
        Direction.N =>Direction.E,
        Direction.E =>Direction.S,
        Direction.S => Direction.W,
        Direction.W => Direction.N,
        _ => throw new NotImplementedException(),
    };
}

