using System.Collections;
using UnityEngine;
using DG.Tweening;
public class TreeBase : MonoBehaviour
{
    public GameObject treePrefab; // Prefab of the tree to spawn
    private GameObject spawnedTree;
    private TreeSounds soundHandler;

    void Start()
    {
        soundHandler = GetComponent<TreeSounds>();
        if (soundHandler == null)
        {
            Debug.LogError("No SoundHandler found on the TreeBase.");
        }

        CheckAndSpawnTree();
    }

    void CheckAndSpawnTree()
    {
        if (spawnedTree == null)
        {
            SpawnTree();
        }
    }

    
    void SpawnTree()
    {
        if (treePrefab != null)
        {
            Vector3 spawnPosition = transform.position; // Offset by 2 units in the Y direction
            spawnedTree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
            spawnedTree.transform.SetParent(transform);
            int temp = Random.Range(0,4);
            if(temp < 2){
                TreeLog treeLog = spawnedTree.GetComponent<TreeLog>();
                treeLog.SetHealth(30);
            }else{
                TreeLog treeLog = spawnedTree.GetComponent<TreeLog>();
                treeLog.SetHealth(60);
            }
            spawnPosition = spawnPosition + new Vector3(0, temp, 0);
            spawnedTree.transform.DOMove(spawnPosition,0.4f);
        }
        else
        {
            Debug.LogError("Tree prefab is not assigned.");
        }
    }


    public void OnTreeDestroyed()
    {
        if (soundHandler != null)
        {
            soundHandler.Play("treeDown");
        }
        StartCoroutine(SpawnTreeAfterDelay(30f));
    }

    IEnumerator SpawnTreeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckAndSpawnTree();
    }
}
