package day01

import getInputLines

fun main() {
    val entries = getInputLines(1).map { it.toInt() }
    val pair = findPair(entries)
    println("Part 1: ${pair.first * pair.second}")
    val triple = findTriple(entries)
    println("Part 2: ${triple.first * triple.second * triple.third}")
}

private fun findPair(entries: List<Int>): Pair<Int, Int> {
    entries.forEach { a ->
        (entries - a).forEach { b ->
            if (a + b == 2020) return a to b
        }
    }
    error("No pair found")
}

private fun findTriple(entries: List<Int>): Triple<Int, Int, Int> {
    entries.forEach { a -> (entries - a).forEach { b ->
        (entries - a - b).forEach { c ->
            if (a + b + c == 2020) return Triple(a, c, b)
        }
    }}
    error("No triple found")
}