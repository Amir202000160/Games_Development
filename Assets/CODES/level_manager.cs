using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_manager : MonoBehaviour
{
    public static level_manager instance;
    public Transform player;

    public int SCORE;
    public int levelItem; 
    public Transform[] Particles;
    public Transform MainCanvas;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
         Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
