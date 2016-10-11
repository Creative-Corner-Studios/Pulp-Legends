using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    //Attributes 
    [SerializeField]
    private Image p1Profile;
    [SerializeField]
    private Image p2Profile;
    [SerializeField] private Slider p1HealthBar;
    [SerializeField] private Slider p2HealthBar;
    [SerializeField] private Slider p1PulpPowerBar;
    [SerializeField] private Slider p2PulpPowerBar;
    [SerializeField] private CanvasGroup p1HUD;
    [SerializeField] private CanvasGroup p2HUD;
    [SerializeField] private Sprite samSpadeSprite;
    [SerializeField] private Sprite noraCarterSprite;
    [SerializeField] private Sprite samSpadeDeadSprite;
    [SerializeField] private Sprite noraCarterDeadSprite;

    private WorldController worldControl;
    private bool setup = false;
    private bool twoPlayers = false;
    // Use this for initialization
    void Start () {
        worldControl = GameObject.Find("WorldController").GetComponent<WorldController>();
        
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePlayerHealth();
	}

    public void SetupUI()
    {
        if (worldControl.p1Active && worldControl.p2Active)
        {
            twoPlayers = true;
            p1HealthBar.maxValue = worldControl.Player1.Health;
            p1HealthBar.value = worldControl.Player1.Health;
            switch (worldControl.Player1.Character)
            {
                case Player.CharacterType.SAMSPADE:
                    p1Profile.sprite = samSpadeSprite;
                    break;

                case Player.CharacterType.NORACARTER:
                    p1Profile.sprite = noraCarterSprite;
                    break;
            }

            p2HealthBar.maxValue = worldControl.Player2.Health;
            p2HealthBar.value = worldControl.Player2.Health;
            switch (worldControl.Player2.Character)
            {
                case Player.CharacterType.SAMSPADE:
                    p2Profile.sprite = samSpadeSprite;
                    break;

                case Player.CharacterType.NORACARTER:
                    p2Profile.sprite = noraCarterSprite;
                    break;
            }
        }
        else if (worldControl.p1Active)
        {
            p2HUD.alpha = 0;

            p1HealthBar.maxValue = worldControl.Player1.Health;
            p1HealthBar.value = worldControl.Player1.Health;

            switch (worldControl.Player1.Character)
            {
                case Player.CharacterType.SAMSPADE:
                    p1Profile.sprite = samSpadeSprite;
                    break;

                case Player.CharacterType.NORACARTER:
                    p1Profile.sprite = noraCarterSprite;
                    break;
            }
        }
        else if (worldControl.p2Active)
        {
            p1HUD.alpha = 0;

            p2HealthBar.maxValue = worldControl.Player2.Health;
            p2HealthBar.value = worldControl.Player2.Health;
            switch (worldControl.Player2.Character)
            {
                case Player.CharacterType.SAMSPADE:
                    p2Profile.sprite = samSpadeSprite;
                    break;

                case Player.CharacterType.NORACARTER:
                    p2Profile.sprite = noraCarterSprite;
                    break;
            }
        }
    }

    void UpdatePlayerHealth()
    {
        if(worldControl.p1Active && worldControl.p2Active)
        {
            p1HealthBar.value = worldControl.Player1.Health;
            p2HealthBar.value = worldControl.Player2.Health;
        }
        else if (worldControl.p1Active)
        {
            p1HealthBar.value = worldControl.Player1.Health;
            if (twoPlayers == true)
            {
                switch (worldControl.Player2.Character)
                {
                    case Player.CharacterType.SAMSPADE:
                        p2Profile.sprite = samSpadeDeadSprite;
                        p2HealthBar.value = 0;
                        break;

                    case Player.CharacterType.NORACARTER:
                        p2Profile.sprite = noraCarterDeadSprite;
                        p2HealthBar.value = 0;
                        break;
                }
            }
        }
        else if (worldControl.p2Active)
        {
            p2HealthBar.value = worldControl.Player2.Health;
            if (twoPlayers == true)
            {
                switch (worldControl.Player1.Character)
                {
                    case Player.CharacterType.SAMSPADE:
                        p1Profile.sprite = samSpadeDeadSprite;
                        p1HealthBar.value = 0;
                        break;

                    case Player.CharacterType.NORACARTER:
                        p1Profile.sprite = noraCarterDeadSprite;
                        p1HealthBar.value = 0;
                        break;
                }
            }
        }
    }
}
