def is_nice(text: str) -> bool:
    num_vowels = 0
    duplicate_letters = 0

    forbidden_strings = ["ab", "cd", "pq", "xy"]

    if any(bad_string in text for bad_string in forbidden_strings):
        return False

    prev = ""
    for char in text:
        if char in "aeiou":
            num_vowels += 1

        if char == prev:
            duplicate_letters += 1

        prev = char

    if num_vowels >= 3 and duplicate_letters >= 1:
        return True
    else:
        return False


nice = 0
with open("input.txt") as file:
    for line in file:
        if is_nice(line):
            nice += 1

print(nice)
