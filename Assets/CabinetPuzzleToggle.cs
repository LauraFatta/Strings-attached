using UnityEngine;

public class CabinetPuzzleToggle : MonoBehaviour
{
    public GameObject cabinet;
    public GameObject puzzlePanel;

    public void ShowPuzzle()
    {
        cabinet.SetActive(false);       // إخفاء الدولاب
        puzzlePanel.SetActive(true);    // إظهار البازل
    }
}
