import re
from ctypes import c_ushort
from typing import Dict, Union

PATTERN = re.compile(r"([a-z]+)? ?(AND|OR|LSHIFT|RSHIFT|NOT) ([a-z\d]+)")

instructions: Dict[str, Union[str, int]] = {}


def replace_var(var: Union[str, int, None]) -> Union[int, None]:
    if var is None or type(var) is int:
        return var
    found_instruction = instructions.get(var)
    if found_instruction and type(found_instruction) is int:
        return found_instruction


with open("input.txt") as file:
    for instruction in file:
        instrIn, instrOut = instruction.strip('\n').split(' -> ')
        if instrIn.isnumeric():
            instrIn = int(instrIn)
        else:
            match = PATTERN.match(instrIn)
            left = replace_var(match.group(1))
            op = match.group(2)
            right = replace_var(match.group(3))
            if type(right) is int:
                if op == "NOT":
                    instrIn = ~ right
                elif type(left) is int:
                    if op == "AND":
                        instrIn = left & right
                    elif op == "OR":
                        instrIn = left | right
                    elif op == "LSHIFT":
                        instrIn = left << right
                    elif op == "RSHIFT":
                        instrIn = left >> right
                    else:
                        raise Exception(f"Unknown operator {op}")

        instructions[instrOut] = instrIn

for key, value in instructions.items():
    if type(value) is int:
        instructions[key] = c_ushort(value)
print(instructions)
