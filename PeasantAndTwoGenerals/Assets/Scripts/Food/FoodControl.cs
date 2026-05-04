using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodControl : MonoBehaviour
{
    [SerializeField] private GameObject[] _foodPrefabs;
    [SerializeField] private FoodUI _foodUI;
    [SerializeField] private int _gameSecond = 120;

    private FoodControl _foodControl;
    private float _timer = 0;
    private int _countCucumbers = 0;
    private int _countBatats = 0;
    private int _gameTime = 0;
    private bool _isPlay = true;
    private int _attempts = 5;

    private void Awake()
    {
        _foodControl = GetComponent<FoodControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.currentPlayer.inventory.CheckItem(10, 1)) _attempts = 12;
        CreatePole();
        _foodUI.SetTotalTime(_gameSecond);
        _foodUI.ViewTime(_gameTime);
        _foodUI.ViewArrows(_countBatats);
        _foodUI.ViewBirds(_countCucumbers);
        _foodUI.ViewAttempts(_attempts);
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
                _foodUI.ViewTime(_gameTime);
            }
        }
    }

    private void CreatePole()
    {
        int i, num, angle;
        GameObject prefab = null;
        Vector3 pos = Vector3.zero;
        pos.y = 1.5f;
        for (i = 0; i < 20; i++)
        {
            pos.x = -10f + 5 * (i % 5);
            pos.z = 7f - 5 * (i / 5);
            num = Random.Range(0, _foodPrefabs.Length);
            angle = Random.Range(0, 180);
            if (num < 4) pos.y = 1.2f; else pos.y = 1.5f;
            prefab = _foodPrefabs[num];
            GameObject food = Instantiate(prefab, pos, Quaternion.Euler(0, angle, 0));
            if (num > 3) food.transform.localScale = new Vector3(15f, 15f, 15f);
            else food.transform.localScale = new Vector3(60f, 60f, 60f);
            food.GetComponent<SimpleFood>().SetFoodControl(_foodControl);
        }
    }

    public void HitToFood(GameObject food)
    {
        if (_attempts > 0)
        {
            _attempts--;
            _foodUI.ViewAttempts(_attempts);
            SimpleFood simpleFood = food.GetComponent<SimpleFood>();
            if (simpleFood != null)
            {
                if (simpleFood.FoodID == 7) //  огурец
                {
                    GameManager.Instance.currentPlayer.inventory.AddItem(new ItemInfo(7, 1));
                    _countCucumbers++;
                    _foodUI.ViewBirds(_countCucumbers);
                }
                if (simpleFood.FoodID == 1) //  картофель
                {
                    GameManager.Instance.currentPlayer.inventory.AddItem(new ItemInfo(1, 1));
                    _countBatats++;
                    _foodUI.ViewArrows(_countBatats);
                }
            }
            Destroy(food, 0.2f);
            if (_attempts == 0) ViewEndPanel();
        }
        else
        {
            ViewEndPanel();
        }            
    }

    private void ViewEndPanel()
    {
        _foodUI.ViewEndPanel(_gameTime, _countBatats + _countCucumbers, _attempts);
    }
}
