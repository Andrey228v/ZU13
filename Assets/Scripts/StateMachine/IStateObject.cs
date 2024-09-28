using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public interface IStateObject
    {
        public SpriteRenderer Renderer { get;}
        public Animator Animator { get;}
        public Rigidbody2D Rigidbody { get;}


    }
}
