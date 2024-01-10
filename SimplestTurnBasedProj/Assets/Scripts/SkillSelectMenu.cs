//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectMenu : MonoBehaviour
{
    [SerializeField] Button[] skillBtns;
    [SerializeField] GameObject cantSelectImg;
    [SerializeField] Button playBtn;

    private void Awake()
    {
        SelectSkillBtn.SelectedCount = 0;
    }

    public void ResetSkills()
    {
        EventManager.Instance.ResetPlayerSelectedSkills();
        SelectSkillBtn.SelectedCount = 0;
        
        for (int i = 0; i < skillBtns.Length; i++)
        {
            skillBtns[i].enabled = true;
        }

        cantSelectImg.SetActive(false);
        playBtn.interactable = false;
    }

    public void Play()
    {
        EventManager.Instance.PlayPressed();
    }

    #region Subscribing to Events

    private void OnEnable()
    {
        EventManager.Instance.onSelectedSkillCountChanged += CheckIfThreeSkillsWereSelected;
    }

    private void OnDisable()
    {
        EventManager.Instance.onSelectedSkillCountChanged -= CheckIfThreeSkillsWereSelected;
    }

    void CheckIfThreeSkillsWereSelected()
    {
        if (SelectSkillBtn.SelectedCount == 3)
        {
            cantSelectImg.SetActive(true);
            playBtn.interactable = true;
        }
    }

    #endregion Subscribing to Events
}
