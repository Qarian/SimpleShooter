using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

    public GameObject evil;
    float fgfg;
    float dfdf;

	void Start ()
    {
        Invoke("Spawn", 5);
        dfdf = Time.time;
	}

    void Spawn()
    {
        fgfg = (Time.time - dfdf)/10;
        Instantiate(evil, this.transform.position, Quaternion.identity);
        Invoke("Spawn",Random.Range(7/(1+fgfg), 10/(1+fgfg)));
    }
}
