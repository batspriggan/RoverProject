public class Planet : ISurface
{
    public Planet(int length, int width)
    {
        Length = length;
        Width = width;
    }
    
    public int Length { get; init; }

    public int Width { get; init; }

    public List<(int, int)> _obstacles = new();

    public void AddObstacle(int x, int y)
    {
        _obstacles.Add((x, y));
    }

    public bool ThereIsAnObstacle(int x, int y)
    {
        if (_obstacles.Contains((x, y)))
            return true;
        return false;
    }
}