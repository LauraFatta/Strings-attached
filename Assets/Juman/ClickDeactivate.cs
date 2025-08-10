using UnityEngine;

public class ClickDeactivate : MonoBehaviour
{
    [Header("حط العناصر اللي تبغى تطفيها عند الضغط")]
    public GameObject[] targetsToDisable;

    [Header("خيارات")]
    public bool disableSelf = false;     // يطفي هذا السبرايت نفسه بعد الضغط
    public bool onlyOnce = true;         // يمنع التفعيل أكثر من مرة

    private bool _clicked = false;

    void OnMouseDown()
    {
        if (onlyOnce && _clicked) return;
        _clicked = true;

        if (targetsToDisable != null)
        {
            for (int i = 0; i < targetsToDisable.Length; i++)
            {
                if (targetsToDisable[i] != null)
                    targetsToDisable[i].SetActive(false);
            }
        }

        if (disableSelf)
            gameObject.SetActive(false);
    }
}
