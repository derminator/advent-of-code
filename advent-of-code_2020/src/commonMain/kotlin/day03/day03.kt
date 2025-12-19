package day03

import getInputLines

private val pattern = getInputLines(3).map { it.map { c -> c == '#' } }

private class MapTraverser(private val slope: Pair<Int, Int> = Pair(3, 1)) {
    private var currentRow = 0
    private var currentCol = 0
    var treesHit = 0L
        private set

    private fun moveForward() {
        currentCol = (currentCol + slope.first) % pattern[currentRow].size
        currentRow += slope.second
        if (currentRow < pattern.size && pattern[currentRow][currentCol]) treesHit++
    }

    private val atEnd get() = currentRow >= pattern.size

    fun moveToEnd() {
        while (!atEnd) moveForward()
    }
}

fun main() {
    val part1Map = MapTraverser()
    part1Map.moveToEnd()
    println("Part 1: ${part1Map.treesHit}")

    val part2Maps = setOf(Pair(1, 1), Pair(3, 1), Pair(5, 1), Pair(7, 1), Pair(1, 2)).map { MapTraverser(it) }
    part2Maps.forEach { it.moveToEnd() }
    println("Part 2: ${part2Maps.map { it.treesHit }.reduce { acc, i -> acc * i }}")
}