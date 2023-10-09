using UnityEngine;

// 좀비 생성할 때 사용할 데이터
[CreateAssetMenu(menuName = "Scriptable/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float Health = 100f; // 좀비 체력
    public float Damage = 20f; // 좀비 데미지
    public float TimeBetAttack = 0.5f; // 좀비 공격 쿨타임
    public float Speed = 2f; // 좀비 속도
    public int Score = 100; // 잡았을 때 점수
    public Color SkinColor = Color.white; // 좀비 색깔
}
