using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    private float _speed = 5;
    private int _verticalDirection = 0;
    private int _horizontalDirection = 0;
    private float _horizontalDamp = 0;
    private float _verticalDamp = 0;

    void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SerialPortUnity>().HorizontalAxis += SetHorizontalMove;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SerialPortUnity>().VerticalAxis += SetVerticalMove;
    }
    void Update()
    {
        if (_horizontalDirection != 0)
            MoveHorizontal();
        if (_verticalDirection != 0)
            MoveVertical();
    }
    public void SetHorizontalMove(int moveAxis)
    {
        _horizontalDirection = Mathf.Abs(moveAxis) / moveAxis;
        _horizontalDamp = Mathf.Abs(moveAxis / 100);
    }

    public void SetVerticalMove(int moveAxis)
    {
        _verticalDirection = Mathf.Abs(moveAxis) / moveAxis * -1;
        _verticalDamp = Mathf.Abs(moveAxis / 100);
    }
    public void MoveHorizontal()
    {
        float currentSpeed = _speed * _horizontalDamp;
        transform.Translate(new Vector3(_horizontalDirection * currentSpeed, 0, 0) * Time.deltaTime);
    }

    public void MoveVertical()
    {
        float currentSpeed = _speed * _verticalDamp;
        transform.Translate(new Vector3(0, _verticalDirection * currentSpeed, 0) * Time.deltaTime);
    }
}
