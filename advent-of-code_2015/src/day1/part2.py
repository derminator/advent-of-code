with open("input.txt") as file:
    data = file.read()

floor = 0

for i, instruction in enumerate(data):
    if instruction == '(':
        floor += 1
    elif instruction == ')':
        floor -= 1
    else:
        print("Invalid Instruction: " + instruction)

    if floor < 0:
        print(i + 1)
        exit(0)

print("Basement not found")
