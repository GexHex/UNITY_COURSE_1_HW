using System.Collections.Generic;
using System.Linq;

public class Inventory
{   
    private readonly List<InventoryCell> _cells = new();

    public Inventory(int maxCount)
    {
        MaxCount = maxCount;
    }

    public IReadOnlyList<InventoryCell> Cells => _cells;

    public int CurrentCount => _cells.Sum(cell => cell.Count);

    public int MaxCount { get; }

    public bool TryAdd(Item item, int count)
    {
        if (item == null || count <= 0)
            return false;

        if (CurrentCount + count > MaxCount)
            return false;

        InventoryCell cell = _cells.FirstOrDefault(c => c.Item.Name == item.Name);

        if (cell != null)
        {
            cell.Add(count);
        }
        else
        {
            _cells.Add(new InventoryCell(item, count));
        }

        return true;
    }

    public int GetItemsBy(string name, int count)
    {
        InventoryCell cell = _cells.FirstOrDefault(c => c.Item.Name == name);

        if (cell == null)
            return 0;

        int removed = cell.Remove(count);

        if (cell.Count == 0)
            _cells.Remove(cell);

        return removed;
    }
}