using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PeasantMovement : MonoBehaviour
{
    [SerializeField] private SwitchCamera _switchCamera;
    //[SerializeField] private float speedRot = 180f;
    [SerializeField, Range(0, 180f)] private float _rotationSmoothness;    // Коэффициент плавности поворота
    //                    
    [SerializeField] private float _moveSpeed = 5f;              //                  
    [SerializeField] private float _jumpForce = 10f;             //            
    private Rigidbody _rb;                       // Rigidbody
    private Animator _anim;
    private IPlayerInteract _playerInteract;
    private bool _isGrounded;
    private bool _isJump = false;
    private bool _isCarriasBox = false;
    private bool _isCarriasTrash = false;
    private Vector3 _movement = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private Vector3 _oldPos = Vector3.zero;

    private float _hor, _ver;
    private float _timer = 0.25f;
    private float _myVelocity = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _playerInteract = GetComponent<IPlayerInteract>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _hor = UnityEngine.Input.GetAxis("Horizontal");
        _ver = UnityEngine.Input.GetAxis("Vertical");
        /*Vector3 rot = transform.rotation.eulerAngles;
        rot.y += Time.deltaTime * speedRot;
        transform.rotation = Quaternion.Euler(rot);*/
        //MovePlaer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJump = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            int id = _playerInteract.GetCurrentTakingID;
            if (id != -1)
            {
                _anim.SetBool("IsWalk", false);
                _anim.SetBool("IsTake", true);
                Invoke("EndTake", 4f);
                GameManager.Instance.currentPlayer.inventory.AddItem(new ItemInfo(id, 1));

                //if (_isCarriasBox == false && _isCarriasTrash == false)
                //{
                //    if (id < 10)
                //    {
                //        _isCarriasBox = true;
                //        _anim.SetBool("IsWalkBox", true);
                //    }
                //    else
                //    {
                //        _isCarriasTrash = true;
                //        _anim.SetBool("IsTakeTrash", true);
                //        Invoke("AnimWalkTrash", 0.35f);
                //    }
                //    _playerInteract.CarriasItem();
                //}
                //else
                //{
                //    _playerInteract.ClearItem();
                //}
                _playerInteract.ClearItem();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _switchCamera.CameraSwitch();
        }
        if (_timer > 0) { _timer -= Time.deltaTime; }
        else
        {
            _timer = 0.25f;
            if (_isCarriasBox == false && _isCarriasTrash == false)
            {
                if (_myVelocity > 0.2f)
                {
                    _anim.SetBool("IsWalk", true);
                }
                else
                {
                    _anim.SetBool("IsWalk", false);
                }
            }
            _myVelocity = 0;
        }
    }

    private void EndTake()
    {
        _anim.SetBool("IsTake", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddForce(movement, ForceMode.Impulse);
        Move(_ver);
        Turn(_hor);
        Jump();
    }

    private void Move(float input)
    {
        //if (input > 0.99f) runStart += Time.deltaTime;
        //else runStart = 0;
        //if (runStart < 3f) input = (input > 0.95f) ? 0.9f : input;
        float mult = 1f;
        //if (runStart >= 3f) mult = 2f;
        //transform.Translate(Vector3.forward * input * moveSpeed * mult * Time.fixedDeltaTime);//Можно добавить Time.DeltaTime
        //_rb.AddForce(transform.forward * input * _moveSpeed * mult * Time.fixedDeltaTime);
        _rb.AddForce(transform.forward * input * _moveSpeed * mult);
        //anim.SetFloat("speed", Mathf.Abs(input));
        _myVelocity += Mathf.Abs(input);
        LimitVelocity(_moveSpeed);
    }
    void LimitVelocity(float maxVel)
    {
        // Ограничиваем скорость до заданного максимума
        if (_rb.velocity.magnitude > maxVel)
        {
            _rb.velocity = _rb.velocity.normalized * maxVel;
        }
    }

    private void Turn(float input)
    {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(0, input * _rotationSmoothness * Time.fixedDeltaTime, 0));
        //transform.Rotate(0, input * _rotationSmoothness * Time.deltaTime, 0);
    }

    //                         
    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    //                 ,                       
    //    if (IsGrounded())
    //    {
    //        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //    }
    //}
    private void Jump()
    {
        if (IsGrounded() && _isJump)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        _isJump = false;
    }

    //                             
    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.1f, ~0, QueryTriggerInteraction.Ignore);
    }
}
