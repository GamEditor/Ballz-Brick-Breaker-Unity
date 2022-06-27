using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TimeController
{
     public static int LastPlayTime {private set; get;}
    public static double OneDayInSeconds = new TimeSpan(1, 0, 0, 0).TotalSeconds;
    

    public static int GetCurrentTime() {
        return ((int)UnixTimestamp());
    }

    public static void SaveLastPlayTime() {
        PlayerPrefs.SetInt("last_play_time", GetCurrentTime());
    }
    
    public static void LoadLastPlayTime() {
       LastPlayTime = PlayerPrefs.GetInt("last_play_time");
    }

    public static DateTime UnixEpoch =
        new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);


    public static double UnixTimestamp()
    {
        var timeSpan = (DateTime.UtcNow - UnixEpoch);
        return timeSpan.TotalSeconds;
    }
}