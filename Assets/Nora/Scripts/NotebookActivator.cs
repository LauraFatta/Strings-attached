// using UnityEngine;

// public class NotebookActivator : MonoBehaviour
// {
//     [Header("Notebook")]
//     public GameObject Notebook;
//     [SerializeField] private GameObject notebookUI;
//     [SerializeField] private GameObject NotebookCamera;
//     [SerializeField] private GameObject BlurBG;
//     [SerializeField] private GameObject DolabCloseButton;

//     [SerializeField] private GameObject closeButton;

//     [Header("Common UI")]
//     [SerializeField] private GameObject DrawerButton;  
//     [SerializeField] private GameObject AllButton;
//     [SerializeField] private GameObject InventoryButton;

//     [Header("Context Rules")]
//     [SerializeField] private GameObject[] keepDrawerContexts;

//     [SerializeField] private GameObject[] hideDrawerContexts;

//     [SerializeField] private bool keepDrawerDefault = true; 

//     private bool keepDrawerOnCloseThisTime;

//     public void OpenNotebook()
//     {
//         keepDrawerOnCloseThisTime = ComputeKeepDrawerOnClose();

//         SafeSetActive(DrawerButton, true);
//         SafeSetActive(DolabCloseButton, true);
//         SafeSetActive(Notebook, true);
//         SafeSetActive(notebookUI, true);
//         SafeSetActive(NotebookCamera, true);
//         SafeSetActive(BlurBG, true);
//         SafeSetActive(closeButton, true);
//         DolabCloseButton.SetActive(false);
//         SafeSetActive(AllButton, false);
//         SafeSetActive(InventoryButton, false);
//     }

//     public void CloseNotebook()
//     {
//         SafeSetActive(DrawerButton, keepDrawerOnCloseThisTime);

//         SafeSetActive(Notebook, false);
//         SafeSetActive(notebookUI, false);
//         SafeSetActive(NotebookCamera, false);
//         SafeSetActive(BlurBG, false);
//         SafeSetActive(closeButton, false);
//         DolabCloseButton.SetActive(false);


//         SafeSetActive(AllButton, true);
//         SafeSetActive(InventoryButton, true);
//     }

//     private bool ComputeKeepDrawerOnClose()
//     {
//         if (IsAnyActive(hideDrawerContexts)) return false;

//         if (IsAnyActive(keepDrawerContexts)) return true;

//         return keepDrawerDefault;
//     }

//     private bool IsAnyActive(GameObject[] arr)
//     {
//         if (arr == null) return false;
//         foreach (var go in arr)
//             if (go != null && go.activeInHierarchy) return true;
//         return false;
//     }

//     private void SafeSetActive(GameObject go, bool state)
//     {
//         if (go != null && go.activeSelf != state) go.SetActive(state);
//     }
// }



using UnityEngine;

public class NotebookActivator : MonoBehaviour
{
    [Header("Notebook")]
    public GameObject Notebook;
    [SerializeField] private GameObject notebookUI;
    [SerializeField] private GameObject NotebookCamera;
    [SerializeField] private GameObject BlurBG;
    [SerializeField] private GameObject DolabCloseButton;

    [SerializeField] private GameObject closeButton;

    [Header("Common UI")]
    [SerializeField] private GameObject DrawerButton;  
    [SerializeField] private GameObject AllButton;
    [SerializeField] private GameObject InventoryButton;

    [Header("Drawer Rules (on close)")]
    [SerializeField] private GameObject[] keepDrawerContexts;
    [SerializeField] private GameObject[] hideDrawerContexts;
    [SerializeField] private bool keepDrawerDefault = true; 

    [Header("Dolab Close Rules (on close)")]
    [SerializeField] private GameObject[] showDolabCloseContexts;
    [SerializeField] private GameObject[] hideDolabCloseContexts;
    [SerializeField] private bool dolabCloseDefault = false;

    private bool keepDrawerOnCloseThisTime;

    public void OpenNotebook()
    {
        keepDrawerOnCloseThisTime = ComputeKeepDrawerOnClose();

        SafeSetActive(DrawerButton, true);
        SafeSetActive(DolabCloseButton, true);
        SafeSetActive(Notebook, true);
        SafeSetActive(notebookUI, true);
        SafeSetActive(NotebookCamera, true);
        SafeSetActive(BlurBG, true);
        SafeSetActive(closeButton, true);

        // نخفي زر الدولاب أثناء فتح النوتبوك
        if (DolabCloseButton) DolabCloseButton.SetActive(false);

        SafeSetActive(AllButton, false);
        SafeSetActive(InventoryButton, false);
    }

    public void CloseNotebook()
    {
        SafeSetActive(DrawerButton, keepDrawerOnCloseThisTime);

        SafeSetActive(Notebook, false);
        SafeSetActive(notebookUI, false);
        SafeSetActive(NotebookCamera, false);
        SafeSetActive(BlurBG, false);
        SafeSetActive(closeButton, false);

        // هنا نقرر ظهور زر الدولاب حسب الكونتكست زي فكرة الدراور
        SafeSetActive(DolabCloseButton, ComputeDolabCloseOnClose());

        SafeSetActive(AllButton, true);
        SafeSetActive(InventoryButton, true);
    }

    private bool ComputeKeepDrawerOnClose()
    {
        if (IsAnyActive(hideDrawerContexts)) return false;
        if (IsAnyActive(keepDrawerContexts)) return true;
        return keepDrawerDefault;
    }

    private bool ComputeDolabCloseOnClose()
    {
        if (IsAnyActive(hideDolabCloseContexts)) return false;
        if (IsAnyActive(showDolabCloseContexts)) return true;
        return dolabCloseDefault;
    }

    private bool IsAnyActive(GameObject[] arr)
    {
        if (arr == null) return false;
        foreach (var go in arr)
            if (go != null && go.activeInHierarchy) return true;
        return false;
    }

    private void SafeSetActive(GameObject go, bool state)
    {
        if (go != null && go.activeSelf != state) go.SetActive(state);
    }
}
