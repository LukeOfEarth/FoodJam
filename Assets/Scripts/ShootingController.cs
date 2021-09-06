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
    Vector2 direction;

    private void Update()
    {
       if(Input.GetMouseButtonDown(0))
       {
            if(ammo > 0)
            {
                FireProjectile(bullet);
                ammo--;
            }
       }
    }

    void FireProjectile(GameObject projectile)
    {
        direction = new Vector2(reticle.transform.position.x, reticle.transform.position.y);
        GameObject shot = Instantiate(projectile, this.transform.position, projectile.transform.rotation);
        shot.GetComponent<Rigidbody2D>().AddForce((firePoint.transform.right * speed * 100));
    }
}
