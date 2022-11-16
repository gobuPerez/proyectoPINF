using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score;

    public int getScore () {

        return score;

    }

    public void modifyScore (int value) {

        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);


    }

    public void resetScore () {

        score = 0;

    }
}
