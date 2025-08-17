// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.UI;
// using System.Collections;

// public class DolabActivator : MonoBehaviour
// {
//     [SerializeField] public GameObject Notebook;
//     [SerializeField] public GameObject notebookContents;

//     [SerializeField] public GameObject Dolab;
//     [SerializeField] public GameObject Button_Thread;
//     [SerializeField] public GameObject Button_Inventory;

//     //[SerializeField] private Volume blurVolume;
//     [SerializeField] public GameObject ClosedView_BriefCase;

//     [SerializeField] public GameObject PuzzelBriefCase;
//     [SerializeField] public GameObject puzzelRoot;
//     [SerializeField] public GameObject policeStationCanvas;
//     [SerializeField] public GameObject DolabCamera;
//     [SerializeField] private GameObject closeButton;
//     [SerializeField] private GameObject closeButton2;
//     [SerializeField] private GameObject bagButton;
//     [SerializeField] private GameObject playDolabAnimationButton;
//     [SerializeField] private Animator DolabAnimator;
//     [SerializeField] private AutoHidePanel intPanel;

//     private Coroutine showBtnCoro;

//     void OnEnable()
//     {
//         if (bagButton) bagButton.SetActive(false);
//         if (playDolabAnimationButton) playDolabAnimationButton.SetActive(false);
//     }

//     public void OpenDolab()
//     {
//         policeStationCanvas.SetActive(false);
//         Dolab.SetActive(true);
//         //blurVolume.weight = 1f;
//         DolabCamera.SetActive(true);
//         playDolabAnimationButton.SetActive(true);
//         bagButton.SetActive(false);
//         closeButton.SetActive(true);

//     }

//     public void OpenDolabAnimation()
//     {
//         DolabAnimator.SetBool("open", true);


//         if (showBtnCoro != null)
//         {
//             StopCoroutine(showBtnCoro);
//             showBtnCoro = null;
//         }

//         showBtnCoro = StartCoroutine(ShowSwitchButtonAfterDelay(1.2f));
//     }

//     private IEnumerator ShowSwitchButtonAfterDelay(float delay)
//     {
//         yield return new WaitForSecondsRealtime(delay);

//         if (!isActiveAndEnabled) yield break;

//         bagButton.SetActive(true);
//         playDolabAnimationButton.SetActive(false);
//         showBtnCoro = null;
//     }

//     public void CloseDolab()
//     {

//         if (showBtnCoro != null)
//         {
//             StopCoroutine(showBtnCoro);
//             showBtnCoro = null;
//         }

//         DolabAnimator.SetBool("open", false);

//         bagButton.SetActive(false);
//         playDolabAnimationButton.SetActive(false);

//         //blurVolume.weight = 0f;
//         DolabCamera.SetActive(false);
//         closeButton.SetActive(false);
//         Dolab.SetActive(false);
//         policeStationCanvas.SetActive(true);
//         PuzzelBriefCase.SetActive(false);
//         puzzelRoot.SetActive(false);
//         Button_Inventory.SetActive(false);
//         Button_Thread.SetActive(false);
//         Notebook.SetActive(false);
//         notebookContents.SetActive(false);
//     }

//     public void OpenBagCloseView()
//     {
//         playDolabAnimationButton.SetActive(false);
//         PuzzelBriefCase.SetActive(true);
//         ClosedView_BriefCase.SetActive(true);
//         Button_Thread.SetActive(true);
//         Button_Inventory.SetActive(true);

//         DolabCamera.SetActive(true);
//         closeButton.SetActive(true);
//         Dolab.SetActive(false);

//         if (intPanel) intPanel.Restart();
//     }

//     public void OpenNotebook2()
//     {
//         closeButton.SetActive(false);
//         closeButton2.SetActive(true);
//         Notebook.SetActive(true);
//         notebookContents.SetActive(true);
//     }
//     public void CloseDolab2()
// {
//     closeButton.SetActive(true);
//     closeButton2.SetActive(false);
//     Notebook.SetActive(false);
//     notebookContents.SetActive(false);
// }


//     public void OpenLockCloseView()
//     {
//         puzzelRoot.SetActive(true);
//         playDolabAnimationButton.SetActive(false);
//         PuzzelBriefCase.SetActive(true);

//         DolabCamera.SetActive(true);
//         closeButton.SetActive(true);
//          Button_Inventory.SetActive(false);
//         Button_Thread.SetActive(false);

//     }



// }

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DolabActivator : MonoBehaviour
{
    [SerializeField] public GameObject Notebook;
    [SerializeField] public GameObject notebookContents;

    [SerializeField] public GameObject Dolab;
    [SerializeField] public GameObject Button_Thread;
    [SerializeField] public GameObject Button_Inventory;

    [SerializeField] public GameObject ClosedView_BriefCase;

    [SerializeField] public GameObject PuzzelBriefCase;
    [SerializeField] public GameObject puzzelRoot;
    [SerializeField] public GameObject policeStationCanvas;
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
        if (policeStationCanvas) policeStationCanvas.SetActive(false);
        if (Dolab) Dolab.SetActive(true);
        if (DolabCamera) DolabCamera.SetActive(true);

        if (playDolabAnimationButton) playDolabAnimationButton.SetActive(true);
        if (bagButton) bagButton.SetActive(false);
        if (closeButton) closeButton.SetActive(true);

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

        if (policeStationCanvas) policeStationCanvas.SetActive(true);
        if (PuzzelBriefCase) PuzzelBriefCase.SetActive(false);
        if (puzzelRoot) puzzelRoot.SetActive(false);
        if (Button_Inventory) Button_Inventory.SetActive(false);
        if (Button_Thread) Button_Thread.SetActive(false);
        if (Notebook) Notebook.SetActive(false);
        if (notebookContents) notebookContents.SetActive(false);

        doorPlayedOnce = false;
        if (playDolabAnimationButton && playDolabAnimationButton.TryGetComponent(out Button btn))
            btn.interactable = true;
    }

    public void OpenBagCloseView()
    {
        if (playDolabAnimationButton) playDolabAnimationButton.SetActive(false);
        if (PuzzelBriefCase) PuzzelBriefCase.SetActive(true);
        if (ClosedView_BriefCase) ClosedView_BriefCase.SetActive(true);
        if (Button_Thread) Button_Thread.SetActive(true);
        if (Button_Inventory) Button_Inventory.SetActive(true);
        ResetDoorToClosedActiveSafe();
        if (DolabCamera) DolabCamera.SetActive(true);
        if (closeButton) closeButton.SetActive(true);
        if (Dolab) Dolab.SetActive(false);

        if (intPanel) intPanel.Restart();
    }

    public void OpenNotebook2()
    {
        if (closeButton) closeButton.SetActive(false);
        if (closeButton2) closeButton2.SetActive(true);
        if (Notebook) Notebook.SetActive(true);
        if (notebookContents) notebookContents.SetActive(true);
    }

    public void CloseDolab2()
    {
        if (closeButton) closeButton.SetActive(true);
        if (closeButton2) closeButton2.SetActive(false);
        if (Notebook) Notebook.SetActive(false);
        if (notebookContents) notebookContents.SetActive(false);
    }

    public void OpenLockCloseView()
    {
        if (puzzelRoot) puzzelRoot.SetActive(true);
        if (playDolabAnimationButton) playDolabAnimationButton.SetActive(false);
        if (PuzzelBriefCase) PuzzelBriefCase.SetActive(true);

        if (DolabCamera) DolabCamera.SetActive(true);
        if (closeButton) closeButton.SetActive(true);
        if (Button_Inventory) Button_Inventory.SetActive(false);
        if (Button_Thread) Button_Thread.SetActive(false);
    }
}
