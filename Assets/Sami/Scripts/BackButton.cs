using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (animatorFunctions == null)
            animatorFunctions = GetComponent<AnimatorFunctions>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetBool("selected", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetBool("selected", false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetBool("pressed", true);
        }
        
        if (animatorFunctions != null)
        {
            animatorFunctions.disableOnce = true;
        }
    }

    void Update()
    {
        if (animator != null && animator.GetBool("pressed"))
        {
            if (!Input.GetMouseButton(0))
            {
                animator.SetBool("pressed", false);
            }
        }
    }
}