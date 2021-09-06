using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public bool rotateX;
    public float rotateXSpeed;
    public bool rotateY;
    public float rotateYSpeed;
    public bool rotateZ;
    public float rotateZSpeed;
    Vector3 rotation;

    public bool verticalBob;
    public float yMax;
    public float yMin;
    public float verticalSpeed;
    bool ascending;

    public bool horizontalBob;
    public float xMax;
    public float xMin;
    public float horizontalSpeed;
    bool movingLeft;

    private void Start()
    {
        ascending = false;
        movingLeft = false;
    }
    void FixedUpdate()
    {
        if (verticalBob)
        {
            MoveVertical();
        }

        if (horizontalBob)
        {
            MoveHorizontal();
        }

        rotation = new Vector3(0, 0, 0);

        if (rotateX)
        {
            rotation.x = rotateXSpeed;
        }

        if (rotateY)
        {
            rotation.y = rotateYSpeed;
        }

        if (rotateZ)
        {
            rotation.z = rotateZSpeed;
        }

        if(rotateX || rotateY || rotateZ)
        {
            Rotate(rotation);
        }
    }

    void MoveVertical()
    {
        if(ascending && transform.position.y > yMax)
        {
            ascending = false;
        } 
        else if(!ascending && transform.position.y < yMin) 
        {
            ascending = true;
        }

        if(ascending)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (verticalSpeed*Time.deltaTime), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (verticalSpeed * Time.deltaTime), transform.position.z);
        }
    }

    void MoveHorizontal()
    {
        if (movingLeft && transform.position.x < xMin)
        {
            movingLeft = false;
        }
        else if (!movingLeft && transform.position.x > xMax)
        {
            movingLeft = true;
        }

        if (movingLeft)
        {
            transform.position = new Vector3(transform.position.x - (horizontalSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + (horizontalSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
    void Rotate(Vector3 rotation)
    {
        transform.Rotate(rotation, 10);
    }
}
