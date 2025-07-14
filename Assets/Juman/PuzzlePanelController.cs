using UnityEngine;

public class PuzzlePanelController : MonoBehaviour
{
    public GameObject puzzlePanel;

    public void ShowPanel()
    {
        puzzlePanel.SetActive(true);
    }

    public void HidePanel()
    {
        puzzlePanel.SetActive(false);
    }
}
