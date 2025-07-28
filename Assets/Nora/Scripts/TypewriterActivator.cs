using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TypewriterActivator : MonoBehaviour
{
    [SerializeField] public GameObject typewriter;
    [SerializeField] private GameObject Typewriterbackground;
    [SerializeField] private Volume blurVolume;

    [SerializeField] public GameObject typewriterCamera;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject AllButton;
    [SerializeField] private GameObject playTypewriterAnimationButton;
    [SerializeField] private Animator typewriterAnimator;


    public void OpenTypewriter()
    {

        typewriter.SetActive(true);
        blurVolume.weight = 1;
        typewriterCamera.SetActive(true);
        closeButton.SetActive(true);
        AllButton.SetActive(false);
        playTypewriterAnimationButton.SetActive(true);

    }
    public void PlayTypewriterAnimation()
    {
        typewriterAnimator.SetBool("play", true);
    }
    // public void CloseTypewriterAnimation()
    // {
    //     typewriterAnimator.SetBool("play", false);
    // }


    public void CloseTypewriter()
    {
        typewriter.SetActive(false);
        blurVolume.weight = 0;
        typewriterCamera.SetActive(false);
        closeButton.SetActive(false);
        AllButton.SetActive(true);
        playTypewriterAnimationButton.SetActive(false);
        typewriterAnimator.SetBool("play", false);

    }
}
