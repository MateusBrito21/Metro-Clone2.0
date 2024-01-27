using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bullerSpeed;
    public Rigidbody2D theRB;
    
    public Vector2 moveDir;
    
    public GameObject impactEffect;
   
   public int damageAmount = 1;

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = moveDir * bullerSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
        }

        // Quaternion = fala para o unity que não a rotação. 
        if(impactEffect != null)
        {
            Instantiate(impactEffect,transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}
