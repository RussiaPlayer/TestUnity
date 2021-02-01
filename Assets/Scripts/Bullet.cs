using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    public GameObject gun;

    public float maxDistance = 15.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player"))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            this.GetComponent<Rigidbody2D>().simulated = false;
            gun.GetComponent<GraplingGun>().midAir = false;
            player.GetComponent<Rope>().EnableRope();
        }
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(player.transform.position, this.transform.position) >= maxDistance)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            this.GetComponent<Rigidbody2D>().simulated = false;
            gun.GetComponent<GraplingGun>().midAir = false;
            gun.GetComponent<GraplingGun>().ProjectileReturn();
        }
    }
}
