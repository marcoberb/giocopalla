using System.Collections;
using UnityEngine;

public class PointingArrow : MonoBehaviour
{
    void Start()
    {
        //for_testing PlayerPrefs.SetString("first_time", "true");

        //from first run of app until user open "InfoPanel", pref will be !=false
        if ( PlayerPrefs.GetString("first_time").Equals("false") )
            this.gameObject.SetActive(false);
        else
        {
            this.gameObject.SetActive(true);
            StartCoroutine(ArrowBouncing());
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetString("first_time").Equals("false"))
            this.gameObject.SetActive(false);
    }

    /* IEnumerator ArrowBouncing(): bounces the arrow until "first_time" pref is false */
    private IEnumerator ArrowBouncing()
    {
        while (!PlayerPrefs.GetString("first_time").Equals("false"))
        {
            for (int i = 0; i < 20; i++)
            {
                this.gameObject.transform.Translate(Vector2.down * 0.5f);
                yield return new WaitForEndOfFrame();
            }
            for (int i = 0; i < 20; i++)
            {
                this.gameObject.transform.Translate(Vector2.up * 0.5f);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
