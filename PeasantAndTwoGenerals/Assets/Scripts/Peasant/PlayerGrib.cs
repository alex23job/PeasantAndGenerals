using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player;

public class PlayerGrib : MonoBehaviour, IPlayerInteract
{
    [SerializeField] private GribLevel _gribLevel;
    [SerializeField] private GameObject _bascet;
    [SerializeField] private GameObject[] _gribs;
    private GameObject _currentTarget;
    private ITaking _currentTaking;
    private int _currentTriggerItemID = -1;

    private bool _isBascet = false;
    private int _countGribViews = 0;

    // Start is called before the first frame update
    void Start()
    {
        //ViewHideBascet(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InventoryItem"))
        {
            _currentTaking = other.gameObject.GetComponent<ITaking>();
            if (_currentTaking != null)
            {
                _currentTarget = _currentTaking.TakingItem();
                _gribLevel.ViewHint($"Нажмите \'E\' чтобы взять {_currentTaking.TakingName} {_currentTaking.Description}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InventoryItem"))
        {
            _gribLevel.HideHint();
            _currentTaking = null;
            _currentTarget = null;
        }
    }
    public int GetCurrentTakingID
    {
        get
        {
            if (_currentTaking == null) return -1;
            else return _currentTaking.TakingID;
        }
    }

    public void ViewHideBascet(bool value)
    {
        _isBascet = value;
        _bascet.SetActive(value);
        if (_isBascet == true)
        {
            foreach(GameObject grib in _gribs)
            {
                grib.SetActive(false);
            }
        }
    }

    public void ClearItem()
    {
        if (_currentTaking != null && _currentTarget != null)
        {
            GameObject destrObject = _currentTarget;
            _gribLevel.HitToFood(destrObject);
            ViewBascetGrib(destrObject);
            _currentTarget.transform.parent = null;
            if (_currentTaking.TakingID == _currentTriggerItemID)
            {
                _currentTaking.HideItem();
                _currentTaking = null;
                _currentTarget = null;
            }
            _gribLevel.HideHint();
            Destroy(destrObject, 2f);
        }
    }

    private void ViewBascetGrib(GameObject grib)
    {
        string nm = grib.name;
        if (nm.IndexOf("FoxGrib") >= 0)
        {
            if ((_countGribViews & 1) == 0) { _countGribViews |= 1; _gribs[0].SetActive(true); }
            else if ((_countGribViews & 2) == 0) { _countGribViews |= 2; _gribs[1].SetActive(true); }
        }
        else if (nm.IndexOf("SyroGrib") >= 0)
        {
            if ((_countGribViews & 4) == 0) { _countGribViews |= 4; _gribs[2].SetActive(true); }
            else if ((_countGribViews & 8) == 0) { _countGribViews |= 8; _gribs[3].SetActive(true); }
        }
        else if (nm.IndexOf("GribBor") >= 0)
        {
            if ((_countGribViews & 16) == 0) { _countGribViews |= 16; _gribs[4].SetActive(true); }
            else if ((_countGribViews & 32) == 0) { _countGribViews |= 32; _gribs[5].SetActive(true); }
        }
        else if (nm.IndexOf("PoganGrib") >= 0)
        {
            if ((_countGribViews & 64) == 0) { _countGribViews |= 64; _gribs[6].SetActive(true); }
            else if ((_countGribViews & 128) == 0) { _countGribViews |= 128; _gribs[7].SetActive(true); }
        }
        else if (nm.IndexOf("GribMh") >= 0)
        {
            if ((_countGribViews & 256) == 0) { _countGribViews |= 256; _gribs[8].SetActive(true); }
            else if ((_countGribViews & 512) == 0) { _countGribViews |= 512; _gribs[9].SetActive(true); }
        }
    }
}
