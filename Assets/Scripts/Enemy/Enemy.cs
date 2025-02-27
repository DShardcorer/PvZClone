// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Enemy
// {
//     private EnemyManager _parent;
//     private EnemyProperties _model;
//     private EnemyView _view;

//     public void Init(EnemyManager parent, EnemyProperties model)
//     {
//         _parent = parent;
//         _model = model;
//         _view = //lay pool sau do GetCompoment EnemyView
//     }

//     public void Dispo()
//     {
//         // return view to pool
//         _parent = null;
//         _model = null;  
//         _view = null;
//     }

//     public void MoveToTarget(Vector2 target)
//     {
//         //logic tinh toan
//         // chan cac dieu di chuyen stun, slow

//         _view.HandleMove(target);
//     }

//     public void Shoot(){
//         //call parent create bullet
//         _view.HandleShowEffectShoot();
//     }
// }
