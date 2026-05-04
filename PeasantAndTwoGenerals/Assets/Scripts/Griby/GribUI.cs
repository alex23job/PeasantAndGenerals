using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GribUI : MonoBehaviour
{
    [SerializeField] private Text _time;
    [SerializeField] private Text _gribs;
    [SerializeField] private Text _arrows;
    [SerializeField] private Text _gribsEnd;
    [SerializeField] private Text _arrowsEnd;

    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private Text _txtHint;

    private int _totalTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("HideHint", 10f);
    }

    public void SetTotalTime(int tm)
    {
        _totalTime = tm;
    }

    public void ViewTime(int currentTime)
    {
        int zn = _totalTime - currentTime;
        _time.text = $"{zn / 60:00}:{zn % 60:00}";
    }

    public void ViewBirds(int zn)
    {
        _gribs.text = zn.ToString();
    }

    public void ViewArrows(int zn)
    {
        _arrows.text = zn.ToString();
    }

    public void ViewEndPanel(int tm, int birds, int arrows)
    {
        _endPanel.SetActive(true);
        _gribsEnd.text = $"Добыто грибов : {birds}";
        string s = "Кончились попытки";
        if (tm >= _totalTime)
        {
            s = $"Время вышло.\nОсталось попыток : {arrows}";
        }
        _arrowsEnd.text = $"{s}";
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ViewHint(string hint)
    {
        _hintPanel.SetActive(true);
        _txtHint.text = hint;
        Invoke("HideHint", 10f);
    }

    public void HideHint()
    {
        _hintPanel.SetActive(false);
    }
}
