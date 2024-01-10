//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Object/Skill SO")]
public class SkillSO : ScriptableObject
{
    [SerializeField] private int index;
    [SerializeField] private string skillName;
    [SerializeField] private int damage;
    [SerializeField] private int manaCost;
    [SerializeField] private Sprite icon;
    [SerializeField] private Color32 iconColor;
    [SerializeField] private GameObject effect;
    [SerializeField] private string effectTag;

    public int Index { get => index; }
    public string SkillName { get => skillName; }
    public int Damage { get => damage; }
    public int ManaCost { get => manaCost; }
    public Sprite Icon { get => icon; }
    public Color32 IconColor { get => iconColor; }
    public string EffectTag { get => effectTag; }

    private void OnValidate()
    {
        if (effect != null)
            effectTag = effect.tag;
    }
}
