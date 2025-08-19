

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;   // NEW

public class TypewriterActivator : MonoBehaviour
{
    [SerializeField] public GameObject policeStationUI;
    [SerializeField] public GameObject typewriter;
    [SerializeField] private GameObject Typewriterbackground;
    [SerializeField] public GameObject typewriterCamera;
    [SerializeField] private GameObject closeButton;
    [SerializeField] public GameObject ButtonToHide1;
    [SerializeField] public GameObject ButtonToHide2;
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
        policeStationUI.SetActive(false);
        typewriter.SetActive(true);
        Typewriterbackground.SetActive(true);
        typewriterCamera.SetActive(true);
        closeButton.SetActive(true);
        ButtonToHide1.SetActive(false);
        ButtonToHide2.SetActive(false);
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
        ButtonToHide1.SetActive(true);
        ButtonToHide2.SetActive(true);
        playTypewriterAnimationButton.SetActive(false);
        typewriterAnimator.SetBool("play", false);
        policeStationUI.SetActive(true);
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
