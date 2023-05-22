using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class move : MonoBehaviour
{
    CHAracter_States states;
    CharacterController controller;
    Animator anim;
    Transform cam;

    float G = 10f;
    [SerializeField] float V_velocity = 6.9f;
    [SerializeField] float jump = 3;
    [SerializeField] float speed = 5;

    AudioSource footSteps;
    AudioSource collect_sound;
    AudioSource S_move_sound;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();
        states = GetComponent<CHAracter_States>();
        collect_sound = GetComponentInChildren<AudioSource>();
        S_move_sound = GetComponentInChildren<AudioSource>();
        
        
    }

    // Update is called once per frame
    void Update()

    {
        // the movement

        float h_input = Input.GetAxis("Horizontal");
        float v_input = Input.GetAxis("Vertical");
        bool isSprint = Input.GetKey(KeyCode.LeftControl);
        float sprint = isSprint ? 7f : 1;
        ///////////////////////////////////////////////////////
        Vector3 m_dirc = new Vector3(h_input, 0, v_input);
        
        anim.SetFloat("speed", Mathf.Clamp(m_dirc.magnitude, 0f, 0.5f) + (isSprint ? 0.5f : 0));
       
        // fight code
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("fight");
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("melee");
            
        }
        /////////////////////////////////
        if (controller.isGrounded)
        {
            if (Input.GetAxis("Jump") > 0)
            {
                V_velocity = jump;
                anim.SetTrigger("jumb");
            }

        }
        else
            V_velocity -= G * Time.deltaTime;

        // makes the player rotate with the camera
        if (m_dirc.magnitude > 0.0)
        {
            float angle = Mathf.Atan2(m_dirc.x, m_dirc.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }


        m_dirc = cam.TransformDirection(m_dirc);
        m_dirc = new Vector3(m_dirc.x * sprint * speed, V_velocity, m_dirc.z * sprint * speed);
        controller.Move(m_dirc * Time.deltaTime);
 
    }
    //increase player health
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            GetComponent<CHAracter_States>().Changehealth(20);
            
            Destroy(other.gameObject);
         
            collect_sound.Play();
        }
        //increase score
        else if (other.CompareTag("Item"))
        {
            level_manager.instance.levelItem++;
            Debug.Log("items :"+ level_manager.instance.levelItem);
            
            Destroy(other.gameObject);
            collect_sound.Play();

        }
    }
    
    

    //////attack/////
    public void DoAttack()
    {
        transform.Find("Colider").GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(HideCollider());
    }
    IEnumerator HideCollider()
    {
        yield return new WaitForSeconds (0.5f);
        transform.Find("Colider").GetComponent<BoxCollider>().enabled = false;
    }

}
