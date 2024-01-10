using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AllUtils
{
    public enum Id
    {
        Player,
        Enemy,
    }

    [System.Serializable]
    public class SkillInfoDisplay
    {
        public SkillSO skillSO;
        public TextMeshProUGUI skillNameTMP;
        public TextMeshProUGUI damageTMP;
        public TextMeshProUGUI manaCostTMP;
        public Image iconImg;
    }

    [System.Serializable]
    public class CharacterInfoDisplay
    {
        public TextMeshProUGUI nameTMP;
        public TextMeshProUGUI healthTMP;
        public TextMeshProUGUI manaTMP;
    }
}