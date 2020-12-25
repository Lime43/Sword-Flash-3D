using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Header("Canvas Sounds Settings")]
    public AudioClip kill;
    public AudioClip cashEarn;
    private void Awake()
    {
        instance = this;
    }
}
