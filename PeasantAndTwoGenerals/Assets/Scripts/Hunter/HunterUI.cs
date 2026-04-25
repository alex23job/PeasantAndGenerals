using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HunterUI : MonoBehaviour
{
    [SerializeField] private Text _time;
    [SerializeField] private Text _birds;
    [SerializeField] private Text _arrows;
    //[SerializeField] private Text _timeEnd;
    [SerializeField] private Text _birdsEnd;
    [SerializeField] private Text _arrowsEnd;

    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject _hintPanel;

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
        _birds.text = zn.ToString();
    }

    public void ViewArrows(int zn)
    {
        _arrows.text = zn.ToString();
    }

    public void ViewEndPanel(int tm, int birds, int arrows)
    {
        _endPanel.SetActive(true);
        _birdsEnd.text = $"Добыто птиц : {birds}";
        string s = "Кончились стрелы";
        if (tm >= _totalTime)
        {
            s = $"Время вышло.\nОсталось стрел : {arrows}";
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

    private void HideHint()
    {
        _hintPanel.SetActive(false);
    }
}
