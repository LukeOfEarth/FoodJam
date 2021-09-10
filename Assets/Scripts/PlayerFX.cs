using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    public GameObject grappleWallFx;
    
    public void SpawnGrappleFX(Vector2 point)
    {
        Instantiate(grappleWallFx, point, grappleWallFx.transform.rotation);
    }
}
