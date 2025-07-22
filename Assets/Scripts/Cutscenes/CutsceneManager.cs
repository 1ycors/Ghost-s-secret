using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    //���������� ����, ���������� ������
    //������������ ��������� � �����
    //���������, ����� ������������ �� �������� � ��� �� ������ �� �����
    //��������� ������� ��� �� ������ ������� "�����...?"

    [SerializeField] private Animator playerAnim;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private DialogueSO defDialogueSO;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform doorTarget;
    [SerializeField] private Transform stopSpot;

    private float speed = 3f;
    private void Start()
    {
        if (playerAnim == null)
            playerAnim = player.GetComponent<Animator>();
    }
    public void StartDefaultCutscene() 
    {
        StartCoroutine(ActivateDefaultCutscene());  
    }
    private IEnumerator ActivateDefaultCutscene() 
    {
        yield return StartCoroutine(TryStartDialogue());
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(MoveToSpot());
        yield return StartCoroutine(MoveToDoor());
    }
    private IEnumerator TryStartDialogue() 
    {
        dialogue.StartDialogue(defDialogueSO);

        while (dialogue.IsDialogueActive) 
            yield return null;
    }
    private IEnumerator MoveToSpot() 
    {
        var playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        playerAnim.SetBool("isMoving", true);

        while (Vector3.Distance(player.transform.position, stopSpot.position) > 0.1f)
        {
            Vector3 direction = (stopSpot.position - player.transform.position).normalized; //��������� ������� � ���������� �� 0.2 ��� 1.7 � 1 ��� -1 ��� ���������

            playerAnim.SetFloat("Horizontal", direction.x);
            playerAnim.SetFloat("Vertical", direction.y);

            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                stopSpot.position,
                speed * Time.deltaTime
            );
            yield return null; //���� ��������� ����
        }
        AnimatorUpdate();

        playerAnim.SetFloat("Horizontal", 1);
        playerAnim.SetFloat("Vertical", 0);

        yield return new WaitForSeconds(4f);
        AnimatorUpdate();
    }
    private IEnumerator MoveToDoor() 
    {
        var playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        playerAnim.SetBool("isMoving", true);

        while (Vector3.Distance(player.transform.position, doorTarget.position) > 0.1f)
        {
            Vector3 direction = (doorTarget.position - player.transform.position).normalized; //��������� ������� � ���������� �� 0.2 ��� 1.7 � 1 ��� -1 ��� ���������

            playerAnim.SetFloat("Horizontal", direction.x);
            playerAnim.SetFloat("Vertical", direction.y);

            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                doorTarget.position,
                speed * Time.deltaTime
            );
            yield return null; //���� ��������� ����
        }
        AnimatorUpdate();
        player.SetActive(false);

        Debug.Log("����� ����� �� �����.");
    }
    private void AnimatorUpdate() 
    {
        playerAnim.SetBool("isMoving", false);
        playerAnim.SetFloat("Horizontal", 0);
        playerAnim.SetFloat("Vertical", 0);
    }
}
