package day04

import getInput

private class Passport(input: String) {
    private val fields = input.split(" ", "\n", "\r\n").associate { field ->
        val list = field.split(":")
        list[0] to list[1]
    }

    val validPart1 = fields.keys.containsAll(listOf("byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"))

    val validPart2 = validPart1 && fields.all { (key, value) ->
        try {
            when (key) {
                "byr" -> value.toInt() in 1920..2002
                "iyr" -> value.toInt() in 2010..2020
                "eyr" -> value.toInt() in 2020..2030
                "hgt" -> {
                    val num = value.dropLast(2).toInt()
                    val unit = value.substring(value.length - 2)
                    when (unit) {
                        "cm" -> num in 150..193
                        "in" -> num in 59..76
                        else -> false
                    }
                }

                "hcl" -> value.startsWith("#") && value.drop(1).all { it in '0'..'9' || it in 'a'..'f' }
                "ecl" -> value in listOf("amb", "blu", "brn", "gry", "grn", "hzl", "oth")
                "pid" -> value.length == 9 && value.all { it in '0'..'9' }
                else -> true
            }
        } catch (_: Exception) {
            false
        }
    }
}

private val passports = getInput(4).split(
    "\n\n", "\r\n\r\n"
).map(::Passport)

fun main() {
    println("Part 1: ${passports.count { it.validPart1 }}")
    println("Part 2: ${passports.count { it.validPart2 }}")
}