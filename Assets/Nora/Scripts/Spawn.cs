using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject itemPrefab; 
    public Transform spawnPoint; 

    public void SpawnItem()
    {
        if (itemPrefab != null && spawnPoint != null)
        {
            Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Spawn settings are incomplete!");
        }
    }
}