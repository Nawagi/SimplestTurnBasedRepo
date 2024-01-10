using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUtils;

public class Character : MonoBehaviour
{
    public Id id;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private int currentMana = 500;
    [SerializeField] private float secondsBetweenAttackAndDamageEffect = .5f;
    [SerializeField] private float secondsToChangeTurn = 1f;
    [SerializeField] Animator anim;

    [SerializeField] private int maxSkills = 3;
    [SerializeField] private List<SkillSO> selectedSkills;

    GameManager gameManager;

    public List<SkillSO> SelectedSkills { get => selectedSkills; }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        selectedSkills = new List<SkillSO>();

        if (id == Id.Enemy)
        {
            List<SkillSO> tempSkillSOList = new List<SkillSO>();
            int randomSkill;

            for (int i = 0; i < gameManager.allSkillSOs.Count; i++)
                tempSkillSOList.Add(gameManager.allSkillSOs[i]);

            for (int i = 0; i < maxSkills; i++)
            {
                randomSkill = Random.Range(0, tempSkillSOList.Count);
                selectedSkills.Add(tempSkillSOList[randomSkill]);
                tempSkillSOList.RemoveAt(randomSkill);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    #region Subscribing to Events

    private void OnEnable()
    {
        EventManager.Instance.onSkillSelected += AddSkillToPlayerList;
        EventManager.Instance.onResetPlayerSelectedSkills += ResetPlayerSkillsList;
        EventManager.Instance.onSkillUsed += Attack;
        EventManager.Instance.onTakeDamage += TakeDamage;
    }

    private void OnDisable()
    {
        EventManager.Instance.onSkillSelected -= AddSkillToPlayerList;
        EventManager.Instance.onResetPlayerSelectedSkills -= ResetPlayerSkillsList;
        EventManager.Instance.onSkillUsed -= Attack;
        EventManager.Instance.onTakeDamage -= TakeDamage;
    }

    void AddSkillToPlayerList(int skillIndex)
    {
        if (id != Id.Player)
            return;

        selectedSkills.Add(gameManager.allSkillSOs[skillIndex]);
    }

    void ResetPlayerSkillsList()
    {
        if (id != Id.Player)
            return;

        selectedSkills.Clear();
    }

    private void Attack(SkillSO skillUsed)
    {
        if (id == Id.Player && GameManager.GameState == GameManager.GameStateEnum.PlayerTurn ||
            id == Id.Enemy && GameManager.GameState == GameManager.GameStateEnum.EnemyTurn)
        {
            currentMana -= skillUsed.ManaCost;
            EventManager.Instance.ManaChanged(id, currentMana);
            anim.SetTrigger("Attack");
            StartCoroutine(WaitToCallTakeDamageRoutine(skillUsed));
        }
    }

    IEnumerator WaitToCallTakeDamageRoutine(SkillSO skillUsed)
    {
        yield return new WaitForSeconds(secondsBetweenAttackAndDamageEffect);
        EventManager.Instance.TakeDamage(skillUsed);
    }

    private void TakeDamage(SkillSO skillUsed)
    {
        if (id == Id.Player && GameManager.GameState != GameManager.GameStateEnum.PlayerTurn ||
            id == Id.Enemy && GameManager.GameState != GameManager.GameStateEnum.EnemyTurn)
        {
            anim.SetTrigger("TakeDamage");
            currentHealth -= skillUsed.Damage;
            EventManager.Instance.HealthChanged(id, currentHealth);

            GameObject skillEffect = ObjectPool.Instance.GetPooledItem(skillUsed.EffectTag);
            if (skillEffect != null)
            {
                skillEffect.transform.position = transform.position;
                skillEffect.transform.rotation = transform.rotation;
                skillEffect.SetActive(true);
            }

            if (currentHealth <= 0)
            {
                GameManager.GameState = GameManager.GameStateEnum.GameOver;
                EventManager.Instance.GameOver(id != Id.Player);
            }
            else
                StartCoroutine(WaitToChangeTurnRoutine());
        }
    }

    IEnumerator WaitToChangeTurnRoutine()
    {
        yield return new WaitForSeconds(secondsToChangeTurn);

        if (GameManager.GameState == GameManager.GameStateEnum.PlayerTurn)
            GameManager.GameState = GameManager.GameStateEnum.EnemyTurn;
        else if (GameManager.GameState == GameManager.GameStateEnum.EnemyTurn)
            GameManager.GameState = GameManager.GameStateEnum.PlayerTurn;

        EventManager.Instance.GameStateTurnChanged();
    }

    #endregion Subscribing to Events
}
