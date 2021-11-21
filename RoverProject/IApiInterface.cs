public interface IApiInterface
{
    void SetInitialRoverPosition(int x, int y, Direction dir);
    void Init(ISurface planet);
    bool SendCommands(string commands, out bool obstacleDetected);
    (int x, int y) LastDetectedObstacle { get;  }
    (int, int, Direction) CurrentRoverPosition { get; }
}
