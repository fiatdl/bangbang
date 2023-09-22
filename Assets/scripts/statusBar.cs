using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class statusBar : MonoBehaviour
{
    [SerializeField] public tank tankFocusing;
    [SerializeField] private TextMeshProUGUI hpDisPlay;
    [SerializeField] private TextMeshProUGUI eDisPlay;
    [SerializeField] private TextMeshProUGUI qDisPlay;
    [SerializeField] private TextMeshProUGUI spaceDisPlay;
    [SerializeField] private TextMeshProUGUI eDisPlayInfo;
    [SerializeField] private TextMeshProUGUI qDisPlayInfo;
    [SerializeField] private TextMeshProUGUI spaceDisPlayInfo;
    [SerializeField] private TextMeshProUGUI attackValue;
    [SerializeField] protected TextMeshProUGUI armorValue;
    [SerializeField] protected TextMeshProUGUI attackSpeedValue;
    [SerializeField] protected TextMeshProUGUI critValue;
    [SerializeField] protected TextMeshProUGUI countdownValue;
    [SerializeField] protected TextMeshProUGUI moveSpeedValue;
    [SerializeField] private Image hpBar;
    [SerializeField]private Image eCooldown;
    [SerializeField] private Image spaceCooldown;
    [SerializeField] private Image qCooldown;
    [SerializeField] private TextMeshProUGUI gold;
    private void Awake()
    {
        gold.text = tankSelectManagement.Gold.ToString() + " Gold";
        hpDisPlay.text = tankFocusing.GetCurrentHP().ToString() + " / " + tankFocusing.GetMaxHp().ToString();
        eCooldown.fillAmount = 1f - ((float)tankFocusing.GetESkillCurrentCoundown() / tankFocusing.GetESkillCoundown());
        qCooldown.fillAmount = 1f - ((float)tankFocusing.GetQSkillCurrentCoundown() / tankFocusing.GetQSkillCoundown());
        spaceCooldown.fillAmount = 1f - ((float)tankFocusing.GetSpaceSkillCurrentCoundown() / tankFocusing.GetSpaceSkillCoundown());
        eDisPlay.text = Math.Round((decimal)tankFocusing.GetESkillCurrentCoundown(), 0).ToString() + " s";
        qDisPlay.text = Math.Round((decimal)tankFocusing.GetQSkillCurrentCoundown(), 0).ToString() + " s";
        spaceDisPlay.text = Math.Round((decimal)tankFocusing.GetSpaceSkillCurrentCoundown(), 0).ToString() + " s";
        attackValue.text = Math.Round((decimal)tankFocusing.damage, 0).ToString();
        armorValue.text = Math.Round((decimal)tankFocusing.armor, 0).ToString();
        attackSpeedValue.text = Math.Round((decimal)tankFocusing.attackSpeed, 0).ToString();
        critValue.text = Math.Round((decimal)tankFocusing.critPercent, 0).ToString() + "%";
        moveSpeedValue.text = Math.Round((decimal)tankFocusing.moveSpeed, 0).ToString();
        countdownValue.text = tankFocusing.countdown.ToString();
        hpBar.fillAmount = tankFocusing.CurrentHp / tankFocusing.hP;
    }
    void Start()
    {
        eDisPlayInfo.text = tankFocusing.eInfo;
        qDisPlayInfo.text = tankFocusing.qInfo;
        spaceDisPlayInfo.text = tankFocusing.spaceInfo;
    }

    // Update is called once per frame
    void Update()
    {
        gold.text = tankSelectManagement.Gold.ToString()+" Gold";
        hpDisPlay.text = tankFocusing.GetCurrentHP().ToString()+" / "+tankFocusing.GetMaxHp().ToString();
        eCooldown.fillAmount=1f-((float)tankFocusing.GetESkillCurrentCoundown()/tankFocusing.GetESkillCoundown());
        qCooldown.fillAmount = 1f-((float)tankFocusing.GetQSkillCurrentCoundown() / tankFocusing.GetQSkillCoundown());
        spaceCooldown.fillAmount = 1f-((float)tankFocusing.GetSpaceSkillCurrentCoundown() / tankFocusing.GetSpaceSkillCoundown());
        eDisPlay.text= Math.Round((decimal)tankFocusing.GetESkillCurrentCoundown(), 0).ToString()+" s";
        qDisPlay.text = Math.Round((decimal)tankFocusing.GetQSkillCurrentCoundown(), 0).ToString()+" s";
        spaceDisPlay.text = Math.Round((decimal) tankFocusing.GetSpaceSkillCurrentCoundown(), 0).ToString()+" s";
        attackValue.text = Math.Round((decimal)tankFocusing.damage, 0).ToString();
        armorValue.text= Math.Round((decimal)tankFocusing.armor, 0).ToString();
        attackSpeedValue.text= Math.Round((decimal)tankFocusing.attackSpeed, 0).ToString();
        critValue.text= Math.Round((decimal)tankFocusing.critPercent, 0).ToString() +"%";
        moveSpeedValue.text= Math.Round((decimal)tankFocusing.moveSpeed, 0).ToString();
        countdownValue.text = tankFocusing.countdown.ToString();
        hpBar.fillAmount = tankFocusing.CurrentHp / tankFocusing.hP;

    }
}
