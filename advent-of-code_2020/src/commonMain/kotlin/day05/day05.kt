package day05

import getInputLines

private fun String.findValue(max: Int, low: Char, high: Char): Int = fold(0..max) { acc, ch ->
    when(ch) {
        low -> acc.first..<(acc.last - (acc.last - acc.first) / 2)
        high -> (acc.first + (acc.last - acc.first) / 2 + 1)..acc.last
        else -> throw IllegalArgumentException("Invalid mapping character $ch")
    }
}.single()

private class BoardingPass(passText: String) {
    val row = passText.substring(0, 7).findValue(127, 'F', 'B')
    val column = passText.substring(7).findValue(7, 'L', 'R')
    val seatId = row * 8 + column
}

fun main() {
    val passes = getInputLines(5).map { BoardingPass(it) }
    println("Part 1: ${passes.maxOf { it.seatId }}")

    val missingSeat = run {
        val sortedPasses = passes.sortedBy { it.seatId }
        var prev = sortedPasses.first().seatId
        sortedPasses.drop(1).forEach {
            val expected = prev + 1
            if (it.seatId != expected) return@run expected
            prev = expected
        }
        throw IllegalStateException("No missing seat found")
    }
    println("Part 2: $missingSeat")
}