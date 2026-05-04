using UnityEngine;
//using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private Camera cameraFPS;
    [SerializeField] private Camera cameraTPS;

    private bool _isFPS = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraFPS.gameObject.SetActive(false);
        cameraTPS.gameObject.SetActive(true);
        IsoCameraFollower isoCamera = cameraTPS.gameObject.GetComponent<IsoCameraFollower>();
        if (isoCamera != null) isoCamera.SetUsed(true);
    }

    public void CameraSwitch()
    {
        //print($"_isFPS={_isFPS}");
        //_isFPS = !_isFPS;
        if (_isFPS)
        {
            _isFPS = false;
            cameraFPS.gameObject.SetActive(false);
            cameraTPS.gameObject.SetActive(true);
            IsoCameraFollower isoCamera = cameraTPS.gameObject.GetComponent<IsoCameraFollower>();
            if (isoCamera != null) isoCamera.SetUsed(true);
        }
        else
        {
            _isFPS = true;
            IsoCameraFollower isoCamera = cameraTPS.gameObject.GetComponent<IsoCameraFollower>();
            if (isoCamera != null) isoCamera.SetUsed(false);
            cameraTPS.gameObject.SetActive(false);
            cameraFPS.gameObject.SetActive(true);
        }
    }

    //public void OnCameraSwitch(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        if (_isFPS)
    //        {
    //            _isFPS = false;
    //            cameraFPS.gameObject.SetActive(false);
    //            cameraTPS.gameObject.SetActive(true);
    //            //cameraTPS.enabled = true;
    //            //cameraFPS.enabled = false;
    //        }
    //        else
    //        {
    //            _isFPS = true;
    //            cameraFPS.gameObject.SetActive(true);
    //            cameraTPS.gameObject.SetActive(false);
    //            //cameraTPS.enabled = false;
    //            //cameraFPS.enabled = true;
    //        }
    //        print($"_isFPS={_isFPS}");
    //    }
    //}
}
