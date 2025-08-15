using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;

public class DolabActivator : MonoBehaviour
{
    [SerializeField] public GameObject Dolab;
    //[SerializeField] private Volume blurVolume;
    [SerializeField] public GameObject PuzzelBriefCase;
    [SerializeField] public GameObject puzzelRoot;
    [SerializeField] public GameObject policeStationCanvas;
    [SerializeField] public GameObject DolabCamera;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject bagButton;
    [SerializeField] private GameObject playDolabAnimationButton;
    [SerializeField] private Animator DolabAnimator;

    private Coroutine showBtnCoro;

    void OnEnable()
    {
        if (bagButton) bagButton.SetActive(false);
        if (playDolabAnimationButton) playDolabAnimationButton.SetActive(false);
    }

    public void OpenDolab()
    {
        policeStationCanvas.SetActive(false);
        Dolab.SetActive(true);
        //blurVolume.weight = 1f;
        DolabCamera.SetActive(true);
        playDolabAnimationButton.SetActive(true);
        bagButton.SetActive(false);
        closeButton.SetActive(true);

    }

    public void OpenDolabAnimation()
    {
        DolabAnimator.SetBool("open", true);


        if (showBtnCoro != null)
        {
            StopCoroutine(showBtnCoro);
            showBtnCoro = null;
        }

        showBtnCoro = StartCoroutine(ShowSwitchButtonAfterDelay(1.2f));
    }

    private IEnumerator ShowSwitchButtonAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (!isActiveAndEnabled) yield break;

        bagButton.SetActive(true);
        playDolabAnimationButton.SetActive(false);
        showBtnCoro = null;
    }

    public void CloseDolab()
    {

        if (showBtnCoro != null)
        {
            StopCoroutine(showBtnCoro);
            showBtnCoro = null;
        }

        DolabAnimator.SetBool("open", false);

        bagButton.SetActive(false);
        playDolabAnimationButton.SetActive(false);

        //blurVolume.weight = 0f;
        DolabCamera.SetActive(false);
        closeButton.SetActive(false);
        Dolab.SetActive(false);
        policeStationCanvas.SetActive(true);
        PuzzelBriefCase.SetActive(false);
        puzzelRoot.SetActive(false);


    }

    public void OpenBagCloseView()
    {
        playDolabAnimationButton.SetActive(false);
        PuzzelBriefCase.SetActive(true);

        DolabCamera.SetActive(true);
        closeButton.SetActive(true);
        Dolab.SetActive(false);

    }
    public void OpenLockCloseView()
    {
        puzzelRoot.SetActive(true);
        playDolabAnimationButton.SetActive(false);
        PuzzelBriefCase.SetActive(true);

        DolabCamera.SetActive(true);
        closeButton.SetActive(true);

    }

}
