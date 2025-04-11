import re

gift_giver_traits = {
    "children": 3,
    "cats": 7,
    "samoyeds": 2,
    "pomeranians": 3,
    "akitas": 0,
    "vizslas": 0,
    "goldfish": 5,
    "trees": 3,
    "cars": 2,
    "perfumes": 1,
}

pattern = re.compile(r"Sue (\d+): ((?:\w+: \d+,? ?)+)")

with open("../../.aoc/2015/16") as f:
    for line in f:
        match = pattern.match(line)
        sue_number = int(match.group(1))
        traits = match.group(2).split(", ")
        traits = {trait.split(": ")[0]: int(trait.split(": ")[1]) for trait in traits}
        if all(traits[k] == gift_giver_traits[k] for k in traits):
            print(f"Part 1: {sue_number}")
            break
