using UnityEngine;

public class NextButtonLogic : MonoBehaviour
{
    public BookPageManager manager;

    void OnMouseDown()
    {
        if (manager != null)
            manager.ShowPage2();
    }
}
