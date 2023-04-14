using UnityEngine;

public class EnemyDumbAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
