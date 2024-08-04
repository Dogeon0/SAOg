using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class BarFill : MonoBehaviour
{
    public Player playerScript;
    public Image barFill;
    private int HP;
    private int currentHP;
    private int newHp;
    

    void Start()
    {
        UpdateHPbars();
    }

    void UpdateHPbars(){
        HP = playerScript.GetHP();
        currentHP = playerScript.GetCurrentHP();
        Debug.Log("player's currentHP: " + currentHP + " and maxHP: " + HP);
        float fillAmount = (float)currentHP / HP;
        fillAmount = Mathf.Clamp01(fillAmount);
        barFill.DOFillAmount(fillAmount,0.3f);
    }

    public void DoDamage(int newDamage)
    {
        if (newDamage <= 0) return;
        Debug.Log("deducting health from Player: " + newDamage);
        currentHP = playerScript.GetCurrentHP();
        playerScript.SetCurrentHP(currentHP - newDamage);
        UpdateHPbars();
    }


    void Update()
    {
        
    }
}
