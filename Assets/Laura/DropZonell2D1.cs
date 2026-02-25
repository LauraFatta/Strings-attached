using UnityEngine;
using System.Collections;

public class DropZonell2D1 : MonoBehaviour
{
    
    private Vector3 originalPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag("BlueClue") || other.CompareTag("RedClue"))
        {
            Draggable2D draggable = other.GetComponent<Draggable2D>();
            if (draggable == null) return;

       
        }
    }

   

}




