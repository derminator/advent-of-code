from ctypes import c_ushort

from day7.shared import find_a, read_file_instructions

instructions = read_file_instructions()

print(c_ushort(find_a(instructions)))
