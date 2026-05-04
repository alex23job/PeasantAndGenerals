using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _selectGamePanel1;
    [SerializeField] private GameObject _selectGamePanel2;
    [SerializeField] private Text _titleGame;
    [SerializeField] private Text _titleGame1;
    [SerializeField] private Text _titleGame2;

    [SerializeField] private Text _txtPercentVita;
    [SerializeField] private Image _imgPercentVita;

    [SerializeField] private GameObject _receptPanel;
    [SerializeField] private Button _bookBtn;

    [SerializeField] private GameObject _lossPanel;
    [SerializeField] private GameObject _winPanel;

    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private Text _hintText;

    private string[] _nameScenes = null;
    private float _timer = 5f;
    private bool _isCook = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCook)
        {
            if (_timer > 0) _timer -= Time.deltaTime;
            else
            {
                _isCook = false;
                _bookBtn.interactable = true;
            }
        }
    }

    public void CookRecept()
    {
        _bookBtn.interactable = false;
        _timer = 5f;
        _isCook = true;
        HideReceptBook();
    }

    public void ViewSelectGamePanel(string[] titles, string[] nameScenes)
    {
        _nameScenes = nameScenes;
        if (titles.Length == 1)
        {
            _selectGamePanel1.SetActive(true);
            _titleGame.text = titles[0];
        }
        if (titles.Length == 2)
        {
            _selectGamePanel2.SetActive(true);
            _titleGame1.text = titles[0];
            _titleGame2.text = titles[1];
        }
    }

    public void LoadMiniGame(int num)
    {
        if (_nameScenes != null && _nameScenes.Length > num)
        {   //  сохранить позицию героя и состояние игры LevelScene
            SceneManager.LoadScene(_nameScenes[num]);
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadInventory()
    {

    }

    public void LoadReceptBook()
    {
        _receptPanel.SetActive(true);
    }

    public void HideReceptBook()
    {
        _receptPanel.SetActive(false);
    }

    public void ViewVitaPercent(int prc)
    {
        _txtPercentVita.text = $"{prc}%";
        _imgPercentVita.fillAmount = (100f - (float)prc) / 100f;
    }

    public void ViewHint(string hintTxt)
    {
        _hintPanel.SetActive(true);
        _hintText.text = hintTxt;
    }

    public void HideHint()
    {
        _hintPanel.SetActive(false);
        _selectGamePanel1.SetActive(false);
        _selectGamePanel2.SetActive(false);
    }

    public void ViewLossPanel()
    {
        _lossPanel.SetActive(true);
    }
    public void ViewWinPanel()
    {
        _winPanel.SetActive(true);
    }
}
