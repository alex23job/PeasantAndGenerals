using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVegeta : MonoBehaviour
{
    [SerializeField] private int _id;

    public int FoodID { get { return _id; } }

    private VegetaControl _vegetaControl = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetVegetaControl(VegetaControl fc)
    {
        _vegetaControl = fc;
    }

    private void OnMouseUp()
    {
        if (_vegetaControl != null)
        {
            _vegetaControl.HitToFood(gameObject);
        }
    }
}
