// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.UI;

// public class NotebookActivator : MonoBehaviour
// {

//     [SerializeField] public GameObject Notebook;
//     [SerializeField] private GameObject BlurBG;
//     [SerializeField] private GameObject DrawerButton;
//     [SerializeField] private GameObject threadCloseButton;

//     [SerializeField] public GameObject NotebookCamera;
//     [SerializeField] private GameObject closeButton;
//     [SerializeField] private GameObject notebookUI;
//     [SerializeField] private GameObject AllButton;
//     [SerializeField] private GameObject InventoryButton;

//     public void OpenNotebook()
//     {
//         DrawerButton.SetActive(false);
//         threadCloseButton.SetActive(false);

//         Notebook.SetActive(true);
//         notebookUI.SetActive(true);
//         BlurBG.SetActive(true);
//         NotebookCamera.SetActive(true);
//         closeButton.SetActive(true);
//         AllButton.SetActive(false);
//         InventoryButton.SetActive(false);


//     }

//     public void CloseNotebook()
//     {
//         DrawerButton.SetActive(true);
//         Notebook.SetActive(false);
//         notebookUI.SetActive(false);
//         BlurBG.SetActive(false);
//         NotebookCamera.SetActive(false);
//         closeButton.SetActive(false);
//         AllButton.SetActive(true);
//         InventoryButton.SetActive(true);

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
    [SerializeField] private GameObject DrawerButton;   // أيقونة الرجوع تبع الدرج
    [SerializeField] private GameObject AllButton;
    [SerializeField] private GameObject InventoryButton;

    [Header("Context Rules")]
    [Tooltip("حطّي هنا كل شاشات الـBooks اللي تبغين فيها الدرج يظل شغال بعد الإغلاق")]
    [SerializeField] private GameObject[] keepDrawerContexts;

    [Tooltip("حطّي هنا الواجهتين اللي تبغين فيها الدرج ينطفّي بعد الإغلاق")]
    [SerializeField] private GameObject[] hideDrawerContexts;

    [Tooltip("لو ما لقينا أي سياق، وش الافتراضي؟")]
    [SerializeField] private bool keepDrawerDefault = true; // لأن أغلب الحالات = Books

    private bool keepDrawerOnCloseThisTime;

    // نفس زر الثريد يندّه على هذه
    public void OpenNotebook()
    {
        // نحدد القاعدة لهذه المرّة حسب الشاشة الحالية
        keepDrawerOnCloseThisTime = ComputeKeepDrawerOnClose();

        SafeSetActive(DrawerButton, true);
        SafeSetActive(DolabCloseButton, true);
        SafeSetActive(Notebook, true);
        SafeSetActive(notebookUI, true);
        SafeSetActive(NotebookCamera, true);
        SafeSetActive(BlurBG, true);
        SafeSetActive(closeButton, true);
        DolabCloseButton.SetActive(false);
        SafeSetActive(AllButton, false);
        SafeSetActive(InventoryButton, false);
    }

    // زر الإغلاق الوحيد للنوتبوك
    public void CloseNotebook()
    {
        // طبق القاعدة لهذه المرّة
        SafeSetActive(DrawerButton, keepDrawerOnCloseThisTime);

        SafeSetActive(Notebook, false);
        SafeSetActive(notebookUI, false);
        SafeSetActive(NotebookCamera, false);
        SafeSetActive(BlurBG, false);
        SafeSetActive(closeButton, false);
        DolabCloseButton.SetActive(true);


        SafeSetActive(AllButton, true);
        SafeSetActive(InventoryButton, true);
    }

    // ——— Helpers ———
    private bool ComputeKeepDrawerOnClose()
    {
        // الأسبقية: لو أي واجهة من الـ"إخفاء" شغّالة → نخفي الدرج
        if (IsAnyActive(hideDrawerContexts)) return false;

        // لو أي واجهة من الـ"إبقاء" شغّالة → نخلي الدرج
        if (IsAnyActive(keepDrawerContexts)) return true;

        // غير كذا نمشي على الافتراضي
        return keepDrawerDefault;
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
