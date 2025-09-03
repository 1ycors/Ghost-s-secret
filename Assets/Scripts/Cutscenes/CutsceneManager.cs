using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private DialogueSO defDialogueSO;
    [SerializeField] private DialogueSO trueDialogueSO;


    [SerializeField] private GameObject player;
    [SerializeField] private Transform doorTarget;
    [SerializeField] private Transform stopSpot;
    [SerializeField] private GameObject theEndScreen;
    [SerializeField] private TextMeshProUGUI defEnd;
    [SerializeField] private TextMeshProUGUI trueEnd;

    private float speed = 3f;
    private void OnEnable()
    {
        if (Player.Instance != null)
        {
            player = Player.Instance.gameObject;
            playerAnim = Player.Instance.GetComponent<Animator>();
        }
    }
    public void StartDefaultCutscene() 
    {
        StartCoroutine(ActivateDefaultCutscene());  
    }
    private IEnumerator ActivateDefaultCutscene() 
    {
        yield return StartCoroutine(TryStartDialogue(defDialogueSO));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(MoveToSpot());
        yield return StartCoroutine(MoveToDoor());
        yield return UIManager.Instance.ScreenFader.FadeOut();
        theEndScreen.SetActive(true);
        defEnd.enabled = true;
        yield return UIManager.Instance.ScreenFader.FadeIn();
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
    private IEnumerator TryStartDialogue(DialogueSO dialogueSO) 
    {
        dialogue.StartDialogue(dialogueSO);

        while (dialogue.IsDialogueActive) 
            yield return null;
    }
    private IEnumerator MoveToSpot() 
    {
        if (Player.Instance != null)
        {
            Player.Instance.enabled = false;
        }
        playerAnim.SetBool("isMoving", true);

        while (Vector3.Distance(player.transform.position, stopSpot.position) > 0.1f)
        {
            Vector3 direction = (stopSpot.position - player.transform.position).normalized; //переводим разницу в расстоянии из 0.2 или 1.7 в 1 или -1 для аниматора

            playerAnim.SetFloat("Horizontal", direction.x);
            playerAnim.SetFloat("Vertical", direction.y);

            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                stopSpot.position,
                speed * Time.deltaTime
            );
            yield return null; //ждем следующий кадр
        }
        AnimatorUpdate();

        playerAnim.SetFloat("LastHorizontal", 1);
        playerAnim.SetFloat("Vertical", 0);

        yield return new WaitForSeconds(4f);
        AnimatorUpdate();
    }
    private IEnumerator MoveToDoor() 
    {
        if (Player.Instance != null)
        {
            Player.Instance.enabled = false;
        }
        playerAnim.SetBool("isMoving", true);

        while (Vector3.Distance(player.transform.position, doorTarget.position) > 0.1f)
        {
            Vector3 direction = (doorTarget.position - player.transform.position).normalized; //переводим разницу в расстоянии из 0.2 или 1.7 в 1 или -1 для аниматора

            playerAnim.SetFloat("Horizontal", direction.x);
            playerAnim.SetFloat("Vertical", direction.y);

            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                doorTarget.position,
                speed * Time.deltaTime
            );
            yield return null; //ждем следующий кадр
        }
        AnimatorUpdate();
        player.SetActive(false);

        Debug.Log("Игрок дошел до двери.");
    }
    private void AnimatorUpdate() 
    {
        playerAnim.SetBool("isMoving", false);
        playerAnim.SetFloat("Horizontal", 0);
        playerAnim.SetFloat("Vertical", 0);
    }
    public void StartTrueCutscene() 
    {
        StartCoroutine(ActivateTrueCutscene());
    }
    private IEnumerator ActivateTrueCutscene() 
    {
        yield return StartCoroutine(TryStartDialogue(trueDialogueSO));
        yield return UIManager.Instance.ScreenFader.FadeOut();
        theEndScreen.SetActive(true);
        trueEnd.enabled = true;
        yield return UIManager.Instance.ScreenFader.FadeIn();
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

}
