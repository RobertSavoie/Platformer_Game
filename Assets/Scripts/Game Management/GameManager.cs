using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    // Visislbe In Editor
    public GameObject player;
    public GameObject youDiedScreen;
    public GameObject loadingScreen;
    public GameObject blackScreen;
    public GameObject deathLoadingScreen;
    public Slider[] sliders;

    // Not Visible In Editor
    [NonSerialized] public GameObject[] entrances;
    [NonSerialized] public GameObject[] exits;
    [NonSerialized] public GameObject[] bonfires;
    private string bonfire;
    private int climbingGloves;
    private int jumpBoots;
    private int dashCloak;
    private Player playerScript;

    // Start is called before the first frame update
    private void Start()
    {
        playerScript = player.GetComponent<Player>();
        bonfire = PlayerPrefs.GetString("BONFIRE");
        climbingGloves = PlayerPrefs.GetInt("CLIMBING_GLOVES");
        jumpBoots = PlayerPrefs.GetInt("JUMP_BOOTS");
        dashCloak = PlayerPrefs.GetInt("DASH_CLOAK");
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main Menu"))
        {
            SetGameObjects();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main Menu"))
        {
            SetGameObjects();
        }
    }

    public void LoadGame()
    {
        player.SetActive(true);
        playerScript = player.GetComponent<Player>();

        foreach (Slider slider in sliders)
        {
            slider.gameObject.SetActive(true);
        }

        SetWorldState();

        if (bonfire != string.Empty)
        {
            SceneManager.LoadScene(bonfire);
            ToggleLoadingScreen();
            Invoke(nameof(ToggleLoadingScreen), 2f);
        }
        else
        {
            SceneManager.LoadScene("Beginning");
            ToggleLoadingScreen();
            Invoke(nameof(ToggleLoadingScreen), 2f);
        }

    }

    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }

    public void SetGameObjects()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        entrances = GameObject.FindGameObjectsWithTag("Entrance");
        exits = GameObject.FindGameObjectsWithTag("Exit");
        bonfires = GameObject.FindGameObjectsWithTag("Bonfire");
    }

    public void ToggleYouDiedScreen()
    {
        if (youDiedScreen.activeSelf)
        {
            youDiedScreen.SetActive(false);
        }
        else
        {
            youDiedScreen.SetActive(true);
        }
    }

    public void ToggleDeathLoadingScreen()
    {
        if (deathLoadingScreen.activeSelf)
        {
            deathLoadingScreen.SetActive(false);
            playerScript.DisabledOff();
        }
        else
        {
            deathLoadingScreen.SetActive(true);
            playerScript.DisabledOn();
        }
    }

    public void ToggleLoadingScreen()
    {
        if (loadingScreen.activeSelf)
        {
            player.GetComponent<CapsuleCollider2D>().enabled = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 2;
            loadingScreen.SetActive(false);
            playerScript.DisabledOff();
        }
        else
        {
            playerScript.DisabledOn();
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<CapsuleCollider2D>().enabled = false;
            loadingScreen.SetActive(true);
        }
    }

    public void ToggleBlackScreen()
    {
        if (blackScreen.activeSelf)
        {
            blackScreen.SetActive(false);
        }
        else
        {
            playerScript.disabled = false;
            blackScreen.SetActive(true);
        }
    }

    public void SetWorldState()
    {
        SetUpgrades();
    }

    public void SetUpgrades()
    {
        if (climbingGloves == 1)
        {
            player.GetComponent<Player>().climbingGloves = true;
        }
    }
}
