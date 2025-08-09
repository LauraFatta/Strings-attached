using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;

public class DolabActivator : MonoBehaviour
{
    [SerializeField] public GameObject Dolab;
    [SerializeField] private Volume blurVolume;
    [SerializeField] public GameObject bag;

    [SerializeField] public GameObject DolabCamera;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject switchButton;
    [SerializeField] private GameObject AllButton;
    [SerializeField] private GameObject playDolabAnimationButton;
    [SerializeField] private Animator DolabAnimator;

    public void OpenDolab()
    {
        Dolab.SetActive(true);
        blurVolume.weight = 1;
        playDolabAnimationButton.SetActive(true);
        DolabCamera.SetActive(true);
        closeButton.SetActive(true);
        AllButton.SetActive(false);
    }

    public void OpenDolabAnimation()
    {
        DolabAnimator.SetBool("open", true);
        StartCoroutine(ShowSwitchButtonAfterDelay(1.2f)); //delay before showing the switch button
    }

    private IEnumerator ShowSwitchButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        switchButton.SetActive(true);
    }

    public void CloseDolab()
    {
        Dolab.SetActive(false);
        blurVolume.weight = 0;
        DolabCamera.SetActive(false);
        closeButton.SetActive(false);
        AllButton.SetActive(true);
        playDolabAnimationButton.SetActive(false);
        DolabAnimator.SetBool("open", false);
        switchButton.SetActive(false);
        bag.SetActive(false);
    }
}
