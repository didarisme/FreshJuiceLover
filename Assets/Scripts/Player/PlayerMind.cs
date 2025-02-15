using System;
using UnityEngine;

public class PlayerMind : MonoBehaviour
{
    [SerializeField] private GameObject[] bubbles;

    public static Action<int> ShowBubble;
    private int lastBubbleInd;

    private void OnEnable()
    {
        ShowBubble += ThinkingBubbleShow;
    }

    private void OnDisable()
    {
        ShowBubble -= ThinkingBubbleShow;
    }

    private void Awake()
    {
        ThinkingBubbleShow(-1);
    }

    private void ThinkingBubbleShow(int bubbleInd)
    {
        if (lastBubbleInd == bubbleInd)
            return;

        lastBubbleInd = bubbleInd;
        for (int i = 0; i < bubbles.Length; i++)
        {
            bubbles[i].SetActive(i == bubbleInd);
        }
    }
}