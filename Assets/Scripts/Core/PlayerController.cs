using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] propellers = null;
    [SerializeField]
    private float basePropellersSpeed = 1f;

    private bool isMoving;
    private MoveDirection direction;

    private enum MoveDirection{
        Left,
        Right,
        Up,
        Down,
        None
    }

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        direction = MoveDirection.None;
    }

    private void FixedUpdate()
    {
        Camera main = Camera.main;
        Vector3 newCamPos = this.transform.position;
        newCamPos.y = Constants.kCamYOffset; ;
        main.transform.position = newCamPos;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementState();
        Move();
        RotatePropellers();
    }

    private void CheckMovementState()
    {
        if (
            Input.GetKeyUp(KeyCode.LeftArrow)
            || Input.GetKeyUp(KeyCode.RightArrow)
            || Input.GetKeyUp(KeyCode.UpArrow)
            || Input.GetKeyUp(KeyCode.DownArrow)
        )
        {
            isMoving = false;
            direction = MoveDirection.None;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            isMoving = true;
            direction = MoveDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = true;
            direction = MoveDirection.Right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            isMoving = true;
            direction = MoveDirection.Up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            isMoving = true;
            direction = MoveDirection.Down;
        }
    }

    private void Move()
    {
        if (!LevelController.Instance.CanPlayerMove)
        {
            StopMovements();
            return;
        }
        if (!isMoving)
        {
            return;
        }
        Vector3 moveVector;
        switch (direction)
        {
            case MoveDirection.Left:
                moveVector = new Vector3(-1, 0);
                break;
            case MoveDirection.Right:
                moveVector = new Vector3(1, 0);
                break;
            case MoveDirection.Up:
                moveVector = new Vector3(0, 0, 1);
                break;
            case MoveDirection.Down:
                moveVector = new Vector3(0, 0, -1);
                break;
            default:
                moveVector = Vector3.zero;
                break;

        }
        GetComponent<Rigidbody>().AddForce(moveVector, ForceMode.Force);
    }

    private void RotatePropellers()
    {
        Vector3 curSpeed = GetComponent<Rigidbody>().velocity;
        if ((Mathf.Abs(curSpeed.x) < Constants.kNoVelocityEps.x) && (Mathf.Abs(curSpeed.y) < Constants.kNoVelocityEps.y) && (Mathf.Abs(curSpeed.z) < Constants.kNoVelocityEps.z))
        {
            return;
        }
        curSpeed.z = curSpeed.x + curSpeed.y + curSpeed.z;
        curSpeed.x = 0;
        curSpeed.y = 0;
        foreach(var cur in propellers)
        {
            cur.transform.Rotate(curSpeed * Time.deltaTime * basePropellersSpeed);
        }
    }

    public void StopMovements()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}
