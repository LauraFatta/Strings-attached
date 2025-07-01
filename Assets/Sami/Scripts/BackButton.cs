using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;

    void Start()
    {
        // Get components if not assigned
        if (animator == null)
            animator = GetComponent<Animator>();
        if (animatorFunctions == null)
            animatorFunctions = GetComponent<AnimatorFunctions>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Trigger hover animation
        if (animator != null)
        {
            animator.SetBool("selected", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Stop hover animation
        if (animator != null)
        {
            animator.SetBool("selected", false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Trigger click animation
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
        // Reset the pressed state after animation
        if (animator != null && animator.GetBool("pressed"))
        {
            // Check if mouse button is released
            if (!Input.GetMouseButton(0))
            {
                animator.SetBool("pressed", false);
            }
        }
    }
}