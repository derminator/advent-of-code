import copy
from ctypes import c_ushort

from day7.shared import read_file_instructions, find_a

initial_instructions = read_file_instructions()
instructions = copy.deepcopy(initial_instructions)
first_a = find_a(instructions)
initial_instructions['b'] = first_a

print(c_ushort(find_a(initial_instructions)))
