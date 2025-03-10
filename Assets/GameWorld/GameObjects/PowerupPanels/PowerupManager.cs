using UnityEngine;

namespace GameWorld.GameObjects.PowerupPanels
{
    public class PowerupManager : MonoBehaviour
    {
        public GameObject leftPanelPrefab;
        public GameObject rightPanelPrefab;
        public Transform leftAnchor;
        public Transform rightAnchor;
    
        public GameObject weaponPrefab;
        public WeaponController weapon;
        public Bullet bullet; 
        
        private int _perkType; // 0 = player multiplier, 1 = fire rate multiplier, 2 = damage multiplier
        private bool _setFireRate = false; //we use this to check if the random perk we genreated is fire rate
        private bool _setDamage = false; //used for checkinf if the perk is damager
        private bool _setPlayerMultiplier = false; //same as before but for player character multiplayer

        // The two panel instances we'll reuse
        private GameObject _leftPanel; //the left and right panel gameobjects
        private GameObject _rightPanel;
        private Powerups _leftPowerup; //we get the compomments from the gameobjects so we can generate the valuese
        private Powerups _rightPowerup;
    
        public static PowerupManager Instance;  //we use this to get the instance of the powerup manager so we can use it in other scripts
        //and so we dont have to create new instances of the powerup manager
    

    
        //poo stuff
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            // Create the two panel instances
            InitializePanels();
        }
    
        //pool stuff
        // Initialize the two panels
        private void InitializePanels()
        {
            // Create left panel
            _leftPanel = Instantiate(leftPanelPrefab);
            _leftPowerup = _leftPanel.GetComponent<Powerups>();
            _leftPanel.SetActive(false);
        
            // Create right panel
            _rightPanel = Instantiate(rightPanelPrefab);
            _rightPowerup = _rightPanel.GetComponent<Powerups>();
            _rightPanel.SetActive(false);
        }
    
        // Start is called before the first frame update
        void Start()
        {
            //we get the components of the weapon and bullet so we can change the values of the weapon and bullet
            weapon = weaponPrefab.GetComponent<WeaponController>();
            bullet = GameObject.FindWithTag("Bullet").GetComponent<Bullet>();
        }

        void Update()
        {
            
            
            //left is deactivted and right is active so set the perk from the left panel
            if(!_leftPowerup.isActiveAndEnabled && _rightPowerup.isActiveAndEnabled) //left is destroyed 
            {
                if (_setFireRate) //we check if the perk is fire rate
                    weapon.SetFireRate(_leftPowerup.FireRateMultiplier);
                else if (_setDamage) //or ikf its damage
                    bullet.Damage = _leftPowerup.DamageMultiplier;
                else if (_setPlayerMultiplier) //or if its player multiplier obviouisly doesn't work. impålement it 
                    return;
            } else if (_leftPowerup.isActiveAndEnabled && !_rightPowerup.isActiveAndEnabled) //right is destroyed
            {
                if (_setFireRate)
                    weapon.SetFireRate(_rightPowerup.FireRateMultiplier);
                else if (_setDamage)
                    bullet.Damage = _rightPowerup.DamageMultiplier;
                else if (_setPlayerMultiplier)
                    return;
            }
        }

        // Activate and set up the panels
        public void SpawnPowerup()
        {
            if (leftAnchor == null || rightAnchor == null)
            {
                Debug.LogError("Anchor positions not assigned!");
                return;
            }
        
            // Assign random perks to panels
            AssignRandomPerk(_leftPowerup);
            AssignRandomPerk(_rightPowerup);
            AssignPerk(_leftPowerup);
            AssignPerk(_rightPowerup);
        
            // Set position
            _leftPanel.transform.position = leftAnchor.position;
            _rightPanel.transform.position = rightAnchor.position;
        
            // Set target position (typically player position or end point)
            Vector3 targetPosition = new Vector3(0, 0, 0); // Replace with actual target position
            _leftPowerup.SetTargetPosition(targetPosition);
            _rightPowerup.SetTargetPosition(targetPosition);
        
        
            // Activate the panels
            _leftPanel.SetActive(true);
            _rightPanel.SetActive(true);
        }

        //here we  asssign the values we generated from the previous method and assignm them to the panels 
        private void AssignPerk(Powerups panel)
        {
            if (_setFireRate)
            {
                panel.AssignGoodColor(!(panel.FireRateMultiplier <= 1));
                panel.SetPerkText(); //this is where we updatre the text
            } else if (_setDamage)
            {
                bullet.Damage = panel.DamageMultiplier;
                panel.AssignGoodColor(!(panel.DamageMultiplier <= 1));
            }
        }

        // We assign random perks to the panels
        private void AssignRandomPerk(Powerups panel)
        {
            _perkType = Random.Range(0, 100); 
            // panel.Health = Random.Range(20, 100);
            panel.Health = 5;

            switch (_perkType)
            {
                case <= 25: // 25% chance
                    panel.FireRateMultiplier = 0;
                    panel.DamageMultiplier = 0;
                    panel.PlayerMultiplier = Random.Range(1, 4); // Player multiplier
                    _setPlayerMultiplier = true;
                    break;
                case > 25 and <= 50:
                    panel.FireRateMultiplier = Random.Range(0.3f, 6f); // Fire rate multiplier
                    panel.DamageMultiplier = 0;
                    panel.PlayerMultiplier = 0;
                    _setFireRate = true;
                    break;
                case > 50:
                    panel.FireRateMultiplier = 0;
                    panel.DamageMultiplier = Random.Range(0.2f, 4f); // Damage multiplier
                    panel.PlayerMultiplier = 0;
                    _setDamage = true;
                    break;
            }
        }
    
        // Method to deactivate a panel
        public void DeactivatePanel(GameObject panel)
        {
            panel.SetActive(false);
        }
    }
}