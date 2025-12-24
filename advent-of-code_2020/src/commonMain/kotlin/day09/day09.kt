package day09

import getInputLines

private const val preambleSize = 25
private val input = getInputLines(9).map { it.toLong() }

private fun isValid(index: Int): Boolean {
    if (index < preambleSize) return true

    for (i in index - preambleSize until index) {
        for (j in i + 1 until index) {
            if (i != j && input[i] + input[j] == input[index]) return true
        }
    }
    return false
}

fun main() {
    val firstBad = input.indices.first { !isValid(it) }
    println("Part 1: ${input[firstBad]}")

    for (potentialStart in input.indices) {
        val set = mutableListOf<Long>()
        for (i in potentialStart until input.lastIndex + 1) {
            set.add(input[i])
            if (set.size >= 2 && set.sum() == input[firstBad]) {
                println("Part 2: ${set.min() + set.max()}")
                return
            }
        }
    }
}