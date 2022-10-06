using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PatrolBehaviour : MonoBehaviour
{
    
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    [SerializeField] private Transform[] pointsArray = new Transform[4];
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _currentTime;
    [SerializeField] private float _waitingTime = 3f;
    [SerializeField] private float _waitingTimer;
    private int _count = 1;
    
    void Update()
    {
          _currentTime += Time.deltaTime;
          var distance = Vector3.Distance(_start.position, _end.position);
          var travelTime = distance / _speed;
          var progress = _currentTime / travelTime ;
          var newPosition = Vector3.Lerp(_start.position, _end.position, progress);
          transform.position = newPosition;
          if (_currentTime > travelTime)
          {
              _waitingTimer += Time.deltaTime;
              if (_waitingTimer < _waitingTime) // Проверка таймера
              {
                  return;
              }
              _waitingTimer = 0;
              if (_count >= pointsArray.Length-1) // Проверка на выход за пределы массива
              {
                  _currentTime = 0;
                  _start = pointsArray[pointsArray.Length-1];
                  _end = pointsArray[0];
                  _count = 0;
              }
              else
              {
                  _count++;
                  _currentTime = 0;
                  _start = _end;
                  _end = pointsArray[_count];
              }
          }
    }
}
