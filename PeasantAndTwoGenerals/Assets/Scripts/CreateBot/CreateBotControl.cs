using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateBotControl : MonoBehaviour
{
    [SerializeField] private GameObject _bot;
    [SerializeField] private GameObject _botSet;

    private Animator _anim;

    private void Awake()
    {
        _anim = _botSet.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateBot", 2f);
        Invoke("ViewBot", 4.4f);
    }

    private void CreateBot()
    {
        _anim.SetBool("IsCreate", true);
    }

    private void ViewBot()
    {
        _bot.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
