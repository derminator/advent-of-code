def is_nice(text: str) -> bool:
    duplicate_letters_found = False
    pair_found = False

    prev = ""
    prev2 = ""
    pairs = []
    for char in text:
        if not duplicate_letters_found:
            if char == prev2:
                duplicate_letters_found = True

            if not pair_found and prev:
                pair = prev + char
                if pair in pairs[:-1]:
                    pair_found = True
                pairs.append(pair)

            prev2 = prev
            prev = char

    if pair_found and duplicate_letters_found:
        return True
    else:
        return False


nice = 0
with open("input.txt") as file:
    for line in file:
        if is_nice(line):
            nice += 1

print(nice)
