using UnityEngine;

public class BackToMainSprites : MonoBehaviour
{
    public GameObject[] spritesToShow;    // Sprite_A و Sprite_B
    public GameObject[] spritesToHide;    // Sprite_A_Target و زر Back

    void OnMouseDown()
    {
        foreach (GameObject obj in spritesToShow)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        foreach (GameObject obj in spritesToHide)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
}
