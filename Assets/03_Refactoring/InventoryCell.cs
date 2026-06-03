using UnityEngine;

public class InventoryCell
{
    public Item Item { get; }

    public int Count { get; private set; }

    public InventoryCell(Item item, int count)
    {
        Item = item;
        Count = count;
    }

    public void Add(int count)
    {
        Count += count;
    }

    public int Remove(int count)
    {
        int removed = Mathf.Min(count, Count);

        Count -= removed;

        return removed;
    }
}