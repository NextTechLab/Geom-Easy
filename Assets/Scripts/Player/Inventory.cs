using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<IStorable> items;

    public void AddItemToStorage(IStorable item)
    {
        items.Add(item);
    }
    
    public void ProcureItemFromStorage()
    {
        
    }
}