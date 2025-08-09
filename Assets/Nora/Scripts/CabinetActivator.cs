
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
    [SerializeField] private GameObject DlabFileButton;
    [SerializeField] private GameObject FileBackground;
    [SerializeField] private GameObject OpenFileButton;
    [SerializeField] private GameObject BookPanel;
    // [SerializeField] private GameObject BackButton;

    [SerializeField] private GameObject SuspectsFileBook;
    [SerializeField] private GameObject SuspectsFileBook2;
    [SerializeField] private GameObject WitnessFileBook;
    



    public void OpenDrawer()
    {
        drawer.SetActive(true);
        drawerBackground.SetActive(true);
        blurVolume.weight = 1;
        drawerCamera.SetActive(true);
        closeButton.SetActive(true);
        allButton.SetActive(false);
        DlabFileButton.SetActive(true);
        // PnelFileBook.SetActive(true);

    }


    public void CloseDrawer()
    {
        drawer.SetActive(false);
        drawerBackground.SetActive(false);
        blurVolume.weight = 0;
        drawerCamera.SetActive(false);
        closeButton.SetActive(false);
        allButton.SetActive(true);
        DlabFileButton.SetActive(false);
        FileBackground.SetActive(false);
        OpenFileButton.SetActive(false);
        BookPanel.SetActive(false);
        SuspectsFileBook.SetActive(false);
        SuspectsFileBook2.SetActive(false);
        WitnessFileBook.SetActive(false);
        

        

    }
    public void Openfile()
    {
        FileBackground.SetActive(true);
        OpenFileButton.SetActive(true);
        DlabFileButton.SetActive(false);
    }

    public void openFileBook()
    {
        BookPanel.SetActive(true);
        // BackButton.SetActive(true);
        DlabFileButton.SetActive(false);
    }

    public void nextWitnessFileBook()
    {
        SuspectsFileBook.SetActive(true);
        WitnessFileBook.SetActive(false);

    }
    public void nextSuspectsFileBook()
    {
        SuspectsFileBook2.SetActive(true);
        SuspectsFileBook.SetActive(false);

    }
    public void previousSuspectsFileBook()
    {
        WitnessFileBook.SetActive(true);
        SuspectsFileBook.SetActive(false);
    }
    public void previousSuspectsFileBook2()
    {
        SuspectsFileBook.SetActive(true);
        SuspectsFileBook2.SetActive(false);
    }

}
