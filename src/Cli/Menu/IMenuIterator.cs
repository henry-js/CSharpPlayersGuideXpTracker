namespace Cli.Menu;

public interface IMenuIterator
{
    IMenuItem Current { get; }
    bool CanMoveNext { get; }
    bool CanMovePrevious { get; }
}
