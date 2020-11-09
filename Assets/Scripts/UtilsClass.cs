using UnityEngine;

public class UtilsClass
{

    #region Sound
    /*
     * This Region was made by Liandur, 
     * and can be found at https://answers.unity.com/questions/283192/how-to-convert-decibel-number-to-audio-source-volu.html   
     */
    public static float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    public static float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20.0f);

        return linear;
    }
    #endregion
}