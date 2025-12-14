package day01

import getInputLines

fun main() {
    val entries = getInputLines(1).map { it.toInt() }
    val pair = findPair(entries)
    println("Part 1: ${pair.first * pair.second}")
}

private fun findPair(entries: List<Int>): Pair<Int, Int> {
    entries.forEach { a ->
        (entries - a).forEach { b ->
            if (a + b == 2020) return a to b
        }
    }
    error("No pair found")
}