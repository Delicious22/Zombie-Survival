using UnityEngine;

// ���� ������ �� ����� ������
[CreateAssetMenu(menuName = "Scriptable/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float Health = 100f; // ���� ü��
    public float Damage = 20f; // ���� ������
    public float TimeBetAttack = 0.5f; // ���� ���� ��Ÿ��
    public float Speed = 2f; // ���� �ӵ�
    public int Score = 100; // ����� �� ����
    public Color SkinColor = Color.white; // ���� ����
}
