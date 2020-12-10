using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    [SerializeField] private GameObject[] instructions;
    [SerializeField] private GameObject prevBtn, nextBtn;
    private int activeIndex = 0;

    private void OnEnable()
    {
        foreach(var g in instructions){
            g.SetActive(false);
        }
        activeIndex = 0;
        instructions[activeIndex].SetActive(true);
        prevBtn.SetActive(false);
    }

    public void NextInstruction()
    {
        if (activeIndex + 1 < instructions.Length){

            instructions[activeIndex].SetActive(false);
            activeIndex++;
            instructions[activeIndex].SetActive(true);
            if (activeIndex > 0 && activeIndex < instructions.Length -1)
            {
                prevBtn.SetActive(true);
                nextBtn.SetActive(true);
            }
            else if (activeIndex == 0)
            {
                prevBtn.SetActive(false);
                nextBtn.SetActive(true);
            }
            else if (activeIndex == instructions.Length - 1)
            {
                prevBtn.SetActive(true);
                nextBtn.SetActive(false);
            }
        }
        Debug.Log(activeIndex);
    }

    public void PreviousInstruction()
    {
        if (activeIndex - 1 >= 0){

            instructions[activeIndex].SetActive(false);
            activeIndex--;
            instructions[activeIndex].SetActive(true);
            if (activeIndex > 0 && activeIndex < instructions.Length)
            {
                prevBtn.SetActive(true);
                nextBtn.SetActive(true);
            }
            else if (activeIndex == 0)
            {
                prevBtn.SetActive(false);
                nextBtn.SetActive(true);
            }
            else if (activeIndex == instructions.Length - 1)
            {
                prevBtn.SetActive(true);
                nextBtn.SetActive(false);
            }
        }
    }

}
