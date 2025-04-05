import re
from itertools import combinations_with_replacement

pattern = re.compile(
    r'([A-z]+): capacity ([+-]?\d+), durability ([+-]?\d+), flavor ([+-]?\d+), texture ([+-]?\d+), calories ([+-]?\d+)')


class Ingredient:
    def __init__(self, name: str, capacity: int, durability: int, flavor: int, texture: int, calories: int):
        self.name = name
        self.capacity = capacity
        self.durability = durability
        self.flavor = flavor
        self.texture = texture
        self.calories = calories


class RecipePart:
    ingredient: Ingredient
    amount: int

    def __init__(self, ingredient: Ingredient, amount: int):
        self.ingredient = ingredient
        self.amount = amount


ingredients = []

with open('../../.aoc/2015/15') as f:
    for line in f:
        match = pattern.match(line)
        if match:
            ingredients.append(Ingredient(match.group(1), int(match.group(2)), int(match.group(3)), int(match.group(4)),
                                          int(match.group(5)), int(match.group(6))))


def calculate_score(recipe: list[RecipePart]) -> int:
    capacity = 0
    durability = 0
    flavor = 0
    texture = 0

    for part in recipe:
        capacity += part.ingredient.capacity * part.amount
        durability += part.ingredient.durability * part.amount
        flavor += part.ingredient.flavor * part.amount
        texture += part.ingredient.texture * part.amount

    if capacity < 0 or durability < 0 or flavor < 0 or texture < 0:
        return 0

    return capacity * durability * flavor * texture


best_score = 0

for amounts in combinations_with_replacement(range(len(ingredients)), 100):
    recipe = []
    for i in range(len(ingredients)):
        recipe.append(RecipePart(ingredients[i], amounts.count(i)))

    score = calculate_score(recipe)
    if score > best_score:
        best_score = score

print(best_score)
