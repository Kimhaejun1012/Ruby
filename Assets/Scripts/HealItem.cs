using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public int heal = 1;


    private void OnTriggerStay2D(Collider2D collision)
    {
        //var ruby = collision.GetComponent<RubyController>();
        //if(ruby != null)
        //{
        //    //Take Damage
        //}

        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("TakeHeal", heal, SendMessageOptions.DontRequireReceiver); //컴퍼넌트에도 붙어있고 게임오브젝트에도 있는 메소드임 호출할 메소드의 이름을 문자열로 넘겨서 호출함, 매개변수는 한개까지 가능 근데 오브젝트 형이라서 인트형 보내면 박싱함
            gameObject.SetActive(false);

        }
    }
}
