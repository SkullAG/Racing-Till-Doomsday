using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class Walking : MonoBehaviour
{
    public float velocity = 12;

    [Range(0.5f, 2)]
    public float lookSensitivity = 1;
    public float lookSensitivityPercent { get { return lookSensitivity * 100; } set { lookSensitivity = value / 100; }  }


    public Camera cam;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _lookAction;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions.FindAction("Move");
        _lookAction = _playerInput.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = _moveAction.ReadValue<Vector2>().normalized;

        if(move.magnitude > 0)
        {
            //Debug.Log(move);

            _rb.MovePosition(_rb.position + transform.rotation * (new Vector3(move.x, 0, move.y) * velocity * Time.deltaTime));
        }

        Vector2 look = _lookAction.ReadValue<Vector2>();

        if (look.magnitude > 0)
        {
            //Debug.Log(move);

            transform.rotation *= Quaternion.Euler(0, look.x * lookSensitivity, 0);
            //cam.transform.rotation = Quaternion.Euler(Mathf.Clamp(cam.transform.rotation.eulerAngles.y + look.y, -90, +90), 0, 0);

            //cam.transform.rotation *= Quaternion.Euler(look.y, 0, 0);
        }
        
        //Debug.Log(Net)
    }

    /*public void Walk(InputAction.CallbackContext context)
    {
        Debug.Log(context.duration);
    }*/
}
