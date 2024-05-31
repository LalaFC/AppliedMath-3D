using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSelectionMode : MonoBehaviour, ISelectionMode
{
    public void OnDeselect(Transform selection)
    {
        ChangeOutline(selection, 0);
    }

    public void OnSelect(Transform selection)
    {
        ChangeOutline(selection, 10);
    }
    private void ChangeOutline(Transform selection, int width)
    {
        Outline outline = selection.GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineWidth = width;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
