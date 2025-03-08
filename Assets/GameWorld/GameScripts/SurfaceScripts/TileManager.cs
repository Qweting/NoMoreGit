using UnityEngine;


public class TileManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0,0,10) * Time.deltaTime;
        
        if(transform.position.z <= -43)
        {
            gameObject.SetActive(false); //disable the tile
            
            gameObject.transform.position = new Vector3(0, 0, 181); //reset the tile position
            gameObject.SetActive(true);
        }
        
    }
}
