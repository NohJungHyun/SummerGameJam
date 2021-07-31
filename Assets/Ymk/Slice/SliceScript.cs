using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceScript : MonoBehaviour
{
    private Vector3 start;
    private Vector3 end;

    public float speed;

    public GameObject swordEffect;

    public AudioSource audioSource;
    public AudioClip []swordSound;

    private void Update()
    {
        if (Time.deltaTime == 0 || GameOver.gameEnd)
            return;
        Vector3 screenToWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Debug.Log(screenToWorldPos.y);
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mouse);
        if (Input.GetMouseButtonDown(0) && mouse.y < screenToWorldPos.y*0.2f*3.7f)
        {
            start = mouse;
        }
        else if (Input.GetMouseButtonUp(0) && start.y < screenToWorldPos.y * 0.2f * 3.7f)
        {
            end = mouse;
            Vector3 center = (end + start) / 2;
            Vector3 dir = (end - start).normalized;

            //����
            RaycastHit2D[] raycastHit2D = Physics2D.BoxCastAll(center, new Vector3(Vector3.Distance(start, end), 0.5f, 1), Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, Vector2.zero, 10, 1 << LayerMask.NameToLayer("Ghost"));

            bool slashFlag = false;
            foreach (RaycastHit2D hit in raycastHit2D)
            {
                Ghost ghost = hit.transform.GetComponent<Ghost>();
                if (ghost == null)
                    continue;
                slashFlag = true;
                ghost.Die(true);
                ScoreManager.instance.kill++;
            }

            audioSource.PlayOneShot(swordSound[Random.Range(0, swordSound.Length)]);

            if (!slashFlag)
            {
                Debug.Log("아무것도 못뱀");
                ComboSystem.instance.remainComboTime = 0;
            }

            {
                //start->end�� ����Ʈ�� �̵��ϵ��� ����
                GameObject temp = Instantiate(swordEffect);
                temp.transform.position = start;
                SwordMove moveObj = temp.AddComponent<SwordMove>();
                moveObj.speed = speed;
                moveObj.target = end;
            }
            start = end;
        }

    }

}
