using AllUtils;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class HUDSkill : MonoBehaviour
{
    [SerializeField] Id id;

    [SerializeField] SkillInfoDisplay skillInfoDisplay;

    [SerializeField] GameObject showEnemyUsedSkill;

    public void SetSkill(SkillSO selectedSkill)
    {
        skillInfoDisplay.skillSO = selectedSkill;
        skillInfoDisplay.skillNameTMP.text = skillInfoDisplay.skillSO.SkillName;
        skillInfoDisplay.damageTMP.text = "Damage: " + skillInfoDisplay.skillSO.Damage.ToString();
        skillInfoDisplay.manaCostTMP.text = "Mana Cost: " + skillInfoDisplay.skillSO.ManaCost.ToString();
        skillInfoDisplay.iconImg.sprite = skillInfoDisplay.skillSO.Icon;
        skillInfoDisplay.iconImg.color = skillInfoDisplay.skillSO.IconColor;
    }

    public void UseSkill()
    {
        if (id == Id.Enemy)
            StartCoroutine(EnemyUsedSkillAnimRoutine());

        EventManager.Instance.SkillUsed(skillInfoDisplay.skillSO);
    }

    IEnumerator EnemyUsedSkillAnimRoutine()
    {
        showEnemyUsedSkill.SetActive(true);
        yield return new WaitForSeconds(.2f);
        showEnemyUsedSkill.SetActive(false);
    }
}
