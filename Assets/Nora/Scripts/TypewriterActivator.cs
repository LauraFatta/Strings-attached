// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.UI;

// public class TypewriterActivator : MonoBehaviour
// {
//     [SerializeField] public GameObject policeStationCanvas;
//     [SerializeField] public GameObject typewriter;
//     [SerializeField] private GameObject Typewriterbackground;
//     [SerializeField] public GameObject typewriterCamera;
//     [SerializeField] private GameObject closeButton;
//     [SerializeField] private GameObject AllButton;
//     [SerializeField] private GameObject InventoryButton;
//     [SerializeField] private GameObject playTypewriterAnimationButton;
//     [SerializeField] private Animator typewriterAnimator;
//     [SerializeField] private GameObject TextOfTypeWriter;


//     public void OpenTypewriter()
//     {
//         TextOfTypeWriter.SetActive(true);
//         policeStationCanvas.SetActive(false);
//         typewriter.SetActive(true);
//         Typewriterbackground.SetActive(true);
//         typewriterCamera.SetActive(true);
//         closeButton.SetActive(true);
//         AllButton.SetActive(false);
//         InventoryButton.SetActive(false);
//         playTypewriterAnimationButton.SetActive(true);


//     }
//     public void PlayTypewriterAnimation()
//     {
//         typewriterAnimator.SetBool("play", true);
//     }
//     // public void CloseTypewriterAnimation()
//     // {
//     //     typewriterAnimator.SetBool("play", false);
//     // }


//     public void CloseTypewriter()
    
//     {
//         TextOfTypeWriter.SetActive(false);
//         typewriter.SetActive(false);
//         Typewriterbackground.SetActive(false);
//         typewriterCamera.SetActive(false);
//         closeButton.SetActive(false);
//         AllButton.SetActive(true);
//         InventoryButton.SetActive(true);
//         playTypewriterAnimationButton.SetActive(false);
//         typewriterAnimator.SetBool("play", false);
//         policeStationCanvas.SetActive(true);

//     }
// }


using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;   // NEW

public class TypewriterActivator : MonoBehaviour
{
    [SerializeField] public GameObject policeStationCanvas;
    [SerializeField] public GameObject typewriter;
    [SerializeField] private GameObject Typewriterbackground;
    [SerializeField] public GameObject typewriterCamera;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject AllButton;
    [SerializeField] private GameObject InventoryButton;
    [SerializeField] private GameObject playTypewriterAnimationButton;
    [SerializeField] private Animator typewriterAnimator;
    [SerializeField] private GameObject TextOfTypeWriter;

    // NEW: إعداد الانتقال
    [SerializeField] private string pastSceneName = "PastScene"; 
    [SerializeField] private int animatorLayer = 0;            
    [SerializeField] private float safetyTimeout = 30f;         

    private Coroutine loadCoro; // NEW

    public void OpenTypewriter()
    {
        TextOfTypeWriter.SetActive(true);
        policeStationCanvas.SetActive(false);
        typewriter.SetActive(true);
        Typewriterbackground.SetActive(true);
        typewriterCamera.SetActive(true);
        closeButton.SetActive(true);
        AllButton.SetActive(false);
        InventoryButton.SetActive(false);
        playTypewriterAnimationButton.SetActive(true);
    }

    public void PlayTypewriterAnimation()
    {
        typewriterAnimator.SetBool("play", true);

        if (loadCoro != null) StopCoroutine(loadCoro);
        loadCoro = StartCoroutine(WaitForAnimationThenLoad());
    }

    // public void CloseTypewriterAnimation()
    // {
    //     typewriterAnimator.SetBool("play", false);
    // }

    public void CloseTypewriter()
    {
        if (loadCoro != null)
        {
            StopCoroutine(loadCoro);
            loadCoro = null;
        }

        TextOfTypeWriter.SetActive(false);
        typewriter.SetActive(false);
        Typewriterbackground.SetActive(false);
        typewriterCamera.SetActive(false);
        closeButton.SetActive(false);
        AllButton.SetActive(true);
        InventoryButton.SetActive(true);
        playTypewriterAnimationButton.SetActive(false);
        typewriterAnimator.SetBool("play", false);
        policeStationCanvas.SetActive(true);
    }

    private System.Collections.IEnumerator WaitForAnimationThenLoad()
    {
        yield return null;

        float t = 0f;

    
        while (t < safetyTimeout)
        {
            var st = typewriterAnimator.GetCurrentAnimatorStateInfo(animatorLayer);
            if (!typewriterAnimator.IsInTransition(animatorLayer))
                break;

            t += Time.deltaTime;
            yield return null;
        }

        t = 0f;
        while (t < safetyTimeout)
        {
            var st = typewriterAnimator.GetCurrentAnimatorStateInfo(animatorLayer);
            if (!typewriterAnimator.IsInTransition(animatorLayer) && st.normalizedTime >= 1f)
                break;

            t += Time.deltaTime;
            yield return null;
        }

        if (!string.IsNullOrEmpty(pastSceneName))
            SceneManager.LoadScene(pastSceneName);
        else
            Debug.LogWarning("[TypewriterActivator] pastSceneName is empty.");

        loadCoro = null;
    }
    
}
