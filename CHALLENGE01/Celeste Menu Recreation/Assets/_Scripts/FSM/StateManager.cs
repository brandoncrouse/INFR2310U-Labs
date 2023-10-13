using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager {
    GameManager manager;
    public enum States {
        Title,
        Menu,
        SaveSelect,
        Credits,
        Exit
    }
    Dictionary<States, State> states = new Dictionary<States, State>();

    public StateManager(GameManager manager) {
        this.manager = manager;
        states[States.Title] = new TitleState(manager.TitleObjects, this);
        states[States.Menu] = new MenuState(manager.MenuObjects, this);
        states[States.Credits] = new CreditState(manager.CreditObjects, this);
        states[States.SaveSelect] = new SaveSelectState(manager.SaveObjects, this);
        //states[States.Exit] = new CleanRoomState(player, this);
        foreach (State state in states.Values) {
            state.Setup();
        }
    }

    public State Title() {
        return states[States.Title];
    }

    public State Menu() {
        return states[States.Menu];
    }

    public State Credit() {
        return states[States.Credits];
    }

    public State Save() {
        return states[States.SaveSelect];   
    }
}
