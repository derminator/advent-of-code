package day10

import getInputLines

private val adaptors = getInputLines(10).map { it.toInt() }.sorted()

fun main() {
    val differences = adaptors.zipWithNext { a, b -> b - a } + adaptors[0] + 3
    println("Part 1: ${differences.count { it == 1 } * differences.count { it == 3}}")
}