package day02

import getInputLines

class PasswordPolicy(input: String) {
    private val min = input.substringBefore('-').toInt()
    private val max = input.substringBefore(" ").substringAfter('-').toInt()
    private val letter = input.substringAfter(" ").substringBefore(":").single()
    private val password = input.substringAfter(": ")

    fun isValid(): Boolean {
        return password.count { it == letter } in min..max
    }
}

fun main() {
    val passwords = getInputLines(2).map(::PasswordPolicy)
    println("Part 1: ${passwords.count { it.isValid() }}")
}