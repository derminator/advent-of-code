from typing import Literal

a = 0
b = 0

REG = Literal['a', 'b']


def hlf(value: int):
    return value // 2


def tpl(value: int):
    return value * 3


def inc(value: int):
    return value + 1


with open('../../.aoc/2015/23') as f:
    instructions = f.readlines()

instruction_ptr = 0


def parse_instruction(instruction: str):
    global instruction_ptr
    instruction_jump = 1
    parts = instruction.strip().split(" ")
    instruction = parts[0]
    if instruction == "hlf":
        reg = parts[1]
        value = globals()[reg]
        globals()[reg] = hlf(value)
    elif instruction == "tpl":
        reg = parts[1]
        value = globals()[reg]
        globals()[reg] = tpl(value)
    elif instruction == "inc":
        reg = parts[1]
        value = globals()[reg]
        globals()[reg] = inc(value)
    elif instruction == "jmp":
        instruction_jump = int(parts[1])
    elif instruction == "jie":
        reg = parts[1][:-1]
        if globals()[reg] % 2 == 0:
            instruction_jump = int(parts[2])
    elif instruction == "jio":
        reg = parts[1][:-1]
        if globals()[reg] == 1:
            instruction_jump = int(parts[2])
    else:
        raise Exception(f"Unknown instruction: {instruction}")
    instruction_ptr += instruction_jump


while len(instructions) > instruction_ptr >= 0:
    parse_instruction(instructions[instruction_ptr])

print(b)
