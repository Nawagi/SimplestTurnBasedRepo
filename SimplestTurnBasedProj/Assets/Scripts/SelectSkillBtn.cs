using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using AllUtils;

public class SelectSkillBtn : MonoBehaviour
{
    public static int SelectedCount;

    [SerializeField] SkillInfoDisplay skillInfoDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        if (skillInfoDisplay.skillSO != null)
        {
            skillInfoDisplay.skillNameTMP.text = skillInfoDisplay.skillSO.SkillName;
            skillInfoDisplay.damageTMP.text = "Damage: " + skillInfoDisplay.skillSO.Damage.ToString();
            skillInfoDisplay.manaCostTMP.text = "Mana Cost: " + skillInfoDisplay.skillSO.ManaCost.ToString();
            skillInfoDisplay.iconImg.sprite = skillInfoDisplay.skillSO.Icon;
        }
    }

    public void SelectSkill()
    {
        EventManager.Instance.SkillSelected(skillInfoDisplay.skillSO.Index);
        SelectedCount++;
        EventManager.Instance.SelectedSkillCountChanged();
    }
}
