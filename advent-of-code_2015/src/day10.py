sequence = "1321131112"


def look_and_say():
    result = ""
    i = 0
    while i < len(sequence):
        count = 1
        while i + 1 < len(sequence) and sequence[i] == sequence[i + 1]:
            i += 1
            count += 1
        result += str(count) + sequence[i]
        i += 1
    return result


for _ in range(40):
    sequence = look_and_say()

print(len(sequence))

for _ in range(10):
    sequence = look_and_say()

print(len(sequence))
