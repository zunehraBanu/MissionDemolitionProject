using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; // Singleton

    [Header("Set in Inspector")]
    public Text uitLevel;          // Level display
    public Text uitShots;          // Shots display
    public Text uitButton;         // UIButton text
    public Vector3 castlePos;      // Where to spawn castles
    public GameObject[] castles;   // Castle prefabs
    public GameOverUI gameOverUI;  // Game Over UI panel

    [Header("Set Dynamically")]
    public int level = 0;
    public int levelMax = 0;
    public int shotsTaken = 0;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; // FollowCam mode

    void Start()
    {
        S = this;

        if (castles == null || castles.Length == 0)
        {
            Debug.LogError("No castles assigned in MissionDemolition! Assign them in the Inspector.");
            return;
        }

        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
{
    if (level >= castles.Length)
    {
        Debug.LogWarning("All levels completed!");
        ShowGameOver();
        return;
    }

    // Destroy all existing castles in the scene
    GameObject[] oldCastles = GameObject.FindGameObjectsWithTag("Castle");
    foreach (GameObject c in oldCastles) Destroy(c);

    // Destroy old projectiles
    GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
    foreach (GameObject p in gos) Destroy(p);

    // Instantiate new castle
    castle = Instantiate(castles[level]);
    castle.transform.position = castlePos;

    shotsTaken = 0;

    // Reset camera
    SwitchView("Show Both");

    // Reset ProjectileLine and Goal
    ProjectileLine.S?.Clear();
    Goal.goalMet = false;

    UpdateGUI();

    mode = GameMode.playing;
}

    void UpdateGUI()
    {
        if (uitLevel != null) uitLevel.text = $"Level: {Mathf.Min(level + 1, levelMax)} of {levelMax}";
        if (uitShots != null) uitShots.text = $"Shots Taken: {shotsTaken}";
    }

    void Update()
    {
        UpdateGUI();

        if (mode == GameMode.playing && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");

            if (level + 1 >= levelMax)
            {
                ShowGameOver();
            }
            else
            {
                Invoke("NextLevel", 2f);
            }
        }
    }

    void NextLevel()
    {
        level++;
        if (level < levelMax)
        {
            StartLevel();
        }
        else
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver();
            Time.timeScale = 0f; // pause game
        }
        else
        {
            Debug.LogWarning("GameOverUI not assigned!");
        }
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "") eView = uitButton?.text ?? "Show Slingshot";
        showing = eView;

        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                if (uitButton != null) uitButton.text = "Show Castle";
                break;

            case "Show Castle":
                FollowCam.POI = castle;
                if (uitButton != null) uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                if (uitButton != null) uitButton.text = "Show Slingshot";
                break;
        }
    }

    // Static method to increment shots
    public static void ShotFired()
    {
        if (S != null) S.shotsTaken++;
    }
}