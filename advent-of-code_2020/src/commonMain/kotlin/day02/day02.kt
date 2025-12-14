package day02

import getInputLines

private class PasswordPolicy(input: String) {
    private val lower = input.substringBefore('-').toInt()
    private val upper = input.substringBefore(" ").substringAfter('-').toInt()
    private val letter = input.substringAfter(" ").substringBefore(":").single()
    private val password = input.substringAfter(": ")

    val isValidPart1: Boolean = password.count { it == letter } in lower..upper

    val isValidPart2: Boolean = (password[lower - 1] == letter) xor (password[upper - 1] == letter)
}

fun main() {
    val passwords = getInputLines(2).map(::PasswordPolicy)
    println("Part 1: ${passwords.count { it.isValidPart1 }}")
    println("Part 2: ${passwords.count { it.isValidPart2 }}")
}