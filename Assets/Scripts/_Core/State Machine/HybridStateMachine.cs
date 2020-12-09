using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public abstract class HybridStateMachine
    {
        public virtual MonoState CurrentState
        {
            get { return currentState; }
            set { Transition(value); }
        }

        [SerializeField]
        protected Dictionary<Type, MonoState> states = new Dictionary<Type, MonoState>();
        protected MonoState currentState;
        protected bool inTransition;

        public MonoState firstState;

        protected virtual void Start()
        {
            this.CurrentState = firstState;
        }

        public virtual void AddState<T>(T state) where T : MonoState
        {
            if (!states.ContainsKey(typeof(T)))
            {
                states.Add(typeof(T), state);
            }
        }

        public virtual T GetState<T>() where T : MonoState
        {
            if (!states.ContainsKey(typeof(T)))
            {
                Debug.LogError("State does not exist");
            }
            return (T)states[typeof(T)];
        }

        public virtual void ChangeState<T>() where T : MonoState
        {
            CurrentState = GetState<T>();
        }

        protected virtual void Transition(MonoState value)
        {
            if (currentState == value || inTransition)
            {
                return;
            }

            inTransition = true;

            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = value;

            if (currentState != null)
            {
                currentState.Enter();
            }

            inTransition = false;

            currentState.AfterTransition();
        }
    }

}
