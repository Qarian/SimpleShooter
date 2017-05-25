using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField] float startLife;
    public Image lifeBar;
    public Text lifetext;

    public float life;

    void Start()
    {
        life = startLife;
        lifetext.text = life.ToString();
    }

    public void Hit(float damage)
    {
        life -= damage;
        if (life <= 0)
            Die();
        else if (life > startLife)
            life = startLife;
        lifeBar.fillAmount = life / startLife;
        lifetext.text = life.ToString();
    }

    private void Die()
    {
        Destroy(gameObject.GetComponent<Collider>());
    }
}
