import re
from ctypes import c_ushort
from idlelib.configdialog import is_int
from typing import Dict, Union

PATTERN = re.compile(r"([a-z\d]+)? ?(AND|OR|LSHIFT|RSHIFT|NOT) ([a-z\d]+)")

instructions: Dict[str, Union[str, int]] = {}


def replace_var(var: Union[str, int, None]) -> Union[int, None]:
    if var is None:
        return var
    if is_int(var):
        return int(var)
    found_instruction = instructions.get(var)
    if found_instruction is not None and type(found_instruction) is int:
        return found_instruction


def parse_instruction(instr_in: str) -> Union[str, int]:
    match = PATTERN.match(instr_in)
    if match is None:
        found_instruction = instructions.get(instr_in)
        if type(found_instruction) is int:
            return found_instruction
        else:
            return instr_in
    left = replace_var(match.group(1))
    op = match.group(2)
    right = replace_var(match.group(3))
    if type(right) is int:
        if op == "NOT":
            instr_in = ~ right
        elif type(left) is int:
            if op == "AND":
                instr_in = left & right
            elif op == "OR":
                instr_in = left | right
            elif op == "LSHIFT":
                instr_in = left << right
            elif op == "RSHIFT":
                instr_in = left >> right
            else:
                raise Exception(f"Unknown operator {op}")

    return instr_in


with open("input.txt") as file:
    for instruction in file:
        instrIn, instrOut = instruction.strip('\n').split(' -> ')
        if instrIn.isnumeric():
            instrIn = int(instrIn)
        else:
            instrIn = parse_instruction(instrIn)

        instructions[instrOut] = instrIn

while type(instructions["a"]) is str:
    for key, value in instructions.items():
        if type(value) is str:
            instructions[key] = parse_instruction(value)

for key, value in instructions.items():
    if type(value) is int:
        instructions[key] = c_ushort(value)
print(instructions['a'])
