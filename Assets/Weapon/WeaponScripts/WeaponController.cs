using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    private float _fireRate = 2f; // rate of fire in bullets per second
    private float _nextFireTime = 0; //duration between shots

    
    
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= _nextFireTime)
        {
            Fire();
            _nextFireTime = Time.time + (1f / _fireRate);
        }
    }

    void Fire()
    {
        GameObject bullet = BulletOP.SharedInstance.GetPoolObject();
        if (bullet != null)
        { 
            //if all bullets are active, expand the pool
            if(BulletOP.SharedInstance.getBulletsActive() >= BulletOP.SharedInstance.poolObjects.Count) 
                BulletOP.SharedInstance.ExpandPool(5);
            
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            BulletOP.SharedInstance.SetBulletsActive();
        }
        
    }
    
    //fireate getter and setter
    public float FireRate //is this valid? lmao
    {
        get => _fireRate ;
        set => _fireRate = value;
    }
    
    public void SetFireRate(float fireRate)
    {
        _fireRate = fireRate;
    }
    
}
