package day04

import getInput

private class Passport(input: String) {
    private val fields = input.split(" ", "\n", "\r\n").associate { field ->
        val list = field.split(":")
        list[0] to list[1]
    }

    val valid = fields.keys.containsAll(listOf("byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"))
}

private val passports = getInput(4).split(
    "\n\n", "\r\n\r\n"
).map(::Passport)

fun main() {
    println("Part 1: ${passports.count { it.valid }}")
}