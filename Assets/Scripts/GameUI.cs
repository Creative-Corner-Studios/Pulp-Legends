using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    [SerializeField] private Color pulpPowerBarColor;
    private bool pauseBtn;
    private Text p1score;
    private Text p2score;

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
        p1score = GameObject.Find("P1Score").GetComponent<Text>();
        p2score = GameObject.Find("P2Score").GetComponent<Text>();

        UpdateScore(worldControl.P1Score, worldControl.P2Score);
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
        UpdatePlayerBars();
        UpdateScore(worldControl.P1Score, worldControl.P2Score);
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
            p1PulpPowerBar.maxValue = worldControl.Player1.PulpMax;
            p1PulpPowerBar.value = worldControl.Player1.PulpCurrent;
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
            p2PulpPowerBar.maxValue = worldControl.Player2.PulpMax;
            p2PulpPowerBar.value = worldControl.Player2.PulpCurrent;
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
            p1PulpPowerBar.maxValue = worldControl.Player1.PulpMax;
            p1PulpPowerBar.value = worldControl.Player1.PulpCurrent;
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
            p2PulpPowerBar.maxValue = worldControl.Player2.PulpMax;
            p2PulpPowerBar.value = worldControl.Player2.PulpCurrent;
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

    void UpdatePlayerBars()
    {
        if(worldControl.p1Active && worldControl.p2Active)
        {
            p1HealthBar.value = worldControl.Player1.Health;
            p2HealthBar.value = worldControl.Player2.Health;
            p1PulpPowerBar.value = worldControl.Player1.PulpCurrent;
            if(p1PulpPowerBar.value >= worldControl.Player1.PulpCost)
            {
                p1PulpPowerBar.gameObject.GetComponentInChildren<Image>().color = Color.blue;
            }
            else
            {
                p1PulpPowerBar.gameObject.GetComponentInChildren<Image>().color = pulpPowerBarColor;
            }
            p2PulpPowerBar.value = worldControl.Player2.PulpCurrent;
            if (p2PulpPowerBar.value >= worldControl.Player2.PulpCost)
            {
                p2PulpPowerBar.gameObject.GetComponentInChildren<Image>().color = Color.blue;
            }
            else
            {
                p2PulpPowerBar.gameObject.GetComponentInChildren<Image>().color = pulpPowerBarColor;
            }
        }
        else if (worldControl.p1Active)
        {
            p1HealthBar.value = worldControl.Player1.Health;
            p1PulpPowerBar.value = worldControl.Player1.PulpCurrent;
            if (p1PulpPowerBar.value >= worldControl.Player1.PulpCost)
            {
                p1PulpPowerBar.gameObject.GetComponentInChildren<Image>().color = Color.blue;
            }
            else
            {
                p1PulpPowerBar.gameObject.GetComponentInChildren<Image>().color = pulpPowerBarColor;
            }
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
            p2PulpPowerBar.value = worldControl.Player2.PulpCurrent;
            if (p2PulpPowerBar.value >= worldControl.Player2.PulpCost)
            {
                p2PulpPowerBar.gameObject.GetComponentInChildren<Image>().color = Color.blue;
            }
            else
            {
                p2PulpPowerBar.gameObject.GetComponentInChildren<Image>().color = pulpPowerBarColor;
            }
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
        PauseMenu.interactable = true;
        EventSystem.current.SetSelectedGameObject(GameObject.Find("Resume Btn"));
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        worldControl.GamePaused = false;
        PauseMenu.alpha = 0;
        PauseMenu.interactable = false;
        Time.timeScale = 1f;
    }

    public void UpdateControlDisplay()
    {
        switch (controlSelector.value)
        {
            case 0:
                controlImage.sprite = keyboardControlSprite;
                EventSystem.current.SetSelectedGameObject(controlSelector.gameObject);
                break;

            case 1:
                controlImage.sprite = p1ControlSprite;
                EventSystem.current.SetSelectedGameObject(controlSelector.gameObject);
                break;

            case 2:
                controlImage.sprite = p2ControlSprite;
                EventSystem.current.SetSelectedGameObject(controlSelector.gameObject);
                break;
        }
    }

    public void DisplayControls()
    {
        ControlsGroup.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(controlSelector.gameObject);
    }

    public void HideControls()
    {
        ControlsGroup.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Resume Btn"));
    }

    public void ReturnToMain()
    {
        Destroy(GameObject.Find("WorldController"));
        Time.timeScale = 1f;
        worldControl.currentScreen = WorldController.Screen.MAINMENU;
        Application.LoadLevel(0);
    }

    public void UpdateScore(int Score1, int Score2)
    {
        p1score.text = "Score: " + Score1;
        p2score.text = "Score: " + Score2;
    }
}
