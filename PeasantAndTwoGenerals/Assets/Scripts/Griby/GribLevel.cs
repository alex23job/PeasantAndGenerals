using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GribLevel : MonoBehaviour
{
    [SerializeField] private GameObject[] _forestPrefabs;
    [SerializeField] private GameObject[] _gribPrefabs;
    [SerializeField] private GribUI _gribUI;
    [SerializeField] private int _gameSecond = 180;
    [SerializeField] private PlayerGrib _playerGrib;

    private GribLevel _gribLevel = null;
    private float _timer = 0;
    private int _countGribs = 0;
    private int _gameTime = 0;
    private bool _isPlay = true;
    private int _attempts = 25;
    private List<GameObject> _forest = new List<GameObject>();
    private List<GameObject> _gribs = new List<GameObject>();

    private void Awake()
    {
        _gribLevel = GetComponent<GribLevel>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bool isBascet = GameManager.Instance.currentPlayer.inventory.CheckItem(10, 1);
        if (isBascet)
        {
            _attempts = 12;
        }
        if (_playerGrib != null) _playerGrib.ViewHideBascet(isBascet);
        CreateForest();
        _gribUI.SetTotalTime(_gameSecond);
        _gribUI.ViewBirds(_countGribs);
        _gribUI.ViewArrows(_attempts);
        _gribUI.ViewTime(_gameTime);
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
                _gribUI.ViewTime(_gameTime);
            }
        }
    }

    private void CreateForest()
    {
        int i, num;
        Vector3 pos = Vector3.zero;
        pos.y = 1.165f;
        GameObject prefab;
        for (i = 0; i < 54; i++)
        {
            pos.x = -60f + 15 * (i / 6);
            pos.z = 30f - 12 * (i % 6);
            num = Random.Range(0, 4);
            if (num == 0)
            {
                num = Random.Range(1, 6);
                prefab = _forestPrefabs[num];
            }
            else
            {
                num = Random.Range(0, 4);
                prefab = _forestPrefabs[6 + num];
            }
            GameObject go = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0, 180), 0));
            go.transform.localScale = new Vector3(24f, 28f, 24f);
            _forest.Add(go);
        }
        for (i = 0; i < 54; i++)
        {
            pos.x = -80f + 3 * i;
            pos.z = 45f;
            prefab = _forestPrefabs[0];
            GameObject go = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0, 180), 0));
            go.transform.localScale = new Vector3(24f, 28f, 24f);
            _forest.Add(go);
            pos.z = -45f;
            go = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0, 180), 0));
            go.transform.localScale = new Vector3(24f, 28f, 24f);
            _forest.Add(go);
        }
        for (i = 0; i < 29; i++)
        {
            pos.x = -80f;
            pos.z = 42f - 3 * i;
            prefab = _forestPrefabs[0];
            GameObject go = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0, 180), 0));
            go.transform.localScale = new Vector3(24f, 28f, 24f);
            _forest.Add(go);
            pos.x = 80f;
            go = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0, 180), 0));
            go.transform.localScale = new Vector3(24f, 28f, 24f);
            _forest.Add(go);
        }
        for (i = 0; i < 40; i++)
        {
            float dop = Random.Range(-1f, 1f);
            pos.x = -52.5f + 15 * (i / 5) + dop;
            dop = Random.Range(-1f, 1f);
            pos.z = 24f - 12 * (i % 5) + dop;
            num = Random.Range(0, 5);
            prefab = _gribPrefabs[num];
            GameObject go = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0, 180), 0));
            go.transform.localScale = new Vector3(24f, 28f, 24f);
            _gribs.Add(go);
        }
    }

    public void HitToFood(GameObject food)
    {
        if (_attempts > 0)
        {
            _attempts--;
            _gribUI.ViewArrows(_attempts);
            GribControl gribControl = food.GetComponent<GribControl>();
            if (gribControl != null)
            {
                if (gribControl.GribID == 2) //  סתוהמבםûי
                {
                    GameManager.Instance.currentPlayer.inventory.AddItem(new ItemInfo(6, 1));
                    _countGribs++;
                    _gribUI.ViewBirds(_countGribs);
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
        _gribUI.ViewEndPanel(_gameTime, _countGribs, _attempts);
    }
    public void ViewHint(string txt)
    {
        _gribUI.ViewHint(txt);
    }

    public void HideHint()
    {
        _gribUI.HideHint();
    }
}
