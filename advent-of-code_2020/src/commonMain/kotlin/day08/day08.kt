package day08

import getInputLines

private class Program(private val instructions: List<Instruction>) {
    var accumulator: Int = 0
    private var instructionPointer: Int = 0
    private val executedInstructions = mutableSetOf<Int>()

    fun execute(instruction: Instruction) {
        when (instruction.operation) {
            "acc" -> accumulator += instruction.argument
            "jmp" -> {
                instructionPointer += instruction.argument
                return
            }

            "nop" -> {}
            else -> throw IllegalArgumentException("Invalid operation: ${instruction.operation}")
        }
        instructionPointer++
    }

    fun hasLoop(): Boolean {
        while (instructionPointer !in executedInstructions && instructionPointer < instructions.size) {
            executedInstructions.add(instructionPointer)
            execute(instructions[instructionPointer])
        }
        return instructionPointer < instructions.size
    }
}

private class Instruction(assemble: String) {
    val operation = assemble.substringBefore(" ")
    val argument = assemble.substringAfter(" ").toInt()
}

fun main() {
    val instructions = getInputLines(8).map(::Instruction)
    Program(instructions).let { program ->
        program.hasLoop()
        println("Part 1: ${program.accumulator}")
    }

    instructions.forEachIndexed { index, instruction ->
        if (instruction.operation == "nop") {
            val newInstructions = instructions.toMutableList()
            newInstructions[index] = Instruction("jmp ${instruction.argument}")
            Program(newInstructions).let { program ->
                if (!program.hasLoop()) {
                    println("Part 2: ${program.accumulator}")
                    return@forEachIndexed
                }
            }
        } else if (instruction.operation == "jmp") {
            val newInstructions = instructions.toMutableList()
            newInstructions[index] = Instruction("nop ${instruction.argument}")
            Program(newInstructions).let { program ->
                if (!program.hasLoop()) {
                    println("Part 2: ${program.accumulator}")
                    return@forEachIndexed
                }
            }
        }
    }
}