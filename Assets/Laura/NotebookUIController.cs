using UnityEngine;

public class NotebookUIController : MonoBehaviour
{
    public GameObject notebookUI;

    public void ToggleNotebook()
    {
        if (notebookUI != null)
        {
            notebookUI.SetActive(!notebookUI.activeSelf);
        }
    }
}
