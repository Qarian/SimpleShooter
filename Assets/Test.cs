using UnityEngine;

public class Test : MonoBehaviour {

    [SerializeField] float hidetime = 10.0f;
    float appeartime;
    Renderer mesh;

    void Start()
    {
        mesh = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent <Player>() != null)
        {
            if(mesh.enabled == true)
            {
                collision.gameObject.GetComponent<Player>().Hit(-10f);
                mesh.enabled = false;
                appeartime = Time.time + hidetime;
            }
        }
    }

    void Update()
    {
        if (Time.time > appeartime && mesh.enabled==false)
            mesh.enabled = true;
    }
    
}

