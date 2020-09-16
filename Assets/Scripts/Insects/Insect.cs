using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InsectMover))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Insect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected int Health = 1;
    [SerializeField] protected int ScoreReward = 1;

    public int Score => ScoreReward; 

    public event UnityAction<Insect> Smashed;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (--Health <= 0)
        {
            Smashed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}