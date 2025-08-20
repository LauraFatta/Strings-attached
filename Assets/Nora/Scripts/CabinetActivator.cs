using UnityEngine;
using UnityEngine.Rendering;


public class DrawerActivator : MonoBehaviour

{
    [SerializeField] public GameObject AllButton;
    [SerializeField] public GameObject ButtonToHide;

    [SerializeField] public GameObject threadBook;
    [SerializeField] public GameObject ThreadContent;

    [SerializeField] public GameObject policeStation_UI;
    [SerializeField] private GameObject TextOfEachDrawer;
    [SerializeField] public GameObject drawer;


    [SerializeField] public GameObject drawerCamera;
    [SerializeField] private GameObject FirstcloseButton;
    [SerializeField] private GameObject SecondcloseButton;
    [SerializeField] private GameObject threadCloseButton;



    [Header("Main Buttons")]
    [SerializeField] private GameObject DrawerFileButton;   
    [SerializeField] private GameObject DrawerFileButton2;  
    [SerializeField] private GameObject DrawerFileButton3;  
    [SerializeField] private GameObject FileBackground;
    [Header("First Drawer")]
    
    [SerializeField] private GameObject OpenFileButton;
    [SerializeField] private GameObject BookPanel;

    [Header("Second Drawer")]
    [SerializeField] private GameObject OpenFileButton2;
    [SerializeField] private GameObject BookPanel2;

    [Header("Third Drawer")]
    [SerializeField] private GameObject OpenFileButton3;
    [SerializeField] private GameObject BookPanel3;

    [Header("Statements Files)")]
    [SerializeField] private GameObject WitnessFileBook;
    [SerializeField] private GameObject SuspectsFileBook;
    [SerializeField] private GameObject SuspectsFileBook2;


    [Header("Evidence Files)")]
    [SerializeField] private GameObject EvidencesBook;
    [SerializeField] private GameObject EvidencesBook2;
    [SerializeField] private GameObject EvidencesBook3;
    [SerializeField] private GameObject EvidencesBook4;
    [SerializeField] private GameObject EvidencesBook5;
    [SerializeField] private GameObject EvidencesBook6;


    [Header("Case Roster Files)")]
    [SerializeField] private GameObject CaseRosterBook;
    [SerializeField] private GameObject CaseRosterBook2;
    [SerializeField] private GameObject CaseRosterBook3;


    // ===== Drawer =====
    public void OpenDrawer()
    {
        DrawerFileButton.SetActive(true);
        DrawerFileButton2.SetActive(true);
        DrawerFileButton3.SetActive(true);
        FirstcloseButton.SetActive(true);
        TextOfEachDrawer.SetActive(true);
        drawer.SetActive(true);
        drawerCamera.SetActive(true);

        AllButton.SetActive(false);
        ButtonToHide.SetActive(false);
        policeStation_UI.SetActive(false);


    }


    // ===== Navigation UI =====
    public void Openfile()
    {
        FileBackground.SetActive(true);
        OpenFileButton.SetActive(true);

        SecondcloseButton.SetActive(true);

        DrawerFileButton.SetActive(false);
        DrawerFileButton2.SetActive(false);
        DrawerFileButton3.SetActive(false);
        FirstcloseButton.SetActive(false); TextOfEachDrawer.SetActive(false);

    }
    public void Openfile2()
    {
        FileBackground.SetActive(true);
        OpenFileButton2.SetActive(true);
        SecondcloseButton.SetActive(true);

        DrawerFileButton.SetActive(false);
        DrawerFileButton2.SetActive(false);
        DrawerFileButton3.SetActive(false);
        FirstcloseButton.SetActive(false);
        TextOfEachDrawer.SetActive(false);

    }
    public void Openfile3()
    {
        FileBackground.SetActive(true);
        OpenFileButton3.SetActive(true);
        SecondcloseButton.SetActive(true);

        DrawerFileButton.SetActive(false);
        DrawerFileButton2.SetActive(false);
        DrawerFileButton3.SetActive(false);
        FirstcloseButton.SetActive(false);
        TextOfEachDrawer.SetActive(false);

    }

    public void openFileBook()
    {

        BookPanel.SetActive(true);
        AllButton.SetActive(true);
        DrawerFileButton.SetActive(false);
        DrawerFileButton2.SetActive(false);
        DrawerFileButton3.SetActive(false);
        OpenFileButton.SetActive(false);
        OpenFileButton2.SetActive(false);
        OpenFileButton3.SetActive(false);
        TextOfEachDrawer.SetActive(false);



    }
    public void openFileBook2()
    {

        BookPanel2.SetActive(true);
        AllButton.SetActive(true);
        DrawerFileButton.SetActive(false);
        DrawerFileButton2.SetActive(false);
        DrawerFileButton3.SetActive(false);
        OpenFileButton.SetActive(false);
        OpenFileButton2.SetActive(false);
        OpenFileButton3.SetActive(false);
        TextOfEachDrawer.SetActive(false);
    }

    public void openFileBook3()
    {

        BookPanel3.SetActive(true);
        AllButton.SetActive(true);
        DrawerFileButton.SetActive(false);
        DrawerFileButton2.SetActive(false);
        DrawerFileButton3.SetActive(false);
        OpenFileButton.SetActive(false);
        OpenFileButton2.SetActive(false);
        OpenFileButton3.SetActive(false);
        TextOfEachDrawer.SetActive(false);
    }



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
    public void nextEvidenceFileBook2()
    {
        if (EvidencesBook2) EvidencesBook2.SetActive(true);
        if (EvidencesBook) EvidencesBook.SetActive(false);
    }

    public void nextEvidenceFileBook3()
    {
        if (EvidencesBook3) EvidencesBook3.SetActive(true);
        if (EvidencesBook2) EvidencesBook2.SetActive(false);
    }
    public void nextEvidenceFileBook4()
    {
        if (EvidencesBook4) EvidencesBook4.SetActive(true);
        if (EvidencesBook3) EvidencesBook3.SetActive(false);
    }
    public void nextEvidenceFileBook5()
    {
        if (EvidencesBook5) EvidencesBook5.SetActive(true);
        if (EvidencesBook4) EvidencesBook4.SetActive(false);
    }

   public void nextEvidenceFileBook6()
   {
       if (EvidencesBook6) EvidencesBook6.SetActive(true);
       if (EvidencesBook5) EvidencesBook5.SetActive(false);
   }
    
    public void previousEvidenceFileBook1()
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
    public void previousEvidenceFileBook4()
    {
        if (EvidencesBook4) EvidencesBook4.SetActive(true);
        if (EvidencesBook5) EvidencesBook5.SetActive(false);
    }

    public void previousEvidenceFileBook5()
    {
        if (EvidencesBook5) EvidencesBook5.SetActive(true);
        if (EvidencesBook6) EvidencesBook6.SetActive(false);
    }
    //--------------Third Book Arrows-----------------
    public void nextToSuspectsCategory()
    {
        if (CaseRosterBook2) CaseRosterBook2.SetActive(true);
        if (CaseRosterBook) CaseRosterBook.SetActive(false);
        if (CaseRosterBook3) CaseRosterBook3.SetActive(false);
    }

    public void nextToVictimsCategory()
    {
        if (CaseRosterBook3) CaseRosterBook3.SetActive(true);
        if (CaseRosterBook2) CaseRosterBook2.SetActive(false);
        if (CaseRosterBook) CaseRosterBook.SetActive(false);
    }
    public void nextToWitnessesCategory()
    {
        if (CaseRosterBook) CaseRosterBook.SetActive(true);
        if (CaseRosterBook2) CaseRosterBook2.SetActive(false);
        if (CaseRosterBook3) CaseRosterBook3.SetActive(false);
    }

    public void BackToDrawer()
    {
        drawerCamera.SetActive(true);
        drawer.SetActive(true);
        TextOfEachDrawer.SetActive(true);
        FirstcloseButton.SetActive(true);
        DrawerFileButton.SetActive(true);
        DrawerFileButton2.SetActive(true);
        DrawerFileButton3.SetActive(true);

        FileBackground.SetActive(false);
        SecondcloseButton.SetActive(false);
        ButtonToHide.SetActive(false);
        AllButton.SetActive(false);
        threadCloseButton.SetActive(false);
        WitnessFileBook.SetActive(false);
        SuspectsFileBook.SetActive(false);
        SuspectsFileBook2.SetActive(false);
        EvidencesBook.SetActive(false);
        EvidencesBook2.SetActive(false);
        EvidencesBook3.SetActive(false);
        EvidencesBook4.SetActive(false);
        EvidencesBook5.SetActive(false);
        EvidencesBook6.SetActive(false);
        CaseRosterBook.SetActive(false);
        CaseRosterBook2.SetActive(false);
        CaseRosterBook3.SetActive(false);

    }



    public void CloseDrawer()
    {
        policeStation_UI.SetActive(true);
        AllButton.SetActive(true);
        ButtonToHide.SetActive(true);

        DrawerFileButton.SetActive(false);
        DrawerFileButton2.SetActive(false);
        DrawerFileButton3.SetActive(false);
        TextOfEachDrawer.SetActive(false);
        drawer.SetActive(false);
        drawerCamera.SetActive(false);
        FirstcloseButton.SetActive(false);
        FileBackground.SetActive(false);
        OpenFileButton.SetActive(false);
        BookPanel.SetActive(false);
        OpenFileButton2.SetActive(false);
        BookPanel2.SetActive(false);
        OpenFileButton3.SetActive(false);
        BookPanel2.SetActive(false);

    }

}