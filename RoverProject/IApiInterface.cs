public interface IApiInterface
{
    void SetInitialRoverPosition(int x, int y, Direction dir);
    void Init(ISurface planet);
    bool SendCommands(string commands);
    (int, int, Direction) CurrentRoverPosition { get; }
}
