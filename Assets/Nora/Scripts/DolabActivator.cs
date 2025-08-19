using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DolabActivator : MonoBehaviour
{

     [SerializeField] public GameObject AllButton;
    [SerializeField] public GameObject ButtonToHide;
    [SerializeField] private GameObject DrawerButton;
    [SerializeField] public GameObject Notebook;
    [SerializeField] public GameObject notebookContents;

    [SerializeField] public GameObject Dolab;


    [SerializeField] public GameObject ClosedView_BriefCase;

    [SerializeField] public GameObject PuzzelBriefCase;
    [SerializeField] public GameObject puzzelRoot;
    [SerializeField] public GameObject policeStationUI;
    [SerializeField] public GameObject DolabCamera;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject closeButton2;
    [SerializeField] private GameObject bagButton;
    [SerializeField] private GameObject playDolabAnimationButton;
    [SerializeField] private Animator DolabAnimator;

    [SerializeField] private AutoHidePanel intPanel;

    [Header("Door Animation")]
    [SerializeField] private string doorStateName = "open";
    [SerializeField] private float bagButtonDelay = 1.2f;

    private Coroutine showBtnCoro;
    private bool doorPlayedOnce = false;

    void OnEnable()
    {
        if (bagButton) bagButton.SetActive(false);
        if (playDolabAnimationButton) playDolabAnimationButton.SetActive(false);
    }

    public void OpenDolab()
    {
        if (policeStationUI) policeStationUI.SetActive(false);
        if (Dolab) Dolab.SetActive(true);
        if (DolabCamera) DolabCamera.SetActive(true);

        if (playDolabAnimationButton) playDolabAnimationButton.SetActive(true);
        if (bagButton) bagButton.SetActive(false);
        if (closeButton) closeButton.SetActive(true);
        if (AllButton) AllButton.SetActive(false);
        if (ButtonToHide) ButtonToHide.SetActive(false);

        ResetDoorToClosedActiveSafe();
        doorPlayedOnce = false;
        if (playDolabAnimationButton && playDolabAnimationButton.TryGetComponent(out Button btn))
            btn.interactable = true;
    }

    public void OpenDolabAnimation()
    {
        if (doorPlayedOnce) return;
        doorPlayedOnce = true;

        if (playDolabAnimationButton && playDolabAnimationButton.TryGetComponent(out Button btn))
            btn.interactable = false;

        if (DolabAnimator)
        {
            DolabAnimator.enabled = true;
            DolabAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            DolabAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            DolabAnimator.Play(doorStateName, 0, 0f);
        }

        if (showBtnCoro != null) { StopCoroutine(showBtnCoro); showBtnCoro = null; }
        showBtnCoro = StartCoroutine(ShowBagAfterDelay(bagButtonDelay));
}

    private IEnumerator ShowBagAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (!isActiveAndEnabled) yield break;

        if (playDolabAnimationButton) playDolabAnimationButton.SetActive(false);
        if (bagButton) bagButton.SetActive(true);
        showBtnCoro = null;
    }

    private void ResetDoorToClosedActiveSafe()
    {
        if (!DolabAnimator) return;

        var go = DolabAnimator.gameObject;
        bool wasActive = go.activeSelf;

        if (!wasActive) go.SetActive(true);
        DolabAnimator.enabled = true;
        DolabAnimator.Rebind();
        DolabAnimator.Update(0f);
        DolabAnimator.enabled = false;
        if (!wasActive) go.SetActive(false);
    }

    public void CloseDolab()
    {
        if (showBtnCoro != null) { StopCoroutine(showBtnCoro); showBtnCoro = null; }

        ResetDoorToClosedActiveSafe();

        if (bagButton) bagButton.SetActive(false);
        if (playDolabAnimationButton) playDolabAnimationButton.SetActive(false);

        if (DolabCamera) DolabCamera.SetActive(false);
        if (closeButton) closeButton.SetActive(false);
        if (Dolab) Dolab.SetActive(false);

        if (policeStationUI) policeStationUI.SetActive(true);
        if (PuzzelBriefCase) PuzzelBriefCase.SetActive(false);
        if (puzzelRoot) puzzelRoot.SetActive(false);
        if (Notebook) Notebook.SetActive(false);
        if (notebookContents) notebookContents.SetActive(false);
     if (AllButton) AllButton.SetActive(true);
        if (ButtonToHide) ButtonToHide.SetActive(true);
        if (DrawerButton) DrawerButton.SetActive(false);

        doorPlayedOnce = false;
        if (playDolabAnimationButton && playDolabAnimationButton.TryGetComponent(out Button btn))
            btn.interactable = true;
    }

    public void OpenBagCloseView()
    {
        if (playDolabAnimationButton) playDolabAnimationButton.SetActive(false);
        if (PuzzelBriefCase) PuzzelBriefCase.SetActive(true);
        if (ClosedView_BriefCase) ClosedView_BriefCase.SetActive(true);
        if (AllButton) AllButton.SetActive(true);
        if (ButtonToHide) ButtonToHide.SetActive(false);
        ResetDoorToClosedActiveSafe();
        if (DolabCamera) DolabCamera.SetActive(true);
        if (closeButton) closeButton.SetActive(true);
        if (Dolab) Dolab.SetActive(false);
     

        if (intPanel) intPanel.Restart();
    }

    public void OpenLockCloseView()
    {
        if (puzzelRoot) puzzelRoot.SetActive(true);
        if (playDolabAnimationButton) playDolabAnimationButton.SetActive(false);
        if (PuzzelBriefCase) PuzzelBriefCase.SetActive(true);

        if (DolabCamera) DolabCamera.SetActive(true);
        if (closeButton) closeButton.SetActive(true);
        if (AllButton) AllButton.SetActive(false);
        if (ButtonToHide) ButtonToHide.SetActive(false);
    }
}
