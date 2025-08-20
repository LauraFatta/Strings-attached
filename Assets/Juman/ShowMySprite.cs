using UnityEngine;

public class ShowMySprite : MonoBehaviour
{
    public GameObject spriteToShow;          // السبرايت اللي راح يظهر
    public GameObject[] spritesToHide;       // السبرايتات اللي راح تختفي

    void OnMouseDown()
    {
        if (spriteToShow != null)
            spriteToShow.SetActive(true);

        foreach (GameObject obj in spritesToHide)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
}
