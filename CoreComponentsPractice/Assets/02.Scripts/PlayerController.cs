using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHp
{
    public float hp => _hp;
    public float hpMin => _hpMin;
    public float hpMax => _hpMax;
    
    private float _hp;
    private float _hpMin = 0.0f;
    private float _hpMax = 100.0f;
    // event ������ : �ܺ� Ŭ���������� �� �븮�ڸ� �� �� +=, -= �� �ǿ����ڷθ� ��밡��
    public event Action<float> onHpDepleted;

    public delegate void Action2<in T>(T t);
    public event Action2<float> onHpDepleted2;


    private void Awake()
    {
        _hp = _hpMax;
    }

    public void DepleteHp(float amount)
    {
        if (_hp <= _hpMin || amount <= 0)
            return;

        _hp -= amount;
        onHpDepleted?.Invoke(_hp);
    }
}
