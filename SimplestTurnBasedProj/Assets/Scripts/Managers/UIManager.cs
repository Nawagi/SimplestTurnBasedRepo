using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AllUtils;

public class UIManager : MonoBehaviour
{
    [Header("Characters Status")]
    [SerializeField] CharacterInfoDisplay playerInfo;
    [SerializeField] CharacterInfoDisplay enemyInfo;

    [Header("Skills")]
    [SerializeField] Button[] playerSkillsBtn;
    [SerializeField] Button[] enemySkillsBtn;
    [SerializeField] HUDSkill[] playerHUDSkills;
    [SerializeField] HUDSkill[] enemyHUDSkills;
    //[SerializeField] Color32 highlightColor;
    //[SerializeField] Color32 noHighlightColor;

    [Header("Game Over")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverTMP;
    private const string youLose = "Game Over";
    private const string youWin = "Victory!";

    GameManager gameManager;

    #region CoontextMenu

    [ContextMenu("Fill player and enemy HUDSkillList")]
    void FillHUDSkillsList()
    {
        playerHUDSkills = new HUDSkill[playerSkillsBtn.Length];
        enemyHUDSkills = new HUDSkill[enemySkillsBtn.Length];

        for (int i = 0; i < playerSkillsBtn.Length; i++)
        {
            playerHUDSkills[i] = playerSkillsBtn[i].GetComponent<HUDSkill>();
            enemyHUDSkills[i] = enemySkillsBtn[i].GetComponent<HUDSkill>();
        }
    }

    #endregion CoontextMenu

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemovePlayerSkillsHighlight()
    {
        for (int i = 0; i < playerSkillsBtn.Length; i++)
            playerSkillsBtn[i].interactable = false;
    }

    #region Subscribing to Events

    private void OnEnable()
    {
        EventManager.Instance.onPlayPressed += InitSkillSetup;
        EventManager.Instance.onGameStateTurnChanged += HighlightSkills;
        EventManager.Instance.onHealthChanged += UpdateHealthDisplay;
        EventManager.Instance.onManaChanged += UpdateManaDisplay;
        EventManager.Instance.onGameOver += GameOver;
    }

    private void OnDisable()
    {
        EventManager.Instance.onPlayPressed -= InitSkillSetup;
        EventManager.Instance.onGameStateTurnChanged -= HighlightSkills;
        EventManager.Instance.onHealthChanged -= UpdateHealthDisplay;
        EventManager.Instance.onManaChanged -= UpdateManaDisplay;
        EventManager.Instance.onGameOver -= GameOver;
    }

    private void InitSkillSetup()
    {
        for (int i = 0; i < playerSkillsBtn.Length; i++)
        {
            playerHUDSkills[i].SetSkill(gameManager.Player.SelectedSkills[i]);
            enemyHUDSkills[i].SetSkill(gameManager.Enemy.SelectedSkills[i]);
            playerSkillsBtn[i].interactable = false;
            enemySkillsBtn[i].interactable = false;
        }
    }

    void HighlightSkills()
    {
        if (GameManager.GameState == GameManager.GameStateEnum.PlayerTurn)
        {
            for (int i = 0; i < playerSkillsBtn.Length; i++)
            {
                playerSkillsBtn[i].interactable = true;
                enemySkillsBtn[i].interactable = false;
            }
        }
        else if (GameManager.GameState == GameManager.GameStateEnum.EnemyTurn)
        {
            for (int i = 0; i < enemySkillsBtn.Length; i++)
            {
                playerSkillsBtn[i].interactable = false;
                enemySkillsBtn[i].interactable = true;
            }

            StartCoroutine(WaitForEnemySkillRoutine());
        }
    }

    IEnumerator WaitForEnemySkillRoutine()
    {
        yield return new WaitForSeconds(1);
        int randomEnemySkill = Random.Range(0, enemyHUDSkills.Length);
        enemyHUDSkills[randomEnemySkill].UseSkill();
    }

    void UpdateHealthDisplay(Id characterId, int currentHealth)
    {
        if (characterId == Id.Player)
            playerInfo.healthTMP.text = "Health: " + currentHealth.ToString();
        else
            enemyInfo.healthTMP.text = "Health: " + currentHealth.ToString();
    }

    void UpdateManaDisplay(Id characterId, int currentMana)
    {
        if (characterId == Id.Player)
            playerInfo.manaTMP.text = "Mana: " + currentMana.ToString();
        else
            enemyInfo.manaTMP.text = "Mana: " + currentMana.ToString();
    }

    void GameOver(bool playerWin)
    {
        if (playerWin)
            gameOverTMP.text = youWin;
        else
            gameOverTMP.text = youLose;

        gameOverPanel.SetActive(true);
    }

    #endregion Subscribing to Events
}
