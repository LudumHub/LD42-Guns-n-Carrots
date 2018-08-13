using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DpsCounter
{
    private const float SecondsToTrack = 7f;
    private readonly Queue<DpsRecord> records = new Queue<DpsRecord>();

    public float Dps
    {
        get
        {
            return records.Any() ? 
                records.Sum(r => r.damage) / SecondsToTrack : 0;
        }
    }

    public void Update()
    {
        while (records.Any() && records.Peek().IsExpired)
        {
            records.Dequeue();
        }
    }

    public void Register(float damage)
    {
        records.Enqueue(new DpsRecord(damage));
    }

    private class DpsRecord
    {
       public readonly float damage;

        private readonly float timestamp;

        public DpsRecord(float damage)
        {
            this.damage = damage;
            timestamp = Time.time;
        }

        public bool IsExpired
        {
            get { return ElapsedTime >= SecondsToTrack; }
        }

        public float EffectiveDps
        {
            get { return Mathf.Lerp(damage, 0, ElapsedTime / SecondsToTrack); }
        }

        private float ElapsedTime
        {
            get { return Time.time - timestamp; }
        }
    }
}