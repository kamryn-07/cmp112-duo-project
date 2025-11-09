using UnityEngine;

public class MyBad : MonoBehaviour
{

    public int targFrameRate = 165;

    void Start()
    {

        // i am so sorry but we are so close to the deadline and i have spent DAYS trying to make anything work so this is my last resort
        // this sucks so bad and i should go to hell i know but please understand my struggles right now it's 4am and i've drank 2 litres
        // of coffee tonight alone to try and cope with my overwhelming exhaustion, PRETEND THIS ISN'T HERE, FOR GOD'S SAKE

        if (targFrameRate > 0)
        {
            Application.targetFrameRate = targFrameRate;
        }
        else
        {
            Application.targetFrameRate = 1000;
        }

    }

}
