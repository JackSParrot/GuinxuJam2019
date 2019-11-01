using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 5;
    public float speed = 20f;
    public GameObject SpawnOnDeath;
    bool dead = false;

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") || other.CompareTag("map"))
        {
            if(!dead)
                Destroy(gameObject, 0.2f);
            if(other.CompareTag("enemy"))
            {
                other.gameObject.GetComponent<AlbertoController>().GetHit(damage);
            }
            if(!dead && SpawnOnDeath != null)
            {
                var go = Instantiate(SpawnOnDeath, transform.position, Quaternion.identity);
                Destroy(go, 2f);
            }
            dead = true;
        }
    }
}
