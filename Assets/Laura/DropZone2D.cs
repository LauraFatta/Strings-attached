using UnityEngine;
using System.Collections;

public class DropZone2D : MonoBehaviour
{
    [Tooltip("This should match the tag of the correct clue (e.g., BlueClue or RedClue).")]
    public string requiredTag;

    private Vector3 originalPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag("BlueClue") || other.CompareTag("RedClue"))
        {
            Draggable2D draggable = other.GetComponent<Draggable2D>();
            if (draggable == null) return;

            if (other.tag == requiredTag)
            {
                // Correct drop
                other.transform.position = transform.position;
                other.transform.SetParent(transform);
                Debug.Log("Correct drop: " + other.name);
            }
            else
            {
                // Incorrect drop — shake slot and reset clue
                Debug.Log("Wrong clue dropped: " + other.name);
                StartCoroutine(Shake());
                draggable.ResetPosition(); // Return clue to original position
            }
        }
    }

    private IEnumerator Shake()
    {
        float duration = 0.2f;
        float strength = 5f; // Degrees of rotation
        float time = 0;

        Quaternion originalRotation = transform.rotation;

        while (time < duration)
        {
            float zRotation = Mathf.Sin(time * 40f) * strength;
            transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = originalRotation;
    }

}




