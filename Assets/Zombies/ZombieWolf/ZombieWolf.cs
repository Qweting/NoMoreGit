using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWolf : MonoBehaviour
{
    private float health = 20;
    private float damage = 10;

    private float speed = 15;
    
    private Animator anim;
    
    private Transform playerPos;
    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        //Find the player position
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        
        anim = GetComponent<Animator>();
        
        if(playerPos != null)
            direction = new Vector3(playerPos.transform.position.x, 0, playerPos.transform.position.z);
    }

    void Update()
    {
        var step = speed * Time.deltaTime;
        //Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, direction, step);

        if (transform.position.z <= 5) //if the boss reaches the player, the player takes damage
        {
            anim.SetBool("IsRunning", false); //play dead animation
            anim.SetBool("IsAttacking", true); //play dead animation
        }
        else 
            anim.SetBool("IsRunning", true); //play dead animation
            
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                other.gameObject.SetActive(false);
                if (health > 0)
                    TakeDamage(bullet.Damage);
                else if(health <= 0)
                    Die();
            }
        }
    }
    
    
}
