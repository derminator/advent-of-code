with open('../../.aoc/2015/19') as f:
    replacements, molecule = f.read().split("\n\n", 2)

replacements = replacements.splitlines()
replacements = map(lambda x: x.split(" => "), replacements)

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
print(len(possible_molecules))
