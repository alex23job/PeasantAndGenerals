using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFood : MonoBehaviour
{
    [SerializeField] private int _id;

    public int FoodID { get { return _id; } }

    private FoodControl _foodControl = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetFoodControl(FoodControl fc)
    {
        _foodControl = fc;
    }

    private void OnMouseUp()
    {
        if (_foodControl != null)
        {
            _foodControl.HitToFood(gameObject);
        }
    }
}
