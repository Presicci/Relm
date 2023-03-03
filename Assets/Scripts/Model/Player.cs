// ReSharper disable All
/// <summary>
/// Player object.
/// </summary>
/// <remarks>Thomas Presicci - https://github.com/Presicci</remarks>
public class Player
{
    private readonly Inventory _inventory = new(20);    // Starting inventory of size 20 for now, can always be changed

    public Inventory GetInventory()
    {
        return _inventory;
    }
}
        