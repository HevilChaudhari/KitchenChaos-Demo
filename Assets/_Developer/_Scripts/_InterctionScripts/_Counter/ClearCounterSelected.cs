using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ClearCounterSelected : MonoBehaviour
{

    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _counterSelectedVisual;

    private void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += ChangeCounterVisual;
    }

    private void ChangeCounterVisual(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == _baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }


    private void Show()
    {
        foreach(GameObject counterVisual in _counterSelectedVisual)
        {
            counterVisual.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject counterVisual in _counterSelectedVisual)
        {
            counterVisual.SetActive(false);
        }
    }
}
