using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public int atk;
    public int def;
    public int spd;
    public bool isDefending;
    public int currentHP;

    void Start(){
        //hp = PlayerData.Instance.GetMaxHP();
        //atk = PlayerData.Instance.GetAtk();
        //def = PlayerData.Instance.GetDef();
        //spd = PlayerData.Instance.GetSpd();
        //currentHP = PlayerData.Instance.GetCurrentHP();
        
    }




    public int GetHP(){return hp;}     public int GetCurrentHP(){return currentHP;}
    
    //public int PlayerAttack(Enemy enemy,bool isCharged){ 
    //    if(atk < (enemy.def / 2)){
    //        return 0;
    //    }
    //    else{
    //        int totalDamage = (atk - (enemy.def / 2));
    //            Debug.Log("Total Damage: " + totalDamage);
    //            
    //        if(isCharged == false){
    //            return totalDamage;
    //        }
    //        else{
    //            return totalDamage * 2;
    //        }
    //    }
    //   
    //}

    public void SetCurrentHP(int h){
        currentHP = h;
    }
    
}