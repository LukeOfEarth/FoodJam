using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject reticle;
    public GameObject firePoint;
    public int ammo;
    public float speed;
    private PlayerState playerState;
    private void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    private void Update()
    {
       if(Input.GetMouseButtonDown(0))
       {
            if(playerState.hp > 1)
            {
                FireProjectile(bullet);
                playerState.hp--;
            }
       }
    }

    void FireProjectile(GameObject projectile)
    {
        if(playerState.activeLayer.GetComponent<HandleFood>().filledSlots == 0)
        {
            playerState.goDownLayer();
        }

        GameObject ammo = playerState.activeLayer.GetComponent<HandleFood>().useFoodAsAmmo();
        GameObject shot = Instantiate(projectile, this.transform.position, projectile.transform.rotation);
        playerState.dropFood(1, false);
        shot.GetComponent<SpriteRenderer>().sprite = ammo.GetComponent<SpriteRenderer>().sprite;
        shot.GetComponent<Rigidbody2D>().AddForce((firePoint.transform.right * speed * 100));
    }
}
