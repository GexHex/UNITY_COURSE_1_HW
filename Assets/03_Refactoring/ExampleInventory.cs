using System.Collections.Generic;
using UnityEngine;

public class ExampleInventory : MonoBehaviour
{
    Inventory _inventory;

    private void Awake()
    {
        _inventory = new Inventory(10);

        Item dragon = new Item("Dragon");
        Item elf = new Item("Elf");
        Item ogr = new Item("Org");


        _inventory.TryAdd(dragon);
        _inventory.TryAdd(dragon);
        _inventory.TryAdd(dragon);
        _inventory.TryAdd(dragon);
        _inventory.TryAdd(dragon);
        _inventory.TryAdd(dragon);   

        _inventory.TryAdd(elf);
        _inventory.TryAdd(elf);
        _inventory.TryAdd(elf);

        foreach (Item item in _inventory.Items)
        {
            Debug.Log(item.Name);
        }

        Debug.Log($"Всего предметов: {_inventory.CurrentCount}");
        //-------------------------------------------------------------------
        List<Item> dragons = _inventory.GetItemsBy("Dragon", 3);
        Debug.Log($"Получено : {dragons.Count}");

        List<Item> elfs = _inventory.GetItemsBy("Elf", 2);
        Debug.Log($"Получено : {elfs.Count}");

        Debug.Log($"Всего предметов: {_inventory.CurrentCount}");
        //-------------------------------------------------------------------
        elfs = _inventory.GetItemsBy("Elf", 5);
        Debug.Log($"Получено : {elfs.Count}");

        dragons = _inventory.GetItemsBy("Dragon", 12);
        Debug.Log($"Получено : {dragons.Count}");       

        Debug.Log($"Всего предметов: {_inventory.CurrentCount}");
        //===================================================================
        Debug.Log($"Все предметы:");

        foreach (Item item in _inventory.Items)
        {
            Debug.Log(item.Name);
        }

        _inventory.TryAdd(ogr);      
        _inventory.TryAdd(ogr);
        _inventory.TryAdd(ogr);
        _inventory.TryAdd(dragon);
        _inventory.TryAdd(dragon);

        _inventory.TryAdd(elf);
        _inventory.TryAdd(elf);
        _inventory.TryAdd(elf);

        foreach (Item item in _inventory.Items)
        {
            Debug.Log(item.Name);
        }

        Debug.Log($"Всего предметов: {_inventory.CurrentCount}");
        //-------------------------------------------------------------------
        dragons = _inventory.GetItemsBy("Dragon", 3);
        Debug.Log($"Получено : {dragons.Count}");

        List<Item> Ogrs = _inventory.GetItemsBy("Org", 2);
        Debug.Log($"Получено : {Ogrs.Count}");

        Debug.Log($"Всего предметов: {_inventory.CurrentCount}");
        //===================================================================
        Debug.Log($"Все предметы:");

        foreach (Item item in _inventory.Items)
        {
            Debug.Log(item.Name);
        }
    }
}