package day08

import getInputLines

private var accumulator: Int = 0
private var instructionPointer: Int = 0
private val executedInstructions = mutableSetOf<Int>()

private class Instruction(assemble: String) {
    private val operation = assemble.substringBefore(" ")
    private val argument = assemble.substringAfter(" ").toInt()

    fun execute() {
        when (operation) {
            "acc" -> accumulator += argument
            "jmp" -> {
                instructionPointer += argument
                return
            }

            "nop" -> {}
            else -> throw IllegalArgumentException("Invalid operation: $operation")
        }
        instructionPointer++
    }
}

fun main() {
    val instructions = getInputLines(8).map(::Instruction)
    while (instructionPointer !in executedInstructions) {
        executedInstructions.add(instructionPointer)
        instructions[instructionPointer].execute()
    }
    println("Part 1: $accumulator")
}