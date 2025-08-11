using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DrawerActivator : MonoBehaviour
{
    [SerializeField] public GameObject drawer;
    [SerializeField] private GameObject drawerBackground;
    [SerializeField] private Volume blurVolume;

    [SerializeField] public GameObject drawerCamera;
    [SerializeField] private GameObject FirstcloseButton;
    [SerializeField] private GameObject SecondcloseButton;    [SerializeField] private GameObject allButton;
    [SerializeField] private GameObject InventoryButton;

    [Header("Main Buttons")]
    [SerializeField] private GameObject DlabFileButton;   // زر 1
    [SerializeField] private GameObject DlabFileButton2;  // زر 2
    [SerializeField] private GameObject DlabFileButton3;  // زر 3

    [Header("First Drawer")]
    [SerializeField] private GameObject FileBackground;
    [SerializeField] private GameObject OpenFileButton;
    [SerializeField] private GameObject BookPanel;

    [Header("Second Drawer")]
    [SerializeField] private GameObject OpenFileButton2;
    [SerializeField] private GameObject BookPanel2;

    [Header("Third Drawer")]
    [SerializeField] private GameObject OpenFileButton3;
    [SerializeField] private GameObject BookPanel3;

    [Header("Default Files (زر 1/3)")]
    [SerializeField] private GameObject WitnessFileBook;
    [SerializeField] private GameObject SuspectsFileBook;
    [SerializeField] private GameObject SuspectsFileBook2;


    [Header("Button 2 Files (خانات خاصة بزر 2)")]
    [SerializeField] private GameObject EvidencesBook;
    [SerializeField] private GameObject EvidencesBook2;
    [SerializeField] private GameObject EvidencesBook3;
    [SerializeField] private GameObject EvidencesBook4;

    [Header("Button 3 Files (خانات خاصة بزر 3)")]
    [SerializeField] private GameObject CharacterFileBook;
    [SerializeField] private GameObject CharacterFileBook2;
    [SerializeField] private GameObject CharacterFileBook3;

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
        SetActiveSafe(EvidencesBook, false);
        SetActiveSafe(EvidencesBook2, false);
        SetActiveSafe(EvidencesBook3, false);
        SetActiveSafe(EvidencesBook4, false);
    }

    private void HideBtn3Files()
    {
        SetActiveSafe(CharacterFileBook, false);
        SetActiveSafe(CharacterFileBook2, false);
        SetActiveSafe(CharacterFileBook3, false);
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
        FirstcloseButton.SetActive(true);
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
        SetActiveSafe(OpenFileButton2, false);
        SetActiveSafe(BookPanel2, false);
        SetActiveSafe(OpenFileButton2, false);
        SetActiveSafe(BookPanel2, false);
    }

    public void BackToDrawer()
    {
        drawer.SetActive(true);
        drawerBackground.SetActive(true);
        blurVolume.weight = 1;
        drawerCamera.SetActive(true);
        FirstcloseButton.SetActive(true);
        allButton.SetActive(false);
        InventoryButton.SetActive(false);

        DlabFileButton.SetActive(true);
        DlabFileButton2.SetActive(true);
        DlabFileButton3.SetActive(true);
        SecondcloseButton.SetActive(false);
        // نظّف الحالة
        HideAllFiles();
        SetActiveSafe(FileBackground, false);
        SetActiveSafe(OpenFileButton, false);
        SetActiveSafe(BookPanel, false);
        SetActiveSafe(OpenFileButton2, false);
        SetActiveSafe(BookPanel2, false);
        SetActiveSafe(OpenFileButton3, false);
        SetActiveSafe(BookPanel3, false);

      

    }



    public void CloseDrawer()
    {
        drawer.SetActive(false);
        drawerBackground.SetActive(false);
        blurVolume.weight = 0;
        drawerCamera.SetActive(false);
        FirstcloseButton.SetActive(false);
        allButton.SetActive(true);
        InventoryButton.SetActive(true);

        DlabFileButton.SetActive(false);
        DlabFileButton2.SetActive(false);
        DlabFileButton3.SetActive(false);

        FileBackground.SetActive(false);
        OpenFileButton.SetActive(false);
        BookPanel.SetActive(false);
        OpenFileButton2.SetActive(false);
        BookPanel2.SetActive(false);
        OpenFileButton3.SetActive(false);
        BookPanel2.SetActive(false);

        HideAllFiles();
    }

    // ===== Navigation UI =====
    public void Openfile()
    {
        FileBackground.SetActive(true);
        OpenFileButton.SetActive(true);

        SecondcloseButton.SetActive(true);

        DlabFileButton.SetActive(false);
        DlabFileButton2.SetActive(false);
        DlabFileButton3.SetActive(false);
        FirstcloseButton.SetActive(false);

    }
    public void Openfile2()
    {
        FileBackground.SetActive(true);
        OpenFileButton2.SetActive(true);
        SecondcloseButton.SetActive(true);

        DlabFileButton.SetActive(false);
        DlabFileButton2.SetActive(false);
        DlabFileButton3.SetActive(false);
        FirstcloseButton.SetActive(false);

    }
    public void Openfile3()
    {
        FileBackground.SetActive(true);
        OpenFileButton3.SetActive(true);
        SecondcloseButton.SetActive(true);

        DlabFileButton.SetActive(false);
        DlabFileButton2.SetActive(false);
        DlabFileButton3.SetActive(false);
        FirstcloseButton.SetActive(false);

    }

    public void openFileBook()
    {
        BookPanel.SetActive(true);

        DlabFileButton.SetActive(false);
        DlabFileButton2.SetActive(false);
        DlabFileButton3.SetActive(false);
        OpenFileButton.SetActive(false);
        OpenFileButton2.SetActive(false);
        OpenFileButton3.SetActive(false);


    }
    public void openFileBook2()
    {
        BookPanel2.SetActive(true);

        DlabFileButton.SetActive(false);
        DlabFileButton2.SetActive(false);
        DlabFileButton3.SetActive(false);
        OpenFileButton.SetActive(false);
        OpenFileButton2.SetActive(false);
        OpenFileButton3.SetActive(false);
    }

    public void openFileBook3()
    {
        BookPanel3.SetActive(true);

        DlabFileButton.SetActive(false);
        DlabFileButton2.SetActive(false);
        DlabFileButton3.SetActive(false);
        OpenFileButton.SetActive(false);
        OpenFileButton2.SetActive(false);
        OpenFileButton3.SetActive(false);
    }
    // // ===== زر 2 =====
    // // استدعاء ملفات زر 2 فقط (يستخدم خانات زر 2)
    // public void OpenSuspectAndWitnessFiles_Btn2()
    // {
    //     HideDefaultFiles();
    //     HideBtn3Files();

    //     SetActiveSafe(EvidencesBook, true);
    //     SetActiveSafe(EvidencesBook2, true);
    //     SetActiveSafe(EvidencesBook3, true);
    // }

    // // ===== زر 3 =====
    // // استدعاء ملفات زر 3 فقط (الخانات الجديدة)
    // public void OpenSuspectAndWitnessFiles_Btn3()
    // {
    //     HideDefaultFiles();
    //     HideBtn2Files();

    //     SetActiveSafe(Btn3_SuspectsFileBook, true);
    //     SetActiveSafe(Btn3_WitnessFileBook, true);
    //     SetActiveSafe(Btn3_SuspectsFileBook2, true);
    // }

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



    //--------------Second Book Arrows-----------------
    public void nextEvidenceFileBook()
    {
        if (EvidencesBook2) EvidencesBook2.SetActive(true);
        if (EvidencesBook) EvidencesBook.SetActive(false);
    }

    public void nextEvidenceFileBook2()
    {
        if (EvidencesBook3) EvidencesBook3.SetActive(true);
        if (EvidencesBook2) EvidencesBook2.SetActive(false);
    }
    public void nextEvidenceFileBook3()
    {
        if (EvidencesBook4) EvidencesBook4.SetActive(true);
        if (EvidencesBook3) EvidencesBook3.SetActive(false);
    }

    public void previousEvidenceFileBook()
    {
        if (EvidencesBook) EvidencesBook.SetActive(true);
        if (EvidencesBook2) EvidencesBook2.SetActive(false);
    }

    public void previousEvidenceFileBook2()
    {
        if (EvidencesBook2) EvidencesBook2.SetActive(true);
        if (EvidencesBook3) EvidencesBook3.SetActive(false);
    }

    public void previousEvidenceFileBook3()
    {
        if (EvidencesBook3) EvidencesBook3.SetActive(true);
        if (EvidencesBook4) EvidencesBook4.SetActive(false);
    }

    //--------------Third Book Arrows-----------------
    public void nextCharacterFileBook()
    {
        if (CharacterFileBook2) CharacterFileBook2.SetActive(true);
        if (CharacterFileBook) CharacterFileBook.SetActive(false);
    }

    public void nextCharacterFileBook2()
    {
        if (CharacterFileBook3) CharacterFileBook3.SetActive(true);
        if (CharacterFileBook2) CharacterFileBook2.SetActive(false);
    }

    public void previousCharacterFileBook()
    {
        if (CharacterFileBook) CharacterFileBook.SetActive(true);
        if (CharacterFileBook2) CharacterFileBook2.SetActive(false);
    }

    public void previousCharacterFileBook2()
    {
        if (CharacterFileBook2) CharacterFileBook2.SetActive(true);
        if (CharacterFileBook3) CharacterFileBook3.SetActive(false);
    }
}
