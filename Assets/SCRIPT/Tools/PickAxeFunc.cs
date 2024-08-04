using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxeFunc : MonoBehaviour
{
    private Tool tool;
    public float checkRadius = 1f;
    public int damageAmount = 10; 
    public Animator animator;
    void Start()
    {
        tool = GetComponent<Tool>();
    }
    

    
    void Update()
    {
        // Example usage
        if (tool != null && tool.IsActive())
        {
            if(Input.GetMouseButtonDown(0)){
                animator.SetTrigger("PickAxeHit");
            }
        }
        else
        {
            Debug.Log("PickAxe is not active");
        }
    }





    void PickAxeHit(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the collider belongs to an object tagged "Tree" and is not a trigger
            if (hitCollider.CompareTag("Rock") && !hitCollider.isTrigger)
            {
                RockMain rockhealth = hitCollider.GetComponentInParent<RockMain>();
                if (rockhealth != null)
                {
                    rockhealth.TakeDamage(damageAmount);
                }
            }
            // Check if the collider belongs to an object tagged "Tree" and is not a trigger
            if (hitCollider.CompareTag("Bush") && !hitCollider.isTrigger)
            {
                BushMain bushHealth = hitCollider.GetComponentInParent<BushMain>();
                if (bushHealth != null)
                {
                    bushHealth.TakeDamage(damageAmount);
                }
            }
        }
    }
}
