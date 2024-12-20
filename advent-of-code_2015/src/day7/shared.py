import re
from idlelib.configdialog import is_int
from typing import Union, Dict

INSTRUCTIONS = Dict[str, Union[str, int]]

PATTERN = re.compile(r"([a-z\d]+)? ?(AND|OR|LSHIFT|RSHIFT|NOT) ([a-z\d]+)")


def replace_var(var: Union[str, int, None], instructions: INSTRUCTIONS) -> Union[int, None]:
    if var is None:
        return var
    if is_int(var):
        return int(var)
    found_instruction = instructions.get(var)
    if found_instruction is not None and type(found_instruction) is int:
        return found_instruction


def parse_instruction(instr_in: str, instructions: INSTRUCTIONS) -> Union[str, int]:
    match = PATTERN.match(instr_in)
    if match is None:
        found_instruction = instructions.get(instr_in)
        if type(found_instruction) is int:
            return found_instruction
        else:
            return instr_in
    left = replace_var(match.group(1), instructions)
    op = match.group(2)
    right = replace_var(match.group(3), instructions)
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


def read_file_instructions():
    file_instructions: INSTRUCTIONS = {}
    with open("input.txt") as file:
        for instruction in file:
            instr_in, instr_out = instruction.strip('\n').split(' -> ')
            if instr_in.isnumeric():
                instr_in = int(instr_in)

            file_instructions[instr_out] = instr_in
    return file_instructions


def find_a(instructions: INSTRUCTIONS):
    while type(instructions["a"]) is str:
        for key, value in instructions.items():
            if type(value) is str:
                instructions[key] = parse_instruction(value, instructions)
    return instructions["a"]
