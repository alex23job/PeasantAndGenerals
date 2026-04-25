using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsControl : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private float _movementSpeed = 25f;
    [SerializeField] private float _rotationSpeed = 45f;
    [SerializeField] private float _stoppingDistance = 0.5f;

    private HunterControl _hunterControl;
    private List<Vector3> _path = new List<Vector3>();
    private int _curIndex = -1;
    private Vector3 _target;
    public int BirdID { get => _id; }

    private Transform _body;
    private Rigidbody _rb;
    private bool _isMove = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //_body = transform.GetChild(0);
        _body = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMove)
        {
            //          ,                             
            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            Vector2 tg = new Vector2(_target.x, _target.z);
            //if (Vector3.Distance(transform.position, target) < stoppingDistance)
            if (Vector3.Distance(pos, tg) < _stoppingDistance)
            {
                NextPoint();
            }
            else
            {
                LookAtWaypoint();

                MoveTowardsWaypoint();
            }
        }
    }

    private void LookAtWaypoint()
    {
        Vector3 dir = _target - transform.position; //dir.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        _body.transform.rotation = Quaternion.Slerp(_body.transform.rotation, lookRot, _rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsWaypoint()
    {
        //                                 
        Vector3 dir = _target - transform.position; //dir.y = 0f;
        //_rb.MovePosition(transform.position - dir.normalized * _movementSpeed * Time.deltaTime);
        _rb.MovePosition(transform.position + transform.forward * _movementSpeed * Time.deltaTime);
    }
    private void NextPoint()
    {
        if (_curIndex < _path.Count)
        {
            _target = _path[_curIndex];
            _curIndex++;
        }
        else
        {
            _curIndex = 0;
            _isMove = false;
            Destroy(gameObject);
        }
    }

    public void SetPath(List<Vector3> pt, HunterControl hc)
    {
        _path = pt;
        _hunterControl = hc;
        _target = pt[0];
        _curIndex = 0;
        _isMove = true;
    }

    private void OnMouseUp()
    {        
        if (_hunterControl != null)
        {
            if (_hunterControl.HitToBird(_id))
            {
                _rb.useGravity = true;
            }
        }
        Destroy(gameObject, 1.5f);
    }
}
