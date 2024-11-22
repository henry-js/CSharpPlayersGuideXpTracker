namespace Cli.Menu;

internal class BackMenuItem : ActionMenuItem
{
    public BackMenuItem(int index) : base(index, "Back")
    {
    }
    public override MenuItemType Type => MenuItemType.Back;
}
