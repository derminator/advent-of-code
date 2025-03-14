code_chars = 0
memory_chars = 0
encoded_chars = 0


def encode_string(str_input):
    return '"' + str_input.replace("\\", "\\\\").replace("\"", "\\\"") + '"'

with open("../../.aoc/2015/8") as file:
    for string in file:
        string = string.strip("\n")
        code_chars += len(string)
        parsed_string = eval(string)
        encoded_string = encode_string(string)
        memory_chars += len(parsed_string)
        encoded_chars += len(encoded_string)

print(f"Code: {code_chars}")
print(f"Memory: {memory_chars}")
print(f"Difference: {code_chars - memory_chars}")
print(f"Encoded: {encoded_chars}")
print(f"Encoded Difference: {encoded_chars - code_chars}")
