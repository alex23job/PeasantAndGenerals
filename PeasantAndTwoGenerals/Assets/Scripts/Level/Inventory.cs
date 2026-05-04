using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class Inventory
{
    private List<InventoryItem> _items = new List<InventoryItem>();
    private List<ItemInfo> _infoItems = new List<ItemInfo>();

    public int CountItems { get { return _items.Count; } }
    public int CountInfo { get { return _infoItems.Count; } }

    public Inventory() { }
    public Inventory(string csv, char sep = '#', char sep2 = ';')
    {
        string[] ar = csv.Split(sep, System.StringSplitOptions.RemoveEmptyEntries);
        if (ar.Length > 0)
        {
            foreach (string item in ar)
            {
                _infoItems.Add(new ItemInfo(item));
            }
        }
    }
    public void ClearInventory()
    {
        _items.Clear();
    }
    public void AddItem(InventoryItem item)
    {
        foreach (InventoryItem it in _items)
        {
            if (item.ItemID == it.ItemID)
            {
                it.ChangeCount(item.Count);
                return;
            }
        }
        _items.Add(item);
    }

    public void AddItem(ItemInfo info)
    {
        foreach (ItemInfo it in _infoItems)
        {
            if (info.ItemID == it.ItemID)
            {
                it.ChangeCount(info.Count);
                return;
            }
        }
        _infoItems.Add(info);
    }

    public bool CheckItem(int id, int count = 1)
    {
        foreach (ItemInfo it in _infoItems)
        {
            if ((it.ItemID == id) && (it.Count >= count)) return true;
        }
        return false;
    }

    public void DecrementItem(int id, int count = 1)
    {
        foreach (ItemInfo it in _infoItems)
        {
            if ((it.ItemID == id) && (it.Count >= count))
            {
                it.ChangeCount(-count);
                return;
            }
        }
    }

    public InventoryItem GetItem(int id)
    {
        foreach( InventoryItem it in _items)
        {
            if (it.ItemID == id) return it;
        }
        return null;
    }

    public string ToCsvString(char sep = '#')
    {
        StringBuilder sb = new StringBuilder();
        foreach(ItemInfo item in _infoItems)
        {
            if (item.ItemID > 0) sb.Append($"{item.ToCsvString(';')}{sep}");
        }
        return sb.ToString();
    }
}

public class ItemInfo
{
    public int ItemID { get; set; }
    public int Count { get; set; }

    public ItemInfo() { }
    public ItemInfo(int itemID, int count)
    {
        ItemID = itemID;
        Count = count;
    }

    public ItemInfo(string csv, char sep = ';')
    {
        string[] ar = csv.Split(sep, System.StringSplitOptions.RemoveEmptyEntries);
        if (ar.Length == 2)
        {
            if (int.TryParse(ar[0], out int znID)) ItemID = znID;
            if (int.TryParse(ar[1], out int znCount)) Count = znCount;
        }
    }

    public void ChangeCount(int zn)
    {
        if ((zn < 0) && (Count > -zn))
        {
            Count += zn;
        }
        if (zn > 0) Count += zn;
    }

    public string ToCsvString(char sep = ';')
    {
        return $"{ItemID}{sep}{Count}{sep}";
    }

}
