import codecs

code_chars = 0
memory_chars = 0

with open("input.txt") as file:
    for string in file:
        string = string.strip("\n")
        code_chars += len(string)
        string_bytes = string[1:-1].encode()
        decoded_string = codecs.escape_decode(string_bytes)
        memory_chars += len(decoded_string)

print(f"Code: {code_chars}")
print(f"Memory: {memory_chars}")
print(f"Difference: {code_chars - memory_chars}")
