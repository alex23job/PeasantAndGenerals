using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterControl : MonoBehaviour
{
    [SerializeField] private HunterUI _hunterUI;
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private GameObject[] _birds;
    [SerializeField] private SpawnBirds _spawnBirds;
    [SerializeField] private int _spawnDelay;
    [SerializeField] private int _gameSecond = 150;

    private List<GameObject> _forest = new List<GameObject>();
    private HunterControl _hunterControl;
    private float _timer = 0;
    private int _countBirds = 0;
    private int _countArrows = 20;
    private int _gameTime = 0;
    private bool _isPlay = true;

    private void Awake()
    {
        _hunterControl = GetComponent<HunterControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateForest();
        _hunterUI.SetTotalTime(_gameSecond);
        _hunterUI.ViewTime(_gameTime);
        _hunterUI.ViewArrows(_countArrows);
        _hunterUI.ViewBirds(_countBirds);
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
                    _hunterUI.ViewEndPanel(_gameTime, _countBirds, _countArrows);
                }
                _hunterUI.ViewTime(_gameTime);
                if ((_gameTime % _spawnDelay) == 0)
                {
                    CreateBird();
                }
            }
        }
    }

    private void CreateBird()
    {
        int num = Random.Range(0, 3);
        GameObject prefab;
        if (num == 0) prefab = _birds[1];else prefab = _birds[0];
        _spawnBirds.CreateBirds(prefab, _hunterControl);
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
            pos.z = 20f - 8 * (i % 6);
            num = Random.Range(0, 4);
            if (num == 0)
            {
                num = Random.Range(0, 4);
                prefab = _prefabs[4 + num];
            }
            else
            {
                num = Random.Range(0, 4);
                prefab = _prefabs[num];
            }
            GameObject go = Instantiate(prefab, pos, Quaternion.identity);
            go.transform.localScale = new Vector3(24f, 28f, 24f);
            _forest.Add(go);
        }

    }

    public bool HitToBird(int id)
    {
        if (_countArrows > 0)
        {
            _countArrows--;
            _hunterUI.ViewArrows(_countArrows);
            if (id == 0)
            {
                _countBirds++;
                GameManager.Instance.currentPlayer.inventory.AddItem(new ItemInfo(3, 1));
                _hunterUI.ViewBirds(_countBirds);
            }
            if (_countArrows == 0)
            {
                Invoke("ViewEndPanel", 2f);
            }
            return true;
        }
        else
        {
            ViewEndPanel();
        }
        return false;
    }

    private void ViewEndPanel()
    {
        _hunterUI.ViewEndPanel(_gameTime, _countBirds, _countArrows);
    }
}
