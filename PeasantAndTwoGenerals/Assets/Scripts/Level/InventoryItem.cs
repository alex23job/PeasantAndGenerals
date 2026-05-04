using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour, ITaking
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;

    private int _count = 0;

    public int Count { get { return _count; } }
    public string Name { get { return _name; } }
    //public string Description { get { return _description; } }
    public Sprite Icon { get { return _icon; } }
    public int ItemID { get { return _id; } }

    string ITaking.Description { get => _description; set => _description = value; }

    public int TakingID { get { return _id; } }

    public string TakingName { get { return _name; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddingParams(string nm, string descr, Sprite icon)
    {
        _name = nm;
        _description = descr;
        _icon = icon;
    }

    public void ChangeCount(int zn)
    {
        if ((zn < 0) && (_count > Mathf.Abs(zn)))
        {
            _count += zn;
        }
        if (zn > 0) _count += zn;
    }

    public string ToCsvString(char sep = ';')
    {
        return $"{_id}{sep}{_count}{sep}";
    }

    public GameObject TakingItem()
    {
        return gameObject;
    }

    public void HideItem()
    {
        
    }
}
