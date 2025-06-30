using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;

    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetAxis ("Submit") == 1 || Input.GetAxis ("Fire1") == 1){
				animator.SetBool ("pressed", true);
			}else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}else{
			animator.SetBool ("selected", false);
		}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (menuButtonController.index == thisIndex)
        {
            animator.SetBool("pressed", true);
            animatorFunctions.disableOnce = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menuButtonController.index = thisIndex;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("selected", false); 
    }
}
