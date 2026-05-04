using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ForestControl : MonoBehaviour, ISelectGame
{
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private string _title1;
    [SerializeField] private string _title2;
    [SerializeField] private string _nameScene1;
    [SerializeField] private string _nameScene2;
    public string[] TitleGames 
    {
        get
        {
            string[] res = { _title1, _title2 };
            return res;
        } 
    }

    public string[] NameScenes
    {
        get
        {
            string[] res = { _nameScene1, _nameScene2 };
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
            _levelUI.ViewSelectGamePanel(TitleGames, NameScenes);
        }
    }
}
