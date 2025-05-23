current_password = "cqjxjnds"


def increment_password(password):
    password = list(password)
    for i in range(len(password) - 1, -1, -1):
        if password[i] == 'z':
            password[i] = 'a'
        else:
            password[i] = chr(ord(password[i]) + 1)
            break
    return ''.join(password)


def check_straight(password):
    for i in range(len(password) - 2):
        if ord(password[i]) == ord(password[i + 1]) - 1 and ord(password[i + 1]) == ord(password[i + 2]) - 1:
            return True
    return False


def check_pairs(password):
    pairs = set()
    for i in range(len(password) - 1):
        if password[i] == password[i + 1]:
            pairs.add(password[i])
    return len(pairs) >= 2


def validate_password(password):
    if 'i' in password or 'o' in password or 'l' in password:
        return False
    if not check_straight(password):
        return False
    if not check_pairs(password):
        return False
    return True


def find_next_password(password):
    while True:
        password = increment_password(password)
        if validate_password(password):
            return password


new_password = find_next_password(current_password)
print(new_password)
print(find_next_password(new_password))
