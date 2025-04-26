with open('../../.aoc/2015/19') as f:
    replacements, molecule = f.read().split("\n\n", 2)

replacements = replacements.splitlines()
replacements = list(map(lambda x: x.split(" => "), replacements))

# Part 1: Find all possible molecules after one replacement
possible_molecules = set()
for r in replacements:
    rule_in, rule_out = r
    start = 0
    while True:
        index = molecule.find(rule_in, start)
        if index == -1:
            break
        possible_molecules.add(
            molecule[:index] + rule_out + molecule[index + len(rule_in):]
        )
        start = index + 1
print("Part 1:", len(possible_molecules))

# Part 2: Find minimum steps to go from 'e' to the medicine molecule
# Reverse the replacements (we'll work backwards from the medicine to 'e')
reverse_replacements = [(rule_out, rule_in) for rule_in, rule_out in replacements]
# Sort by length of the 'from' part in descending order to replace longer sequences first
reverse_replacements.sort(key=lambda x: len(x[0]), reverse=True)

# Greedy approach: always replace the longest match first
current = molecule.strip()
steps = 0

while current != 'e':
    for rule_from, rule_to in reverse_replacements:
        if rule_from in current:
            # Replace only the first occurrence
            current = current.replace(rule_from, rule_to, 1)
            steps += 1
            break
    else:
        # If no replacement was made in this iteration, we're stuck
        print("No solution found")
        break

print("Part 2:", steps)
