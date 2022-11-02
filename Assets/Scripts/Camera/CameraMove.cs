using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 3.0f;

    private float _rotationY;
    private float _rotationX;

    [SerializeField]
    public Transform _target;

    [SerializeField]
    private float _distanceFromTarget = 8.0f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField]
    private float _smoothTime = 0.2f;

    [SerializeField]
    private Vector2 _rotationXMinMax = new Vector2(-40, 40);

    public bool isCursor;

    public Player player;

    double position;

    public Connection connection;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) SetCursor(true);
        if (Input.GetKeyUp(KeyCode.LeftAlt)) SetCursor(false);

        if (_target == null || isCursor) return;

        if (position == null) this.position = player.position;

        Debug.Log(position);
        Debug.Log(connection.listPlayers.Count);

        if (Input.GetKeyDown(KeyCode.UpArrow)) SetPositionTarget(1);
        if (Input.GetKeyDown(KeyCode.DownArrow)) SetPositionTarget(-1);

        /*if (Input.GetKeyDown(KeyCode.DownArrow) && position <= connection?.listPlayers?.Count)
        {
            _target = connection?.listPlayers[(int)this.position--]?.transform;
        }*/

        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationY += mouseX;
        _rotationX += mouseY;

        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;

        transform.position = _target.position - transform.forward * _distanceFromTarget;

        float delta = Input.mouseScrollDelta.y;
        if ((_distanceFromTarget - delta) > 2) _distanceFromTarget -= delta;
    }

    public void SetCursor(bool enable)
    {
        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;

        this.isCursor = enable;
    }

    public void SetPositionTarget(int n)
    {
        this.position += n;

        _target = connection.listPlayers[(int)this.position].transform;
    }
}