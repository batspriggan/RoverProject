/// <summary>
/// Defines the interface of a generic Rover that moves on a surface
/// MoveForward and MoveBackward makes the rover move, is the rover can't move due to an obstacle
/// the functions return false
/// 
/// </summary>
public interface IRover<ISurface>
{
    ISurface Surface { get; init; }
    bool MoveForward();
    bool MoveBackward();
    void TurnLeft();
    void TurnRight();

    (int, int, Direction) CurrentPosition { get; set; }

}
