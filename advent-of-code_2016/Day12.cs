using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2016;

public static class Day12
{
    public static void Run()
    {
        var instructions = File.ReadAllLines("../../../.aoc/2016/12");
        var part1 = new Computer(instructions);
        Console.WriteLine(part1.RunProgram());
        var part2 = new Computer(instructions, 1);
        Console.WriteLine(part2.RunProgram());
    }

    private class Computer(string[] instructions, int ignitionPosition = 0)
    {
        private readonly Dictionary<Register, int> _registerValues = new()
        {
            { Register.A, 0 },
            { Register.B, 0 },
            { Register.C, ignitionPosition },
            { Register.D, 0 }
        };

        private int _instructionPointer;

        private Register ReadRegister(string name)
        {
            return name switch
            {
                "a" => Register.A,
                "b" => Register.B,
                "c" => Register.C,
                "d" => Register.D,
                _ => throw new Exception("Unknown register: " + name + " in instruction: " +
                                         instructions[_instructionPointer] + "")
            };
        }

        private void Cpy(Register sourceRegister, Register destinationRegister)
        {
            _registerValues[destinationRegister] = _registerValues[sourceRegister];
        }

        private void Cpy(int value, Register destinationRegister)
        {
            _registerValues[destinationRegister] = value;
        }

        private void Inc(Register register)
        {
            _registerValues[register]++;
        }

        private void Dec(Register register)
        {
            _registerValues[register]--;
        }

        private static int Jnz(int value, int offset)
        {
            return value != 0 ? offset : 1;
        }

        public int RunProgram()
        {
            while (_instructionPointer < instructions.Length)
            {
                var instruction = instructions[_instructionPointer];
                var instructionParts = instruction.Split(' ');
                var instructionType = instructionParts[0];
                var instructionJump = 1;
                switch (instructionType)
                {
                    case "cpy":
                        if (int.TryParse(instructionParts[1], out var value))
                            Cpy(value, ReadRegister(instructionParts[2]));
                        else Cpy(ReadRegister(instructionParts[1]), ReadRegister(instructionParts[2]));
                        break;
                    case "inc":
                        Inc(ReadRegister(instructionParts[1]));
                        break;
                    case "dec":
                        Dec(ReadRegister(instructionParts[1]));
                        break;
                    case "jnz":
                        instructionJump =
                            Jnz(
                                int.TryParse(instructionParts[1], out var jumpValue)
                                    ? jumpValue
                                    : _registerValues[ReadRegister(instructionParts[1])],
                                int.Parse(instructionParts[2]));
                        break;
                }

                _instructionPointer += instructionJump;
            }

            return _registerValues[Register.A];
        }

        private enum Register
        {
            A,
            B,
            C,
            D
        }
    }
}