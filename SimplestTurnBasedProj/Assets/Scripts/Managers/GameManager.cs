using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameStateEnum
    {
        SkillSelectMenu,
        PlayerTurn,
        EnemyTurn,
        GameOver
    }
    public static GameStateEnum GameState;

    [SerializeField] GameObject skillSelectMenu, hud;
    

    [Header("Skill Management Related")]

    public List<SkillSO> allSkillSOs;
    [SerializeField] Character player;
    [SerializeField] Character enemy;

    public Character Player { get => player; }
    public Character Enemy { get => enemy; }

    #region ContextMenu

    [ContextMenu("Fill allSkillSOs list")]
    void GetReportsSO()
    {
        System.Object[] tempskillSOs = Resources.LoadAll("ScriptableObjects/Skills", typeof(SkillSO));

        allSkillSOs = new List<SkillSO>();

        foreach (SkillSO skillSO in tempskillSOs)
            allSkillSOs.Add(skillSO);
    }

    #endregion ContextMenu

    private void Awake()
    {
        GameState = GameStateEnum.SkillSelectMenu;
    }

    //called by a button in GameOverPanel
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region Subscribing to Events

    private void OnEnable()
    {
        EventManager.Instance.onPlayPressed += StartGame;
    }

    private void OnDisable()
    {
        EventManager.Instance.onPlayPressed -= StartGame;
    }

    void StartGame()
    {
        int randomTurn = UnityEngine.Random.Range(0, 2);

        if (randomTurn == 0)
            GameState = GameStateEnum.PlayerTurn;
        else
            GameState = GameStateEnum.EnemyTurn;

        //Time.timeScale = 1;
        StartCoroutine(WaitForFirstTurnRoutine());
    }

    IEnumerator WaitForFirstTurnRoutine()
    {
        yield return new WaitForSeconds(1);
        EventManager.Instance.GameStateTurnChanged();
    }

    #endregion Subscribing to Events
}
