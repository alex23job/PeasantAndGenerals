using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingControl : MonoBehaviour
{
    [SerializeField] private FishNetControl _fishNetControl;
    [SerializeField] private GameObject[] _sectors;
    [SerializeField] private int _changeSectorsDelay = 15;
    [SerializeField] private FishingUI _fishingUI;
    [SerializeField] private int _gameSecond = 300;

    private float _timer = 0;
    private int _countFish = 0;
    private int _gameTime = 0;
    private bool _isPlay = true;
    private bool _isFishing = false;
    private int _attempts = 20;
    private int _currentSector = 0;

    // Start is called before the first frame update
    void Start()
    {
        _fishingUI.SetTotalTime(_gameSecond);
        _fishingUI.ViewBirds(_countFish);
        _fishingUI.ViewArrows(_attempts);
        _fishingUI.ViewTime(_gameTime);
        ChangeFishingSector();
        FishingControl fs = GetComponent<FishingControl>();
        for (int i = 0; i < _sectors.Length; i++)
        {
            _sectors[i].GetComponent<SectorControl>().SetFishingControl(fs);
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
                if ((_gameTime % _changeSectorsDelay) == 0)
                {
                    ChangeFishingSector();
                }
                if (_gameTime >= _gameSecond)
                {   //  end game
                    ViewEndPanel();
                }
                _fishingUI.ViewTime(_gameTime);
            }
        }
    }

    private void ChangeFishingSector()
    {
        SectorControl sectorControl = _sectors[_currentSector].GetComponent<SectorControl>();
        if (sectorControl != null)
        {
            sectorControl.SetPlay(false);
        }
        _currentSector = Random.Range(0, _sectors.Length);
        sectorControl = _sectors[_currentSector].GetComponent<SectorControl>();
        if (sectorControl != null)
        {
            sectorControl.SetPlay(true);
        }
    }

    public void EndFishing()
    {
        _isFishing = false;
        ViewCountFish();
    }

    public void HitToSector(GameObject sector)
    {
        if (_isFishing) return;
        SectorControl sectorControl = sector.GetComponent<SectorControl>();
        if (sectorControl != null)
        {
            bool isFish = false;
            _isFishing = true;
            if (_attempts > 0)
            {
                _attempts--;
                _fishingUI.ViewArrows(_attempts);
                if (_attempts == 0) Invoke("ViewEndPanel", 3f);

                int takeFish = Random.Range(0, 10);
                if (sector.name == _sectors[_currentSector].name)
                {
                    if (takeFish < 8)
                    {
                        _countFish++;
                        isFish = true;
                        GameManager.Instance.currentPlayer.inventory.AddItem(new ItemInfo(8, 1));
                        //Invoke("ViewCountFish", 5f);
                    }
                }
                else
                {
                    if (takeFish < 3)
                    {
                        _countFish++;
                        isFish = true;
                        GameManager.Instance.currentPlayer.inventory.AddItem(new ItemInfo(8, 1));
                        //Invoke("ViewCountFish", 5f);
                    }
                }
                _fishNetControl.Zabros(sector.transform.position, isFish);
            }
            else
            {
                Invoke("ViewEndPanel", 3f);
            }
        }
    }

    private void ViewCountFish()
    {
        _fishingUI.ViewBirds(_countFish);
    }

    private void ViewEndPanel()
    {
        _fishingUI.ViewEndPanel(_gameTime, _countFish, _attempts);
    }
}
