using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WarningLight : MonoBehaviour, ISignal
{
    private Animator red;

    void Start()
    {
        red = GetComponent<Animator>();
        Floor1Console.consoleFalse.AddListener(Receiver);
    }

    public void Receiver(bool state)
    {
        if (!state)
        {
            Debug.Log("수신양호");
            AudioManager.Instance.Play("F1Siren",false,0.5f,0.7f);
            red.SetTrigger("Open");
            red.SetTrigger("Close");
        }
    }
   
    public void Clear(UnityEvent<bool> signal) { }
    public void Sender(bool state) { }
}
