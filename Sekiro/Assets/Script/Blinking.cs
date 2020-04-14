using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    private float timer;
    private bool blink = true;
    // Start is called before the first frame update
    void Start()
    {
        blink = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (blink)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5)
            {
                GetComponent<Text>().enabled = true;
            }
            if (timer >= 1)
            {
                GetComponent<Text>().enabled = false;
                timer = 0f;
            }
        }
    }

}
