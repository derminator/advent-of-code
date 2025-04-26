TARGET = 33100000


def deliver_to_house(house_number: int):
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


def find_min_house_with_gits(target: int):
    house_number = 1
    while True:
        if deliver_to_house(house_number) >= target:
            return house_number
        house_number += 1


print(find_min_house_with_gits(TARGET))
