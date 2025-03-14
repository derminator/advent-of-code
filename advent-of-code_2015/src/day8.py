code_chars = 0
memory_chars = 0

with open("../../.aoc/2015/8") as file:
    for string in file:
        string = string.strip("\n")
        code_chars += len(string)
        parsed_string = eval(string)
        memory_chars += len(parsed_string)

print(f"Code: {code_chars}")
print(f"Memory: {memory_chars}")
print(f"Difference: {code_chars - memory_chars}")
