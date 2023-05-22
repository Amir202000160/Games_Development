using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CHAracter_States : MonoBehaviour
{
    
    public float maxhealth = 100;
    public float power = 10;
    int killscore = 200;
    Animator ANIM;
    
    

    public float  currenthealth { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        ANIM = GetComponent<Animator>();
    }

    public void Changehealth(float value)
    {
        currenthealth = Mathf.Clamp(currenthealth + value, 0, maxhealth);
        Debug.Log("Current Health" + currenthealth + " / " + maxhealth);
        if (transform.CompareTag("Enemy"))
        {
            transform.Find("Canvas").GetChild(1).GetComponent<Image>().fillAmount = currenthealth / maxhealth;
        } 
        else if (transform.CompareTag("Player"))
        {
             level_manager.instance.MainCanvas.Find("Panel(ST)").Find("Image(health)").GetComponent<Image>().fillAmount = currenthealth / maxhealth;
        }

        if (currenthealth <= 0.0f)
        {
            DIE();
        }
        void DIE()
        {
            ///palyer die
            if (transform.CompareTag("Player"))
            {
                //game over
                //ANIM.SetTrigger("die");
                Invoke(nameof(Reloadlevel), 1.3f);


            }
            ///enemy die
            else if (transform.CompareTag("Enemy"))
            {
                level_manager.instance.SCORE += killscore;
                ANIM.SetTrigger("dead");
                Destroy(gameObject);
               //gameObject.CompareTag("Enemy").iskinematic = true;

                
            }
        }
        
    }
    void Reloadlevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  
}
