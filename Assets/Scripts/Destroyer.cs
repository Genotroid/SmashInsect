using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroyer : MonoBehaviour
{
    public event UnityAction InsectMissed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SafeInsect>())
            InsectMissed?.Invoke();
        Destroy(collision.gameObject);
    }
}
