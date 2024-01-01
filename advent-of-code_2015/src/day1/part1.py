with open("input.txt") as file:
    data = file.read()

floor = 0

for instruction in data:
    if instruction == '(':
        floor += 1
    elif instruction == ')':
        floor -= 1
    else:
        print("Invalid Instruction: " + instruction)

print(floor)
