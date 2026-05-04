using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private Image[] imgFones;

    [SerializeField] private GameObject _zastavka;
    [SerializeField] private Text _txtDebug;
    [SerializeField] private PlaySounds _playSounds;

    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Slider _sliderFone;
    [SerializeField] private Slider _sliderEffects;
    [SerializeField] private Toggle _togleFone;
    [SerializeField] private Toggle _togleEffects;

    // Start is called before the first frame update
    void Start()
    {
        if (_playSounds != null)
        {
            _playSounds.SetVolume(GameManager.Instance.currentPlayer.volumeFone);
            if (GameManager.Instance.currentPlayer.isSoundFone == false) _playSounds.PauseSounds();
        }
        if (GameManager.Instance.currentPlayer.isZastavkaView) CloseZastavka();
        else
        {
            if (_playSounds != null)
            {
                if (GameManager.Instance.currentPlayer.isSoundFone == false) _playSounds.PauseSounds();
                else _playSounds.PlayClip(1);
            }
        }
        Invoke("CloseZastavka", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CloseZastavka()
    {
        _zastavka.SetActive(false);
        GameManager.Instance.currentPlayer.isZastavkaView = true;
        if (_playSounds != null)
        {
            if (GameManager.Instance.currentPlayer.isSoundFone == false) _playSounds.PauseSounds();
            else _playSounds.PlayClip(0);
        }
    }

    public void ViewRecord()
    {

    }

    public void ViewSetting()
    {
        _settingsPanel.SetActive(true);
        _togleFone.isOn = GameManager.Instance.currentPlayer.isSoundFone;
        _togleEffects.isOn = GameManager.Instance.currentPlayer.isSoundEffects;
        _sliderFone.value = GameManager.Instance.currentPlayer.volumeFone / 100f;
        _sliderEffects.value = GameManager.Instance.currentPlayer.volumeEffects / 100f;
        _sliderFone.interactable = _togleFone.isOn;
        _sliderEffects.interactable = _togleEffects.isOn;
    }

    public void ChangeSoundFone()
    {
        GameManager.Instance.currentPlayer.isSoundEffects = _togleFone.isOn;
        _sliderFone.interactable = _togleFone.isOn;
        if (_togleFone.isOn) _playSounds.PlayClip(0);
        else _playSounds.PauseSounds();
    }

    public void ChangeSoundEffects()
    {
        GameManager.Instance.currentPlayer.isSoundEffects = _togleEffects.isOn;
        _sliderEffects.interactable = _togleEffects.isOn;
    }

    public void ChangeVolumeFone()
    {
        GameManager.Instance.currentPlayer.volumeFone = (int)(_sliderFone.value * 100);
        _playSounds.SetVolume(GameManager.Instance.currentPlayer.volumeFone);
    }

    public void ChangeVolumeEffects()
    {
        GameManager.Instance.currentPlayer.volumeEffects = (int)(_sliderEffects.value * 100);
    }

    public void CloseSettings()
    {
        GameManager.Instance.SaveGame();
        _settingsPanel.SetActive(false);
    }

    public void ViewDebug(string str)
    {
        _txtDebug.text = str;
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void QuitGame()
    {
        GameManager.Instance.SaveGame();
        Application.Quit();
    }
}
