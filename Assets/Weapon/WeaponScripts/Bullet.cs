using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float _bulletVelocity = 0.3f; //speed of bullet
    private float _damage = 10; //damage of bullet
    private int _zombiesKileld = 0; //number of zombies killed
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //move the bullet forward
        gameObject.transform.position += gameObject.transform.forward* _bulletVelocity;        
    }
    
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InvisibleWall"))
        {
            gameObject.SetActive(false);
            BulletOP.SharedInstance.SetBulletsInactive();
        }
    }

    public int ZombiesKileld
    {
        get => _zombiesKileld;
        set => _zombiesKileld = value;
    }
}
