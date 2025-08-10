using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DrawerActivator : MonoBehaviour
{
    [SerializeField] public GameObject drawer;
    [SerializeField] private GameObject drawerBackground;
    [SerializeField] private Volume blurVolume;

    [SerializeField] public GameObject drawerCamera;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject allButton;
    [SerializeField] private GameObject InventoryButton; 

    [Header("Main Buttons")]
    [SerializeField] private GameObject DlabFileButton;   // زر 1
    [SerializeField] private GameObject DlabFileButton2;  // زر 2
    [SerializeField] private GameObject DlabFileButton3;  // زر 3

    [Header("Common UI")]
    [SerializeField] private GameObject FileBackground;
    [SerializeField] private GameObject OpenFileButton;
    [SerializeField] private GameObject BookPanel;

    [Header("Default Files (زر 1/3)")]
    [SerializeField] private GameObject SuspectsFileBook;
    [SerializeField] private GameObject SuspectsFileBook2;
    [SerializeField] private GameObject WitnessFileBook;

    [Header("Button 2 Files (خانات خاصة بزر 2)")]
    [SerializeField] private GameObject Btn2_SuspectsFileBook;
    [SerializeField] private GameObject Btn2_SuspectsFileBook2;
    [SerializeField] private GameObject Btn2_WitnessFileBook;

    [Header("Button 3 Files (خانات خاصة بزر 3)")]
    [SerializeField] private GameObject Btn3_SuspectsFileBook;
    [SerializeField] private GameObject Btn3_SuspectsFileBook2;
    [SerializeField] private GameObject Btn3_WitnessFileBook;

    // ===== Helpers =====
    private void SetActiveSafe(GameObject go, bool state)
    {
        if (go != null) go.SetActive(state);
    }

    private void HideDefaultFiles()
    {
        SetActiveSafe(SuspectsFileBook, false);
        SetActiveSafe(SuspectsFileBook2, false);
        SetActiveSafe(WitnessFileBook, false);
    }

    private void HideBtn2Files()
    {
        SetActiveSafe(Btn2_SuspectsFileBook, false);
        SetActiveSafe(Btn2_SuspectsFileBook2, false);
        SetActiveSafe(Btn2_WitnessFileBook, false);
    }

    private void HideBtn3Files()
    {
        SetActiveSafe(Btn3_SuspectsFileBook, false);
        SetActiveSafe(Btn3_SuspectsFileBook2, false);
        SetActiveSafe(Btn3_WitnessFileBook, false);
    }

    private void HideAllFiles()
    {
        HideDefaultFiles();
        HideBtn2Files();
        HideBtn3Files();
    }

    // ===== Drawer =====
    public void OpenDrawer()
    {
        drawer.SetActive(true);
        drawerBackground.SetActive(true);
        blurVolume.weight = 1;
        drawerCamera.SetActive(true);
        closeButton.SetActive(true);
        allButton.SetActive(false);
        InventoryButton.SetActive(false); 

        DlabFileButton.SetActive(true);
        DlabFileButton2.SetActive(true);
        DlabFileButton3.SetActive(true);

        // نظّف الحالة
        HideAllFiles();
        SetActiveSafe(FileBackground, false);
        SetActiveSafe(OpenFileButton, false);
        SetActiveSafe(BookPanel, false);
    }

    public void CloseDrawer()
    {
        drawer.SetActive(false);
        drawerBackground.SetActive(false);
        blurVolume.weight = 0;
        drawerCamera.SetActive(false);
        closeButton.SetActive(false);
        allButton.SetActive(true);
        InventoryButton.SetActive(true);

        DlabFileButton.SetActive(false);
        DlabFileButton2.SetActive(false);
        DlabFileButton3.SetActive(false);

        FileBackground.SetActive(false);
        OpenFileButton.SetActive(false);
        BookPanel.SetActive(false);

        HideAllFiles();
    }

    // ===== Navigation UI =====
    public void Openfile()
    {
        FileBackground.SetActive(true);
        OpenFileButton.SetActive(true);

        DlabFileButton.SetActive(false);
        DlabFileButton2.SetActive(false);
        DlabFileButton3.SetActive(false);
    }

    public void openFileBook()
    {
        BookPanel.SetActive(true);

        DlabFileButton.SetActive(false);
        DlabFileButton2.SetActive(false);
        DlabFileButton3.SetActive(false);
    }

    // ===== زر 2 =====
    // استدعاء ملفات زر 2 فقط (يستخدم خانات زر 2)
    public void OpenSuspectAndWitnessFiles_Btn2()
    {
        HideDefaultFiles();
        HideBtn3Files();

        SetActiveSafe(Btn2_SuspectsFileBook, true);
        SetActiveSafe(Btn2_WitnessFileBook, true);
        SetActiveSafe(Btn2_SuspectsFileBook2, true);
    }

    // ===== زر 3 =====
    // استدعاء ملفات زر 3 فقط (الخانات الجديدة)
    public void OpenSuspectAndWitnessFiles_Btn3()
    {
        HideDefaultFiles();
        HideBtn2Files();

        SetActiveSafe(Btn3_SuspectsFileBook, true);
        SetActiveSafe(Btn3_WitnessFileBook, true);
        SetActiveSafe(Btn3_SuspectsFileBook2, true);
    }

    // ===== التنقل الافتراضي (زر 1/3 الافتراضي) =====
    public void nextWitnessFileBook()
    {
        if (SuspectsFileBook) SuspectsFileBook.SetActive(true);
        if (WitnessFileBook) WitnessFileBook.SetActive(false);
    }

    public void nextSuspectsFileBook()
    {
        if (SuspectsFileBook2) SuspectsFileBook2.SetActive(true);
        if (SuspectsFileBook) SuspectsFileBook.SetActive(false);
    }

    public void previousSuspectsFileBook()
    {
        if (WitnessFileBook) WitnessFileBook.SetActive(true);
        if (SuspectsFileBook) SuspectsFileBook.SetActive(false);
    }

    public void previousSuspectsFileBook2()
    {
        if (SuspectsFileBook) SuspectsFileBook.SetActive(true);
        if (SuspectsFileBook2) SuspectsFileBook2.SetActive(false);
    }
}
