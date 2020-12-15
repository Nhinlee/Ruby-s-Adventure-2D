using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCJambiController : MonoBehaviour
{
    [SerializeField]
    private GameObject dialog;
    [SerializeField]
    private float timeShowDialog = 4f;

    private float dialogTimer;

    private void Update()
    {
        dialogTimer -= Time.deltaTime;
        if(dialogTimer <= 0)
        {
            dialog.SetActive(false);
        }
    }
    public void ShowDialog()
    {
        dialogTimer = timeShowDialog;
        dialog.SetActive(true);
    }
}
