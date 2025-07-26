using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class NotebookActivator : MonoBehaviour
{
    [SerializeField] public GameObject Notebook;
    [SerializeField] private Volume blurVolume;

    [SerializeField] public GameObject NotebookCamera;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject notebookUI;
    [SerializeField] private GameObject AllButton;

    public void OpenNotebook()
    {

        Notebook.SetActive(true);
        notebookUI.SetActive(true);
        blurVolume.weight = 1;
        NotebookCamera.SetActive(true);
        closeButton.SetActive(true);
        AllButton.SetActive(false);

    }

    public void CloseNotebook()
    {
        Notebook.SetActive(false);
        notebookUI.SetActive(false);
        blurVolume.weight = 0;
        NotebookCamera.SetActive(false);
        closeButton.SetActive(false);
        AllButton.SetActive(true);
    }
}
