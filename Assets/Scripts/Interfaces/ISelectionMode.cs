using UnityEngine;

public interface ISelectionMode
{
    void OnSelect(Transform selection);
    void OnDeselect(Transform selection);
}
