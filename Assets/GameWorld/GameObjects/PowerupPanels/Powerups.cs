using GameWorld.GameObjects.PowerupPanels;
using TMPro;
using UnityEngine;
public class Powerups : MonoBehaviour
{
    // Panel properties
    private readonly float _speed = 5f;
    private readonly float _damage = 30f;
    private Vector3 _targetPosition;
    
    public Material blueMaterial;
    public Material redMaterial;

    public TextMeshPro perkText;
    

    // Method to set target position
    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }

    void Start() //set the text of the perk 
    {
        perkText.text = Health + "X";
    }

    void Update()
    { 
        // Move panels towards the target
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y, _targetPosition.z);
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);
        // Deactivate instead of destroying
        if (transform.position.z <= 2) //the panel gets deactivated and should also damage the player
        {
            PowerupManager.Instance.leftPanelPrefab.transform.position = new Vector3(0, 0, 0); 
            PowerupManager.Instance.rightPanelPrefab.transform.position = new Vector3(0, 0, 0);
            PowerupManager.Instance.DeactivatePanel(gameObject); //this basically removes it from the view 
        }
        if(perkText != null) //we call on the text in the update so it keeps updating with the correct value 
            SetPerkText();
    }

    public void AssignGoodColor(bool isGood) //we assign the color of the panel, if true then color is blue
    {
        if(isGood)
            gameObject.GetComponent<Renderer>().material = blueMaterial;
        else 
            gameObject.GetComponent<Renderer>().material = redMaterial;
    }
    public float Health { get; set; } //get the health of the panel and also set it. We generate this in PowerupManager
    public float FireRateMultiplier { get; set; } //same as before, but this is for the fire rate
    public float DamageMultiplier { get; set; } // This is for the damage multipler of the gun.
    public int PlayerMultiplier { get; set; }
    
    public void SetPerkText() //this is the function that needs to work 
    {
        if(FireRateMultiplier != 0)
            perkText.text = "FR\n" + FireRateMultiplier + "X";  
        else if(DamageMultiplier != 0)
            perkText.text = "D\n" + DamageMultiplier + "X";
        else
            perkText.text = "X";
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (Health > 0)
            {
                Health -= 2;
                if(perkText != null) //updaet when bullet is registered
                    perkText.text = Health + "";
            }
            else
                PowerupManager.Instance.DeactivatePanel(gameObject); // Deactivate instead of destroying
        }
    }
    
    // Reset properties when this object is reused
    private void OnEnable()
    {
        // Reset any properties that need to be initialized when object is reused
        Health = 0;
        FireRateMultiplier = 0;
        DamageMultiplier = 0;
    }
}