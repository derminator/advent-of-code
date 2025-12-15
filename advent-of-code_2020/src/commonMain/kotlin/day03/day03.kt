package day03

import getInputLines

private object Map {
    private val pattern = getInputLines(3).map { it.map { c -> c == '#' } }
    private var currentRow = 0
    private var currentCol = 0
    var treesHit = 0

    fun moveForward() {
        currentCol = (currentCol + 3) % pattern[currentRow].size
        currentRow++
        if (currentRow < pattern.size && pattern[currentRow][currentCol]) treesHit++
    }

    val atEnd get() = currentRow >= pattern.size
}

fun main() {
    while (!Map.atEnd) Map.moveForward()
    println("Part 1: ${Map.treesHit}")
}