
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
    

    public void OpenDrawer()
    {
        drawer.SetActive(true);
        drawerBackground.SetActive(true);
        blurVolume.weight = 1;
        drawerCamera.SetActive(true);
        closeButton.SetActive(true);
        allButton.SetActive(false);
        
    }

  
    public void CloseDrawer()
    {
        drawer.SetActive(false);
        drawerBackground.SetActive(false);
        blurVolume.weight = 0;
        drawerCamera.SetActive(false);
        closeButton.SetActive(false);
        allButton.SetActive(true);
       
    }
}
