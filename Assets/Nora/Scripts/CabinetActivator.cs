
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



    public void OpenDrawer()
    {
        drawer.SetActive(true);
        drawerBackground.SetActive(true);
        blurVolume.weight = 1;
        drawerCamera.SetActive(true);
        closeButton.SetActive(true);
        allButton.SetActive(false);
        DlabFileButton.SetActive(true);

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

    }
    public void Openfile()
    {
        FileBackground.SetActive(true);
        OpenFileButton.SetActive(true);
    }

}
