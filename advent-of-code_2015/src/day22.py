import heapq
from copy import deepcopy

SPELL_COSTS = {
    'magic_missile': 53,
    'drain': 73,
    'shield': 113,
    'poison': 173,
    'recharge': 229
}
PLAYER_STARTING_HP = 50
PLAYER_STARTING_MANA = 500
BOSS_STARTING_HP = 51
BOSS_DAMAGE = 9


class Game:
    def __init__(self):
        self.player_hp = PLAYER_STARTING_HP
        self.player_mana = PLAYER_STARTING_MANA
        self.boss_hp = BOSS_STARTING_HP
        self.players_turn = True
        self.effects = {
            'shield': 0,
            'poison': 0,
            'recharge': 0,
        }

    def boss_turn(self):
        damage = max(1, BOSS_DAMAGE - self.player_armor())
        self.player_hp -= damage

    def player_armor(self):
        return 7 if self.effects['shield'] > 0 else 0

    def apply_effects(self):
        for effect, turns in list(self.effects.items()):
            if turns > 0:
                if effect == "poison":
                    self.boss_hp -= 3
                elif effect == "recharge":
                    self.player_mana += 101
                self.effects[effect] = turns - 1

    def player_turn(self, spell):
        # Check if player has enough mana to cast the spell
        if spell not in SPELL_COSTS:
            raise ValueError(f"Unknown spell: {spell}")

        cost = SPELL_COSTS[spell]
        if self.player_mana < cost:
            raise ValueError(f"Not enough mana to cast {spell}")

        # Check if effect is already active
        if spell in ['shield', 'poison', 'recharge'] and self.effects[spell] > 0:
            raise ValueError(f"Effect {spell} is already active")

        # Apply the spell's effects
        if spell == 'magic_missile':
            self.boss_hp -= 4
        elif spell == 'drain':
            self.boss_hp -= 2
            self.player_hp += 2
        elif spell == 'shield':
            self.effects['shield'] = 6
        elif spell == 'poison':
            self.effects['poison'] = 6
        elif spell == 'recharge':
            self.effects['recharge'] = 5

        # Deduct the mana cost
        self.player_mana -= cost


def find_optimal_spell_sequence():
    # Priority queue for BFS - (total_mana_spent, game_state, spell_sequence)
    queue = [(0, Game(), [])]
    visited = set()

    while queue:
        mana_spent, game, spell_sequence = heapq.heappop(queue)

        # Skip if player is defeated
        if game.player_hp <= 0:
            continue

        # Skip if boss is already defeated
        if game.boss_hp <= 0:
            return spell_sequence, mana_spent

        # Create a state key to avoid revisiting identical states
        state_key = (
            game.player_hp,
            game.player_mana,
            game.boss_hp,
            tuple(sorted((k, v) for k, v in game.effects.items()))
        )

        if state_key in visited:
            continue

        visited.add(state_key)

        # Try each possible spell
        for spell, cost in SPELL_COSTS.items():
            # Skip if not enough mana or effect already active
            if (game.player_mana < cost or
                    (spell in ['shield', 'poison', 'recharge'] and game.effects[spell] > 0)):
                continue

            # Create a new game state
            new_game = deepcopy(game)

            # Apply effects at the start of player's turn
            new_game.apply_effects()

            # Cast the spell
            try:
                new_game.player_turn(spell)
            except ValueError:
                continue  # Skip invalid spells

            # Check if boss is defeated
            if new_game.boss_hp <= 0:
                return spell_sequence + [spell], mana_spent + cost

            # Apply effects at the start of boss's turn
            new_game.apply_effects()

            # Check if boss is defeated by effects
            if new_game.boss_hp <= 0:
                return spell_sequence + [spell], mana_spent + cost

            # Boss attacks
            new_game.boss_turn()

            # Add new state to queue if player is still alive
            if new_game.player_hp > 0:
                heapq.heappush(
                    queue,
                    (mana_spent + cost, new_game, spell_sequence + [spell])
                )

    return None, float('inf')  # No solution found


# Usage
optimal_sequence, min_mana = find_optimal_spell_sequence()
print(f"Optimal spell sequence: {optimal_sequence}")
print(f"Minimum mana required: {min_mana}")
