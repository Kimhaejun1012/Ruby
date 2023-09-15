using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //var ruby = collision.GetComponent<RubyController>();
        //if(ruby != null)
        //{
        //    //Take Damage
        //}

        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver); //���۳�Ʈ���� �پ��ְ� ���ӿ�����Ʈ���� �ִ� �޼ҵ��� ȣ���� �޼ҵ��� �̸��� ���ڿ��� �Ѱܼ� ȣ����, �Ű������� �Ѱ����� ���� �ٵ� ������Ʈ ���̶� ��Ʈ�� ������ �ڽ���
        }
    }
}
