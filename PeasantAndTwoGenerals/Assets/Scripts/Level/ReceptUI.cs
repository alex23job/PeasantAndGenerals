using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceptUI : MonoBehaviour
{
    // Делегат для уведомления о выборе рецепта
    public delegate void SelectReceptEventHandler(SimpleRecept recept);
    public event SelectReceptEventHandler OnReceptSelect;

    [SerializeField] private ReceptSet _receptSet;
    [SerializeField] private Toggle[] _toggles;
    [SerializeField] private Button _cookBtn;
    [SerializeField] private Button _prevBtn;
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Text _title;

    private int _currentRecept = 0;
    private SimpleRecept[] _recepts = null;

    private void Awake()
    {
        _recepts = _receptSet.GetRecepts();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateRecept();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCookButtonClick()
    {
        OnReceptSelect?.Invoke(_recepts[_currentRecept]);
    }

    public void OnPrevButtonClick()
    {
        int countRecepts = _recepts.Length;
        _currentRecept += countRecepts - 1;
        _currentRecept %= countRecepts;
        UpdateRecept();
    }

    public void OnNextButtonClick()
    {
        int countRecepts = _recepts.Length;
        _currentRecept++;
        _currentRecept %= countRecepts;
        UpdateRecept();
    }

    private void UpdateRecept()
    {
        Inventory inv = GameManager.Instance.currentPlayer.inventory;
        SimpleRecept sr = _recepts[_currentRecept];
        _title.text = sr.Name;
        bool isCook = true;
        bool yes;
        for (int i = 0; i < _toggles.Length - 1; i++)
        {
            if (i < sr.Sostav.Length)
            {
                _toggles[i].gameObject.SetActive(true);
                Text labelText = _toggles[i].GetComponentsInChildren<Text>(true)[0];
                labelText.text = sr.Sostav[i].Name;    
                yes = inv.CheckItem(sr.Sostav[i].ID, sr.Sostav[i].Count);
                _toggles[i].isOn = yes;
                if (yes == false) isCook = false; 
            }
            else
            {
                _toggles[i].gameObject.SetActive(false);
            }
        }
        if (sr.Mode == 1)
        {
            _toggles[3].gameObject.SetActive(true);
            yes = inv.CheckItem(9, 1);
            _toggles[3].isOn = yes;
            if (yes == false) isCook = false;
        }
        else
        {
            _toggles[3].gameObject.SetActive(false);
        }
        _cookBtn.interactable = isCook;
    }
}
