using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNetControl : MonoBehaviour
{
    [SerializeField] private FishingControl _fishingControl;
    [SerializeField] private GameObject _net;
    [SerializeField] private GameObject _fish;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _distance = 0.5f;

    private Vector3 _startPos;
    private Vector3 _target;
    private bool _isFishing = false;
    private bool _isMoving = false;

    private void Awake()
    {
        _startPos = _net.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        _fish.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving)
        {
            Vector3 dir = _target - _net.transform.position;
            Vector2 horDir = new Vector2(dir.x, dir.z);
            if (horDir.magnitude < _distance)
            {
                if (dir.magnitude < _distance)
                {
                    _net.transform.position = _target;
                    if ((_target - _startPos).magnitude < 0.1f)
                    {   //  вытащили невод
                        _fishingControl.EndFishing();
                    }
                    else
                    {   //  сеть упала
                        if (_isFishing) ViewFish();
                        _target = _startPos;
                    }
                }
                else
                {   //  сеть летит вниз
                    _net.transform.position += dir.normalized * _speed * 10 * Time.deltaTime;
                }
                /*Vector3 tmpTarget = _target;
                if ((tmpTarget - _startPos).magnitude < 0.1f)
                {   //  тащим невод
                    _net.transform.position += dir.normalized * _speed * Time.deltaTime;
                }
                else
                {   //  летит в сектор
                    tmpTarget.y += 3f;
                    dir = tmpTarget - _net.transform.position;
                    _net.transform.position += dir.normalized * _speed * 2 * Time.deltaTime;
                }*/
            }
            else
            {
                Vector3 tmpTarget = _target;
                if ((tmpTarget - _startPos).magnitude < 0.1f)
                {   //  тащим невод
                    _net.transform.position += dir.normalized * _speed * Time.deltaTime;
                }
                else
                {   //  летит в сектор
                    tmpTarget.y += 3f;
                    dir = tmpTarget - _net.transform.position;
                    _net.transform.position += dir.normalized * _speed * 2 * Time.deltaTime;
                }
            }
        }
    }

    public void ViewFish()
    {
        _fish.SetActive(true);
    }

    public void Zabros(Vector3 tg, bool fish = false)
    {
        //print($"tg={tg} fish={fish}");
        _net.transform.position = _startPos;
        _target = tg;
        _fish.SetActive(false);
        _isFishing = fish;
        _isMoving = true;
    }
}
