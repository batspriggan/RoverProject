/// <summary>
/// Defines a simple surface
/// 0,0 is the lower left corner (SW)
/// Y axis is Lenght
/// X axis is Width 
/// SW = Length, Width
/// </summary>
public interface ISurface
{
    bool ThereIsAnObstacle(int x, int y);
    void AddObstacle(int x, int y);
    int Length { get; init; }
    int Width { get; init; }
}
