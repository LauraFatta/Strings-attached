// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class TutorialManager : MonoBehaviour
// {
//     [Header("Tutorial Panels")]
//     public GameObject[] tutorialPanels; // Assign all your panels here in order

//     [Header("Scene Settings")]
//     public string nextSceneName = "PoliceStation"; // Scene to load after tutorial

//     [Header("Navigation Settings")]
//     public KeyCode nextKey = KeyCode.RightArrow;
//     public KeyCode previousKey = KeyCode.LeftArrow;
//     public KeyCode skipKey = KeyCode.S;

//     private int currentPanelIndex = 0;
//     private bool canNavigate = true;

//     void Start()
//     {
//         // Initialize tutorial - show first panel, hide others
//         InitializeTutorial();
//     }

//     void Update()
//     {
//         if (!canNavigate) return;

//         // Handle input
//         HandleInput();
//     }

//     void InitializeTutorial()
//     {
//         // Hide all panels first
//         foreach (GameObject panel in tutorialPanels)
//         {
//             if (panel != null)
//                 panel.SetActive(false);
//         }

//         // Show first panel if available
//         if (tutorialPanels.Length > 0 && tutorialPanels[0] != null)
//         {
//             tutorialPanels[0].SetActive(true);
//             currentPanelIndex = 0;
//         }

//         Debug.Log($"Tutorial initialized with {tutorialPanels.Length} panels");
//     }

//     void HandleInput()
//     {
//         // Skip tutorial
//         if (Input.GetKeyDown(skipKey))
//         {
//             SkipTutorial();
//             return;
//         }

//         // Navigate forward
//         if (Input.GetKeyDown(nextKey))
//         {
//             NavigateNext();
//         }

//         // Navigate backward
//         if (Input.GetKeyDown(previousKey))
//         {
//             NavigatePrevious();
//         }
//     }

//     public void NavigateNext()
//     {
//         if (currentPanelIndex < tutorialPanels.Length - 1)
//         {
//             // Move to next panel
//             StartCoroutine(SwitchPanel(currentPanelIndex + 1));
//         }
//         else
//         {
//             // Last panel - finish tutorial
//             FinishTutorial();
//         }
//     }

//     public void NavigatePrevious()
//     {
//         if (currentPanelIndex > 0)
//         {
//             StartCoroutine(SwitchPanel(currentPanelIndex - 1));
//         }
//     }

//     public void SkipTutorial()
//     {
//         Debug.Log("Tutorial skipped");
//         LoadNextScene();
//     }

//     public void FinishTutorial()
//     {
//         Debug.Log("Tutorial completed");
//         LoadNextScene();
//     }

//     IEnumerator SwitchPanel(int newPanelIndex)
//     {
//         canNavigate = false;

//         // Hide current panel
//         if (tutorialPanels[currentPanelIndex] != null)
//             tutorialPanels[currentPanelIndex].SetActive(false);

//         // Small delay for smoother transition (optional)
//         yield return new WaitForSeconds(0.1f);

//         // Show new panel
//         if (tutorialPanels[newPanelIndex] != null)
//             tutorialPanels[newPanelIndex].SetActive(true);

//         currentPanelIndex = newPanelIndex;

//         Debug.Log($"Switched to panel {currentPanelIndex + 1}/{tutorialPanels.Length}");

//         canNavigate = true;
//     }

//     void LoadNextScene()
//     {
//         canNavigate = false;

//         // Optional: Add fade out or transition effect here
//         StartCoroutine(LoadSceneWithDelay());
//     }

//     IEnumerator LoadSceneWithDelay()
//     {
//         // Small delay before scene change (optional)
//         yield return new WaitForSeconds(0.5f);

//         // Load the next scene
//         SceneManager.LoadScene(nextSceneName);
//     }

//     // Public methods for UI buttons (for future use)
//     public void OnNextButtonClicked()
//     {
//         NavigateNext();
//     }

//     public void OnPreviousButtonClicked()
//     {
//         NavigatePrevious();
//     }

//     public void OnSkipButtonClicked()
//     {
//         SkipTutorial();
//     }

//     // Helper method to get current progress
//     public float GetProgress()
//     {
//         return (float)(currentPanelIndex + 1) / tutorialPanels.Length;
//     }

//     // Method to jump to specific panel (useful for testing)
//     public void JumpToPanel(int panelIndex)
//     {
//         if (panelIndex >= 0 && panelIndex < tutorialPanels.Length)
//         {
//             StartCoroutine(SwitchPanel(panelIndex));
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial Panels")]
    public GameObject[] tutorialPanels; // Assign all your panels here in order

    [Header("Scene Settings")]
    public string nextSceneName = "PoliceStation"; // Scene to load after tutorial

    [Header("Navigation Settings")]
    public KeyCode nextKey = KeyCode.RightArrow;
    public KeyCode previousKey = KeyCode.LeftArrow;
    public KeyCode skipKey = KeyCode.S;

    [Header("Transition")]
    [SerializeField] private float fadeDuration = 0.35f;   // نعومة التبديل
    [SerializeField] private bool useUnscaledTime = true;  // لو التوتوريال أثناء إيقاف الوقت

    private int currentPanelIndex = 0;
    private bool canNavigate = true;

    // void Start()
    // {
    //     InitializeTutorial();
    // }

    void Update()
    {
        if (!canNavigate) return;
        HandleInput();
    }

    void InitializeTutorial()
    {
        foreach (GameObject panel in tutorialPanels)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        if (tutorialPanels.Length > 0 && tutorialPanels[0] != null)
        {
            var first = tutorialPanels[0];
            first.SetActive(true);

            // نضمن CanvasGroup ونثبت ألفا = 1 (لا يمس المقاسات أبدًا)
            var cg = EnsureCanvasGroup(first);
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
            cg.interactable = true;

            currentPanelIndex = 0;
        }

        Debug.Log($"Tutorial initialized with {tutorialPanels.Length} panels");
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(skipKey))
        {
            SkipTutorial();
            return;
        }

        if (Input.GetKeyDown(nextKey))
        {
            NavigateNext();
        }

        if (Input.GetKeyDown(previousKey))
        {
            NavigatePrevious();
        }
    }

    public void NavigateNext()
    {
        if (currentPanelIndex < tutorialPanels.Length - 1)
        {
            StartCoroutine(SwitchPanel(currentPanelIndex + 1));
        }
        else
        {
            FinishTutorial();
        }
    }

    public void NavigatePrevious()
    {
        if (currentPanelIndex > 0)
        {
            StartCoroutine(SwitchPanel(currentPanelIndex - 1));
        }
    }

    public void SkipTutorial()
    {
        // Debug.Log("Tutorial skipped");
        // LoadNextScene();
        gameObject.SetActive(false); 
    }

    public void FinishTutorial()
    {
        // Debug.Log("Tutorial completed");
        // LoadNextScene();
        gameObject.SetActive(false); 
    }

    IEnumerator SwitchPanel(int newPanelIndex)
    {
        canNavigate = false;

        var cur = (currentPanelIndex >= 0 && currentPanelIndex < tutorialPanels.Length) ? tutorialPanels[currentPanelIndex] : null;
        var nxt = (newPanelIndex >= 0 && newPanelIndex < tutorialPanels.Length) ? tutorialPanels[newPanelIndex] : null;

        if (nxt == null)
        {
            canNavigate = true;
            yield break;
        }

        // فعّل الجديد فورًا لكن اجعله شفافًا
        if (!nxt.activeSelf) nxt.SetActive(true);
        var cgNext = EnsureCanvasGroup(nxt);
        cgNext.alpha = 0f;
        cgNext.blocksRaycasts = false;
        cgNext.interactable = false;

        CanvasGroup cgCur = null;
        if (cur != null)
        {
            if (!cur.activeSelf) cur.SetActive(true);
            cgCur = EnsureCanvasGroup(cur);
            cgCur.alpha = 1f;
            cgCur.blocksRaycasts = false; // لا يستقبل ضغط أثناء الانتقال
            cgCur.interactable = false;
        }

        // كروس فيد فقط (لا تحريك، لا تغيير مقاسات)
        float t = 0f;
        while (t < fadeDuration)
        {
            t += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            float k = Mathf.Clamp01(t / fadeDuration);

            if (cgCur)  cgCur.alpha  = 1f - k;
            if (cgNext) cgNext.alpha = k;

            yield return null;
        }

        if (cgCur)  cgCur.alpha = 0f;
        if (cgNext) cgNext.alpha = 1f;

        // أخفِ القديم وفعّل التفاعل على الجديد
        if (cur) cur.SetActive(false);
        cgNext.blocksRaycasts = true;
        cgNext.interactable = true;

        currentPanelIndex = newPanelIndex;
        Debug.Log($"Switched to panel {currentPanelIndex + 1}/{tutorialPanels.Length}");

        canNavigate = true;
    }


    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextSceneName);
    }

    public void OnNextButtonClicked()    { NavigateNext(); }
    public void OnPreviousButtonClicked(){ NavigatePrevious(); }
    public void OnSkipButtonClicked()    { SkipTutorial(); }

    public float GetProgress()
    {
        return (float)(currentPanelIndex + 1) / tutorialPanels.Length;
    }

    public void JumpToPanel(int panelIndex)
    {
        if (panelIndex >= 0 && panelIndex < tutorialPanels.Length)
        {
            StartCoroutine(SwitchPanel(panelIndex));
        }
    }

    // يضمن وجود CanvasGroup بدون أي تأثير على المقاس/الـLayout
    private CanvasGroup EnsureCanvasGroup(GameObject go)
    {
        var cg = go.GetComponent<CanvasGroup>();
        if (!cg) cg = go.AddComponent<CanvasGroup>();
        return cg;
    }
}
