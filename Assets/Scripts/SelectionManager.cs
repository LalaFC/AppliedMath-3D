using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private IRayCastProvider iRayCastProvider;
    private ISelectionMode iSelectionMode;
    private ISelector iSelector;

    private Transform currentSelection;

    private void Awake()
    {
        iRayCastProvider = GetComponent<IRayCastProvider>();
        iSelector = GetComponent<ISelector>();
        iSelectionMode = GetComponent<ISelectionMode>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSelection != null)
            iSelectionMode.OnDeselect(currentSelection);
            iSelector.Check(iRayCastProvider.CreateRay());
            currentSelection = iSelector.GetSelection();
        

        if (currentSelection != null )
        {
            iSelectionMode.OnSelect(currentSelection);
        }
    }
}
