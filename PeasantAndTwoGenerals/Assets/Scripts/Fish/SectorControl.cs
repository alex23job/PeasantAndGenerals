using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorControl : MonoBehaviour
{
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _sphere;
    [SerializeField] private int _secondPlay = 15;

    private Animator _animCircle;
    private Animator _animSphere;
    private FishingControl _fishingControl = null;

    private float _timer = 1f;
    private int _countSecond = 0;
    private bool _isPlay = false;

    private void Awake()
    {
        _animCircle = _circle.GetComponent<Animator>();
        _animSphere = _sphere.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeAnimations();
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
                _timer = 1f;
                _countSecond++;
                ChangeAnimations();
                if (_countSecond >= _secondPlay)
                {
                    _isPlay = false;
                }
            }
        }
    }

    public void SetFishingControl(FishingControl fc)
    {
        _fishingControl = fc;
    }

    private void ChangeAnimations()
    {
        int mode = _countSecond % 3;
        switch(mode)
        {
            case 0:
                float x = Random.Range(-3f, -3f);
                float z = Random.Range(-2f, -2f);
                Vector3 pos = new Vector3(x, _circle.transform.position.y, z);
                _circle.transform.position = pos;
                pos.y = _sphere.transform.position.y;
                _sphere.transform.position = pos;

                _animCircle.SetBool("IsPlay", false);
                _animSphere.SetBool("IsPlay", false);
                break;
            case 1:
                _animCircle.SetBool("IsPlay", false);
                _animSphere.SetBool("IsPlay", true);
                break;
            case 2:
                _animCircle.SetBool("IsPlay", true);
                _animSphere.SetBool("IsPlay", false);
                break;
        }
    }

    public void SetPlay(bool value)
    {
        _isPlay = value;
        if (_isPlay)
        {
            _countSecond = 0;
        }
        else
        {
            _animCircle.SetBool("IsPlay", false);
            _animSphere.SetBool("IsPlay", false);
        }
    }

    private void OnMouseUp()
    {
        //print($"MouseUp to sector {gameObject.name}");
        if (_fishingControl != null)
        {
            _fishingControl.HitToSector(gameObject);
        }
    }
}
