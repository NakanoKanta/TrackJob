using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FightingSystem : MonoBehaviour
{
    [Header("設定")]
    [SerializeField] private Command[] _commands;
    [SerializeField] private Animator _animator;
    private InputManager _inputManager;
    private InputCommandBuffer _commandBuffer;

    void Start()
    {
        _inputManager = gameObject.AddComponent<InputManager>();
        _commandBuffer = new InputCommandBuffer();
        //イベント登録
        _inputManager.OnInputDetected += OnInputReceived;
        //デフォルトコマンド設定
        SetUpDefaultCommands();
    }

    void OnInputReceived(InputType input)
    {
        _commandBuffer.AddInput(input);
        CheckAllCommands();
    }

    void CheckAllCommands()
    {
        foreach (var command in _commands)
        {
            if (_commandBuffer.CheckCommand(command._inputs))
            {
                ExecuteCommand(command);
                _commandBuffer.Clear();
                break;
            }
        }
    }

    void ExecuteCommand(Command command)
    {
        Debug.Log($"コマンド発動: {command._name}");
        if (_animator != null)
        {
            _animator.SetTrigger(command._animation);
        }
    }

    void SetUpDefaultCommands()
    {
        _commands = new Command[]
        {
            new Command
            {
                _name = "波動拳コマンド",
                _inputs = new [] {InputType.Down, InputType.Right, InputType.Punch},
                _animation = "Hadoken"
            },
            new Command
            {
                _name = "昇竜拳コマンド",
                _inputs = new [] {InputType.Right, InputType.Down, InputType.Punch},
                _animation = "Shoryuken"
            },
        };
    }
}