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
    private bool pauseBtn;

    //Pause menu Attributes

    private WorldController worldControl;
    private bool setup = false;
    private bool twoPlayers = false;
    [SerializeField] private CanvasGroup PauseMenu;
    [SerializeField] private CanvasGroup ControlsGroup;
    [SerializeField] private Dropdown controlSelector;
    [SerializeField] private Image controlImage;
    [SerializeField] private Sprite keyboardControlSprite;
    [SerializeField] private Sprite p1ControlSprite;
    [SerializeField] private Sprite p2ControlSprite;
    // Use this for initialization
    void Start () {
        worldControl = GameObject.Find("WorldController").GetComponent<WorldController>();
        
	}
	
	// Update is called once per frame
	void Update () {
        getInput();
        if (pauseBtn && worldControl.GamePaused == false)
        {
            PauseGame();
        }
        else if(pauseBtn && worldControl.GamePaused)
        {
            ResumeGame();
        }
        pauseBtn = false;
        UpdatePlayerHealth();
	}

    void getInput()
    {
        pauseBtn = Input.GetButtonDown("Pause");
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

    public void PauseGame()
    {
        worldControl.GamePaused = true;
        PauseMenu.alpha = 1;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        worldControl.GamePaused = false;
        PauseMenu.alpha = 0;
        Time.timeScale = 1f;
    }

    public void UpdateControlDisplay()
    {
        switch (controlSelector.value)
        {
            case 0:
                controlImage.sprite = keyboardControlSprite;
                break;

            case 1:
                controlImage.sprite = p1ControlSprite;
                break;

            case 2:
                controlImage.sprite = p2ControlSprite;
                break;
        }
    }

    public void DisplayControls()
    {
        ControlsGroup.alpha = 1;
        PauseMenu.alpha = 0;
    }

    public void HideControls()
    {
        ControlsGroup.alpha = 0;
        PauseMenu.alpha = 1;
    }

    public void ReturnToMain()
    {
        Destroy(GameObject.Find("WorldController"));
        Time.timeScale = 1f;
        worldControl.currentScreen = WorldController.Screen.MAINMENU;
        Application.LoadLevel(0);
    }
}
