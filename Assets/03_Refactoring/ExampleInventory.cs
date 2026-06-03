using UnityEngine;

public class ExampleInventory : MonoBehaviour
{
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = new Inventory(10);

        Item dragon = new Item("Dragon");
        Item elf = new Item("Elf");
        Item ogr = new Item("Org");

        //-------------------------------------------------------------------

        _inventory.TryAdd(dragon, 6);
        _inventory.TryAdd(elf, 3);

        ShowInventory();

        Debug.Log($"Всего предметов: {_inventory.CurrentCount}");

        //-------------------------------------------------------------------

        int dragons = _inventory.GetItemsBy("Dragon", 3);
        Debug.Log($"Получено : {dragons}");

        int elfs = _inventory.GetItemsBy("Elf", 2);
        Debug.Log($"Получено : {elfs}");

        Debug.Log($"Всего предметов: {_inventory.CurrentCount}");

        ShowInventory();

        //-------------------------------------------------------------------

        elfs = _inventory.GetItemsBy("Elf", 5);
        Debug.Log($"Получено : {elfs}");

        dragons = _inventory.GetItemsBy("Dragon", 12);
        Debug.Log($"Получено : {dragons}");

        Debug.Log($"Всего предметов: {_inventory.CurrentCount}");

        ShowInventory();

        //===================================================================

        _inventory.TryAdd(ogr, 3);
        _inventory.TryAdd(dragon, 2);
        _inventory.TryAdd(elf, 3);

        ShowInventory();

        Debug.Log($"Всего предметов: {_inventory.CurrentCount}");

        //-------------------------------------------------------------------

        dragons = _inventory.GetItemsBy("Dragon", 3);
        Debug.Log($"Получено : {dragons}");

        int ogrs = _inventory.GetItemsBy("Org", 2);
        Debug.Log($"Получено : {ogrs}");

        Debug.Log($"Всего предметов: {_inventory.CurrentCount}");

        ShowInventory();
    }

    private void ShowInventory()
    {
        Debug.Log("----------- Все предметы: -----------");

        foreach (InventoryCell cell in _inventory.Cells)
        {
            Debug.Log($"{cell.Item.Name}: {cell.Count}");
        }

        Debug.Log("-------------------------------------");
    }
}