using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;

//Made by Jeb

public class Hats : MonoBehaviour
{
    public UnityEvent addHealth;
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] bool isPreFab;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickUpSFX, transform.position);
            GameObject.FindWithTag("Player").GetComponent<Player>().recieveHealth();
            if(isPreFab)
                GameObject.FindWithTag("Player").GetComponentInChildren<addHats>().recieveHat();
            else
                addHealth.Invoke();
            Destroy(gameObject);
        }
    }

    /*public void recieveHealth()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health = GameObject.FindWithTag("Player").GetComponent<Player>().playerData.health + 1;
    }*/
}
