using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Made by Stewy
/// 
/// Handles the cutscene at start of BossFight, depends on the BossMusic script to only finish the cutscene after the music has switched from track 1 to 2.
/// </summary>

public class StartOfScottShowdown : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject ScottHimSelf;

    [SerializeField]
    Transform CutScenePos;

    [SerializeField]
    GameObject PlayerPusher;

    [SerializeField]
    GameObject BackGround;

    [SerializeField] GameObject FightDeco;
    [SerializeField] GameObject PreFightDeco;

    bool hasBeenTriggered = false;
    bool hasStopped;

    BossMusic music;
    private void Start()
    {
        music = FindObjectOfType<BossMusic>().GetComponent<BossMusic>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenTriggered)
        {
            music.currentTrack = 1;
            hasBeenTriggered = true;
            hasStopped = false;
            StartCoroutine(CutScene(12.5f) );
            PlayerPusher.SetActive(true);
            FindObjectOfType<Player>().RemoveInputAndAudio(12.5f);
        }
    }


    IEnumerator ToPos(GameObject GO,Vector3 pos, float time, float moveSpeed)
    { 
        while(GO.transform.position != CutScenePos.position && !hasStopped)
        { 
            GO.transform.position = Vector2.MoveTowards(GO.transform.position, pos, moveSpeed);
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator CutScene(float time)
    {
        cam.GetComponent<CamFollowPlayer>().enabled = false;
        StartCoroutine(ToPos(cam.gameObject, CutScenePos.position + new Vector3(0,0,20000), 0.01f, 0.1f) );
        ScottHimSelf.SetActive(true);
        ScottHimSelf.GetComponent<ScottFightMainController>().StartFight(false);
        StartCoroutine(ToPos(ScottHimSelf, CutScenePos.position, 0.05f, 0.15f) );

        while (!music.fightCanStart)
        {
            yield return new WaitForEndOfFrame();
        }
        hasStopped = true;
        ScottHimSelf.GetComponent<ScottFightMainController>().StartFight(true);
        cam.GetComponent<CamFollowPlayer>().enabled = true;
        BackGround.gameObject.SetActive(true); 
        FightDeco.gameObject.SetActive(true);
        PreFightDeco.gameObject.SetActive(false);
    }



}
