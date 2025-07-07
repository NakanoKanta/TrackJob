using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FightingSystem : MonoBehaviour
{
    [Header("�ݒ�")]
    [SerializeField] private Command[] _commands;
    [SerializeField] private Animator _animator;
    private InputManager _inputManager;
    private InputCommandBuffer _commandBuffer;

    void Start()
    {
        _inputManager = gameObject.AddComponent<InputManager>();
        _commandBuffer = new InputCommandBuffer();
        //�C�x���g�o�^
        _inputManager.OnInputDetected += OnInputReceived;
        //�f�t�H���g�R�}���h�ݒ�
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
        Debug.Log($"�R�}���h����: {command._name}");
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
                _name = "�g�����R�}���h",
                _inputs = new [] {InputType.Down, InputType.Right, InputType.Punch},
                _animation = "Hadoken"
            },
            new Command
            {
                _name = "�������R�}���h",
                _inputs = new [] {InputType.Right, InputType.Down, InputType.Punch},
                _animation = "Shoryuken"
            },
        };
    }
}