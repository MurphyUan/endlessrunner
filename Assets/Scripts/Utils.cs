using System;
using System.Collections;
using System.Collections.Generic;

public static class Utils
{
    public delegate void PlayerDeath();
    public static PlayerDeath PlayerDeathEvent;

    public delegate void PlayerCoin(Coin coin);
    public static PlayerCoin PlayerCoinEvent;

    public delegate void PlayerPowerup(string powerupName);
    public static PlayerPowerup PlayerPowerupEvent;

    public delegate void MilestoneReached();
    public static MilestoneReached MilestoneReachedEvent;

    public static System.Random r = new System.Random();

    public static void PublishPlayerDeath()
    {
        if(PlayerDeathEvent != null)
            PlayerDeathEvent();
    }

    public static void PublishPlayerCoinEvent(Coin coin)
    {
        if(PlayerCoinEvent != null)
            PlayerCoinEvent(coin);
    }

    public static void PublishPlayerPowerupEvent(string powerupName)
    {
        if(PlayerPowerupEvent != null)
            PlayerPowerupEvent(powerupName);
    }

    public static void PublishMilestoneReachedEvent()
    {
        if(MilestoneReachedEvent != null)
            MilestoneReachedEvent();
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;

        while(n > 1)
        {
            n--;
            int k = r.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static List<T> CreateList<T>(params T[] values)
    {
        return new List<T>(values);
    }

    public static T GetRandomFromList<T>(this IList<T> list)
    {
        int n = list.Count;
        int k = r.Next(0, n);
        return list[k];
    }
}
