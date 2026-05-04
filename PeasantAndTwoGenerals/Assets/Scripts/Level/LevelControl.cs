using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private ReceptUI _receptUI;

    private int _vitaPercent = 50;
    private float _timer = 10f;
    private bool _isPlay = true;

//    private int _currentRecept = 0;

    // Start is called before the first frame update
    void Start()
    {
        _vitaPercent = GameManager.Instance.currentPlayer.totalGold;
        _levelUI.ViewVitaPercent(_vitaPercent);
        _receptUI.OnReceptSelect += ReceptSelect;
    }

    private void ReceptSelect(SimpleRecept recept)
    {
        _levelUI.CookRecept();
        for (int i = 0; i < recept.Sostav.Length; i++)
        {
            GameManager.Instance.currentPlayer.inventory.DecrementItem(recept.Sostav[i].ID, recept.Sostav[i].Count);
        }
        ChangePersent(recept.PercentVita);

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
                _timer = 10f;
                ChangePersent(-1);
            }
        }
    }

    public void ChangePersent(int zn)
    {
        if (zn > 0)
        {
            _vitaPercent += zn;
            if (_vitaPercent > 100) _vitaPercent = 100;
        }
        else
        {
            if (_vitaPercent > zn) _vitaPercent += zn;
            else
            {   //  проиграли !!!
                _vitaPercent = 0;
                _isPlay = false;
                _levelUI.ViewLossPanel();
            }
        }
        GameManager.Instance.currentPlayer.totalGold = _vitaPercent;
        _levelUI.ViewVitaPercent(_vitaPercent);
    }

    public void CookRecept()
    {
        _levelUI.HideReceptBook();
    }

    public void ViewHint(string txt)
    {
        _levelUI.ViewHint(txt);
    }

    public void HideHint()
    {
        _levelUI.HideHint();
    }
}
