using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetaControl : MonoBehaviour
{
    [SerializeField] private GameObject[] _vegetaPrefabs;
    [SerializeField] private VegetaUI _vegetaUI;
    [SerializeField] private int _gameSecond = 120;

    private VegetaControl _vegetaControl;
    private float _timer = 0;
    private int _countAppels = 0;
    private int _countSlivas = 0;
    private int _gameTime = 0;
    private bool _isPlay = true;
    private int _attempts = 5;

    private void Awake()
    {
        _vegetaControl = GetComponent<VegetaControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.currentPlayer.inventory.CheckItem(10, 1)) _attempts = 12;
        CreatePole();
        _vegetaUI.SetTotalTime(_gameSecond);
        _vegetaUI.ViewTime(_gameTime);
        _vegetaUI.ViewArrows(_countAppels);
        _vegetaUI.ViewBirds(_countSlivas);
        _vegetaUI.ViewAttempts(_attempts);

    }

    private void CreatePole()
    {
        int i, num, angle;
        GameObject prefab = null;
        Vector3 pos = Vector3.zero;
        pos.y = 1.5f;
        for (i = 0; i < 9; i++)
        {
            pos.x = -20f + 10 * (i % 5) + 5f * (i / 5);
            if (i == 5) pos.x -= 3f;
            if (i == 8) pos.x += 3f;
            pos.z = -2f - 10 * (1 - i / 5);
            num = Random.Range(0, _vegetaPrefabs.Length);
            angle = Random.Range(0, 180);
            if (num < 4) pos.y = 1.2f; else pos.y = 1.5f;
            prefab = _vegetaPrefabs[num];
            GameObject food = Instantiate(prefab, pos, Quaternion.Euler(0, angle, 0));
            if (num > 3) food.transform.localScale = new Vector3(15f, 15f, 15f);
            else food.transform.localScale = new Vector3(20f, 20f, 20f);
            food.GetComponent<SimpleVegeta>().SetVegetaControl(_vegetaControl);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlay)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _timer = 1f;
                _gameTime++;
                if (_gameTime >= _gameSecond)
                {   //  end game
                    ViewEndPanel();
                }
                _vegetaUI.ViewTime(_gameTime);
            }
        }
    }
    public void HitToFood(GameObject food)
    {
        if (_attempts > 0)
        {
            _attempts--;
            _vegetaUI.ViewAttempts(_attempts);
            SimpleVegeta simpleFood = food.GetComponent<SimpleVegeta>();
            if (simpleFood != null)
            {
                if (simpleFood.FoodID == 6) //  слива
                {
                    GameManager.Instance.currentPlayer.inventory.AddItem(new ItemInfo(6, 1));
                    _countSlivas++;
                    _vegetaUI.ViewBirds(_countSlivas);
                }
                if (simpleFood.FoodID == 5) //  €блоко
                {
                    GameManager.Instance.currentPlayer.inventory.AddItem(new ItemInfo(5, 1));
                    _countAppels++;
                    _vegetaUI.ViewArrows(_countAppels);
                }
            }
            //Destroy(food, 0.2f);
            if (_attempts == 0) ViewEndPanel();
        }
        else
        {
            ViewEndPanel();
        }
    }

    private void ViewEndPanel()
    {
        _vegetaUI.ViewEndPanel(_gameTime, _countSlivas + _countAppels, _attempts);
    }
}
