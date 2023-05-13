using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelVisibility : MonoBehaviour
{ 
 public GameObject panel;

void Start()
{
    panel.SetActive(false);
}

public void SpawnPanel()
{
    panel.SetActive(true);
}

}
