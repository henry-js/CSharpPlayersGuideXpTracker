namespace Cli.Menu;

internal class ExitMenuItem : ActionMenuItem
{
    public ExitMenuItem(int index) : base(index, "Exit")
    {
    }
    public override MenuItemType Type => MenuItemType.Exit;
}
