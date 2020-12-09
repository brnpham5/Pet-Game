using System;
using System.Collections;
using System.Collections.Generic;

namespace Core {
    public class StateMachine {

        public virtual State CurrentState {
            get { return currentState; }
            set { Transition(value); }
        }

        protected Dictionary<Type, State> states = new Dictionary<Type, State>();
        protected State currentState;
        protected bool inTransition;

        public virtual void AddState<T>(T state) where T : State
        {
            if (!states.ContainsKey(typeof(T)))
            {
                states.Add(typeof(T), state);
            }
        }

        public virtual T GetState<T>() where T : State {
            if (!states.ContainsKey(typeof(T))) {
                states.Add(typeof(T), (T)Activator.CreateInstance(typeof(T)));
            }
            return (T)states[typeof(T)];
        }

        public virtual void ChangeState<T>() where T : State {
            CurrentState = GetState<T>();
        }

        protected virtual void Transition(State value) {
            if (currentState == value || inTransition) {
                return;
            }

            inTransition = true;

            if (currentState != null) {
                currentState.Exit();
            }

            currentState = value;

            if (currentState != null) {
                currentState.Enter();
            }

            inTransition = false;

            currentState.AfterTransition();
        }
    }

}
