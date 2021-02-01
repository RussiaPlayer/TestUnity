using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingGun : MonoBehaviour
{
    public float offset;

    public GameObject player;
    public GameObject projectile;
    public GameObject shotPointGO;
    public Transform shotPoint;

    public bool inShoot = false;
    public bool midAir = false;

    [Header("Force")]
    public float shootForce = 5.0f;
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (Input.GetButtonDown("Fire1") && !inShoot)
        {
            midAir = true;
            shotPointGO.transform.DetachChildren();
            projectile.GetComponent<Rigidbody2D>().simulated = true;
            projectile.GetComponent<BoxCollider2D>().isTrigger = false;
            projectile.GetComponent<Rigidbody2D>().AddForce(shotPoint.right * shootForce, ForceMode2D.Impulse);
            inShoot = true;
        }
        if (Input.GetButtonDown("Fire1") && inShoot && !midAir)
        {
            player.GetComponent<Rope>().DisableRope();
            ProjectileReturn();
        }
    }

    public void ProjectileReturn()
    {
        projectile.transform.position = shotPoint.position;
        projectile.transform.rotation = shotPoint.rotation;
        projectile.transform.parent = shotPointGO.transform;
        inShoot = false;
    }
}
