using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InsectList
{
    [SerializeField] private Insect _insect;
    [SerializeField] private int _count;

    public Insect Insect => _insect;
    public int Count => _count;

    public void EnableInsect()
    {
        _count--;
    }
}
