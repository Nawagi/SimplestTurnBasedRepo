//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System;
using AllUtils;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #region OnGameStateChanged Related

    /*public event Action onTutorialEnded;

    public void TutorialEnded()
    {
        onTutorialEnded?.Invoke();
    }*/

    public event Action onPlayPressed;

    public void PlayPressed()
    {
        onPlayPressed?.Invoke();
    }

    /*public event Action onGameStateChangedToPlayerTurn;

    public void GameStateChangedToPlayerTurn()
    {
        onGameStateChangedToPlayerTurn?.Invoke();
    }

    public event Action onGameStateChangedToEnemyTurn;

    public void GameStateChangedToEnemyTurn()
    {
        onGameStateChangedToEnemyTurn?.Invoke();
    }*/

    public event Action onGameStateTurnChanged;

    public void GameStateTurnChanged()
    {
        onGameStateTurnChanged?.Invoke();
    }

    public event Action<bool> onGameOver;

    public void GameOver(bool playerWin)
    {
        onGameOver?.Invoke(playerWin);
    }

    #endregion OnGameStateChanged Related


    #region SkillSelect Menu

    public event Action<int> onSkillSelected;

    public void SkillSelected(int skillIndex)
    {
        onSkillSelected?.Invoke(skillIndex);
    }

    public event Action onSelectedSkillCountChanged;

    public void SelectedSkillCountChanged()
    {
        onSelectedSkillCountChanged?.Invoke();
    }

    public event Action onResetPlayerSelectedSkills;

    public void ResetPlayerSelectedSkills()
    {
        onResetPlayerSelectedSkills?.Invoke();
    }

    #endregion SkillSelect Menu


    public event Action<SkillSO> onSkillUsed;

    public void SkillUsed(SkillSO skillUsed)
    {
        onSkillUsed?.Invoke(skillUsed);
    }

    public event Action<SkillSO> onTakeDamage;

    public void TakeDamage(SkillSO skillUsed)
    {
        onTakeDamage?.Invoke(skillUsed);
    }

    public event Action<Id, int> onHealthChanged;

    public void HealthChanged(Id characterId, int currentHealth)
    {
        onHealthChanged?.Invoke(characterId, currentHealth);
    }

    public event Action<Id, int> onManaChanged;

    public void ManaChanged(Id characterId, int currentMana)
    {
        onManaChanged?.Invoke(characterId, currentMana);
    }
}