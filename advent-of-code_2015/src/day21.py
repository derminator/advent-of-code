from typing import Optional

PLAYER_HP = 100

BOSS_ARMOR = 2

BOSS_DAMAGE = 8

BOSS_HP = 109


class Item:
    def __init__(self, name, cost, damage, armor):
        self.name = name
        self.cost = cost
        self.damage = damage
        self.armor = armor


class Loadout:
    def __init__(self, weapon: Item, armor: Optional[Item], ring1: Optional[Item], ring2: Optional[Item]):
        self.weapon = weapon
        self.armor = armor
        self.ring1 = ring1
        self.ring2 = ring2

    def cost(self):
        cost = 0
        for item in [self.weapon, self.armor, self.ring1, self.ring2]:
            if item is not None:
                cost += item.cost
        return cost

    def damage(self):
        damage = self.weapon.damage
        for item in [self.armor, self.ring1, self.ring2]:
            if item is not None:
                damage += item.damage
        return damage

    def defense(self):
        armor = self.weapon.armor
        for item in [self.armor, self.ring1, self.ring2]:
            if item is not None:
                armor += item.armor
        return armor

class Player:
    def __init__(self, hp, damage, armor):
        self.hp = hp
        self.damage = damage
        self.armor = armor


def simulate_battle(player1: Player, player2: Player) -> bool:
    player_turn = True
    while player1.hp > 0 and player2.hp > 0:
        if player_turn:
            player2.hp -= max(player1.damage - player2.armor, 1)
        else:
            player1.hp -= max(player2.damage - player1.armor, 1)
        player_turn = not player_turn
    return player1.hp > 0


WEAPONS = [
    Item("Dagger", 8, 4, 0),
    Item("Shortsword", 10, 5, 0),
    Item("Warhammer", 25, 6, 0),
    Item("Longsword", 40, 7, 0),
    Item("Greataxe", 74, 8, 0)
]

ARMOR = [
    Item("Leather", 13, 0, 1),
    Item("Chainmail", 31, 0, 2),
    Item("Splintmail", 53, 0, 3),
    Item("Bandedmail", 75, 0, 4),
    Item("Platemail", 102, 0, 5),
]

RINGS = [
    Item("Damage +1", 25, 1, 0),
    Item("Damage +2", 50, 2, 0),
    Item("Damage +3", 100, 3, 0),
    Item("Defense +1", 20, 0, 1),
    Item("Defense +2", 40, 0, 2),
    Item("Defense +3", 80, 0, 3)
]

possible_combos = []

for w in WEAPONS:
    for a in ARMOR + [None]:
        for r1 in RINGS + [None]:
            for r2 in RINGS + [None]:
                if r1 != r2 or (r1 is None and r2 is None):
                    possible_combos.append(Loadout(w, a, r1, r2))

possible_combos.sort(key=lambda x: x.cost())

for combo in possible_combos:
    if simulate_battle(Player(PLAYER_HP, combo.damage(), combo.defense()), Player(BOSS_HP, BOSS_DAMAGE, BOSS_ARMOR)):
        print(combo.cost())
        break

# Part 2
possible_combos.reverse()
for combo in possible_combos:
    if not simulate_battle(Player(PLAYER_HP, combo.damage(), combo.defense()),
                           Player(BOSS_HP, BOSS_DAMAGE, BOSS_ARMOR)):
        print(combo.cost())
        break
