using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private float _health = 29f; //health of the zombie
    private float _speed = 9f; //speed of the zombie
    private float _speedMultiplier = 1.5f; //multiplier for speed when player is close
    private Transform _player; 
    private GameObject _playerObject;
    private GameObject _score;
    private Animator animator;
    
    
    
    void Awake()
    {
        //get the player object
        _playerObject = GameObject.FindGameObjectWithTag("Player");
        _score = GameObject.FindGameObjectWithTag("Score");
        animator = GetComponent<Animator>();
        if (_playerObject!= null)
            _player = _playerObject.transform;
        else Debug.Log("Player not found.");
    }
    
    void Update()
    {
         //get the direction of the player
         Vector3 direction = new Vector3(_player.position.x, transform.position.y, _player.position.z);
         //move the zombie towards the player
         transform.position = Vector3.MoveTowards(transform.position, direction, Speed * Time.deltaTime);
        
        // if the player is close, increase the speed
         if (Vector3.Distance(transform.position, _player.position) < 30f)
             Speed = 10f;
         else
             Speed = 8f;



         //damage the player
         if (Vector3.Distance(transform.position, _player.position) < 11f)
         {
             animator.SetBool("isWalking", false);
             if (Vector3.Distance(transform.position, _player.position) < 1.0f)
             {
                 _playerObject.GetComponent<Player>().Health -= 10;
                 _score.GetComponent<Score>().UpdatePlayerHealth(_playerObject.GetComponent<Player>().Health);
                 gameObject.SetActive(false);
             }
         }


    }

    public float Health
    {
        get => _health;
        set => _health = value;
    }

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public float SpeedMultiplier
    {
        get => _speedMultiplier;
        set => _speedMultiplier = value;
    }
    
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }
    
    void Die()
    {
        _score.GetComponent<Score>().IncrementScore(1);
        gameObject.SetActive(false);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.Damage);
                bullet.gameObject.SetActive(false);
            }
        }
    }
    
    
}
