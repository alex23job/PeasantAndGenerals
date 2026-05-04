using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameLoader : MonoBehaviour, ISelectGame
{
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private string _title1;
    [SerializeField] private string _nameScene1;
    public string[] TitleGames
    {
        get
        {
            string[] res = { _title1 };
            return res;
        }
    }

    public string[] NameScenes
    {
        get
        {
            string[] res = { _nameScene1 };
            return res;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_nameScene1 == "BotCreateScene")
            {
                if (CheckBoard())
                {
                    _levelUI.ViewSelectGamePanel(TitleGames, NameScenes);
                }
                else
                {
                    _levelUI.ViewHint("Соберите все доски и вёсла, чтобы построить лодку");
                }
            }
            else
            {
                _levelUI.ViewSelectGamePanel(TitleGames, NameScenes);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _levelUI.HideHint();
        }
    }

    private bool CheckBoard()
    {
        if (GameManager.Instance.currentPlayer.inventory.CheckItem(11, 2) && GameManager.Instance.currentPlayer.inventory.CheckItem(12, 6)) { return true; }
        return false;
    }
}
