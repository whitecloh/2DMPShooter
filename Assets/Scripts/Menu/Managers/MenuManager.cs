using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField]
    private Menu[] _menus;

    private void Awake()
    {
        Instance = this;
    }
    public void OpenMenu(string menuName)
    {
        for(int i=0;i<_menus.Length;i++)
        {
            if(_menus[i].menuName == menuName)
            {
                _menus[i].Open();
            }
            else if(_menus[i].isOpen)
            {
                CloseMenu(_menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        menu.Open();
    }
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
