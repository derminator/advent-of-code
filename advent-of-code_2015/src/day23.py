def hlf(value: int):
    return value // 2


def tpl(value: int):
    return value * 3


def inc(value: int):
    return value + 1


with open('../../.aoc/2015/23') as f:
    instructions = f.readlines()


class Program:
    def __init__(self, a_val: int):
        self.instruction_ptr = 0
        self.registers = {'a': a_val, 'b': 0}
        self.instruction_ptr = 0

    def _parse_instruction(self, instruction: str):
        instruction_jump = 1
        parts = instruction.strip().split(" ")
        instruction = parts[0]
        if instruction == "hlf":
            reg = parts[1]
            value = self.registers[reg]
            self.registers[reg] = hlf(value)
        elif instruction == "tpl":
            reg = parts[1]
            value = self.registers[reg]
            self.registers[reg] = tpl(value)
        elif instruction == "inc":
            reg = parts[1]
            value = self.registers[reg]
            self.registers[reg] = inc(value)
        elif instruction == "jmp":
            instruction_jump = int(parts[1])
        elif instruction == "jie":
            reg = parts[1][:-1]
            if self.registers[reg] % 2 == 0:
                instruction_jump = int(parts[2])
        elif instruction == "jio":
            reg = parts[1][:-1]
            if self.registers[reg] == 1:
                instruction_jump = int(parts[2])
        else:
            raise Exception(f"Unknown instruction: {instruction}")
        self.instruction_ptr += instruction_jump

    def run(self):
        while len(instructions) > self.instruction_ptr >= 0:
            self._parse_instruction(instructions[self.instruction_ptr])
        print(self.registers["b"])


part1 = Program(0)
part1.run()

part2 = Program(1)
part2.run()
