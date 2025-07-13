using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TypewriterActivator : MonoBehaviour
{
    [SerializeField] public GameObject typewriter;
    [SerializeField] private Volume blurVolume;

    [SerializeField] public GameObject typewriterCamera;
    [SerializeField] private GameObject closeButton;

    public void OpenTypewriter()
    {

        typewriter.SetActive(true);
        blurVolume.weight = 1;
        typewriterCamera.SetActive(true);
        closeButton.SetActive(true);

    }

    public void CloseTypewriter()
    {
        typewriter.SetActive(false);
        blurVolume.weight = 0;
        typewriterCamera.SetActive(false);
        closeButton.SetActive(false);
    }
}
