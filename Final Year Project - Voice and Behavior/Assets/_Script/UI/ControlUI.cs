using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
	[SerializeField] private GameObject ControlMenu;
	
    public void HideControlUI()
    {
        ControlMenu.SetActive(false);
    }
	

}
