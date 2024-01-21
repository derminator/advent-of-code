import hashlib

KEY = 'yzbqklnj'


def find_coin(leading_zeros):
    number = 0
    prefix = '0' * leading_zeros
    while True:
        coin = KEY + str(number)
        h = hashlib.md5(coin.encode()).hexdigest()
        if h.startswith(prefix):
            return number
        number += 1
