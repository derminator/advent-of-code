TARGET = 33100000


def deliver_to_house(house_number: int) -> int:
    presents = 0
    # Only need to check up to the square root of house_number
    sqrt = int(house_number ** 0.5)

    for elf in range(1, sqrt + 1):
        if house_number % elf == 0:
            presents += elf * 10
            # Add the pair divisor if it's different
            pair = house_number // elf
            if pair != elf:
                presents += pair * 10

    return presents


def deliver_to_house_limited(house_number: int) -> int:
    presents = 0
    sqrt = int(house_number ** 0.5)

    for elf in range(1, sqrt + 1):
        if house_number % elf == 0:
            # Check if the elf visits this house (visits only 50 houses)
            if house_number // elf <= 50:
                presents += elf * 11

            # Add the pair divisor if different
            pair = house_number // elf
            if pair != elf and elf <= 50:  # Elf only visits 50 houses
                presents += pair * 11

    return presents


def find_min_house_with_gits(target: int, limited=False):
    delivery_algorithm = deliver_to_house_limited if limited else deliver_to_house
    house_number = 1
    while True:
        if delivery_algorithm(house_number) >= target:
            return house_number
        house_number += 1


print(find_min_house_with_gits(TARGET))
print(find_min_house_with_gits(TARGET, limited=True))
