using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeFunc : MonoBehaviour
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
                animator.SetTrigger("AxeHit");
            }
        }
        else
        {
            Debug.Log("Axe is not active");
        }
    }





    void AxeHit(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the collider belongs to an object tagged "Tree" and is not a trigger
            if (hitCollider.CompareTag("Tree") && !hitCollider.isTrigger)
            {
                TreeLog treeHealth = hitCollider.GetComponentInParent<TreeLog>();
                if (treeHealth != null)
                {
                    treeHealth.TakeDamage(damageAmount);
                }
            }

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
