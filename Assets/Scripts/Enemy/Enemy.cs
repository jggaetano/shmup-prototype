using UnityEngine;
using System.Collections.Generic;

public class Enemy {

    public const float flashTimer = 0.5f;

    public float ShotCooldown { get; protected set; }
    public float Timer { get; set; }
    public bool CanTurn { get; protected set; }

    public IEnemyState PassiveState { get; protected set; }
    public IEnemyState CycleState {
        get {
            if (states.Count == 0)
                return null;
            return states[currentState];
        }
        set { }
    }
    protected List<IEnemyState> states;
    private int currentState;
   
    public Enemy() {
        ShotCooldown = 0;
        CanTurn = true;
        currentState = 0;
        states = new List<IEnemyState>();
    }

    //public Enemy(float shotCooldown) {
    //    ShotCooldown = shotCooldown;
    //    CanTurn = true;
    //    currentState = 0;
    //    states = new List<IEnemyState>();
    //}

    public void NextState() {
        if (CycleState.OneTime)
            states.Remove(CycleState);
        else
            currentState = (currentState + 1) % states.Count;
    }

    public void RemoveState(IEnemyState state) {
        states.Remove(state);
    }

}
