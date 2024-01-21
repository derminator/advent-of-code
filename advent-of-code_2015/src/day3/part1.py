import hashlib

KEY = 'yzbqklnj'
number = 0

while True:
    coin = KEY + str(number)
    h = hashlib.md5(coin.encode()).hexdigest()
    if h.startswith('0' * 5):
        print(number)
        exit(0)
    number += 1
