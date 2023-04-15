using UnityEngine;

public class EnemyDumbAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public int damage;

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
