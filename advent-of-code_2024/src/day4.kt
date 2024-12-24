import java.io.File

const val word = "XMAS"
val input = File(".aoc/2024/4").readLines()

private fun findNextLetter(pos: Int, prevLine: Int, prevCol: Int): List<Pair<Int, Int>> {
    val results = ArrayList<Pair<Int, Int>>()
    val targetSpots = arrayOf(
        Pair(prevLine-1, prevCol-1),
        Pair(prevLine-1, prevCol),
        Pair(prevLine-1, prevCol+1),
        Pair(prevLine, prevCol-1),
        Pair(prevLine, prevCol+1),
        Pair(prevLine+1, prevCol-1),
        Pair(prevLine+1, prevCol),
        Pair(prevLine+1, prevCol+1),
    )
    val searchLetter = word[pos]
    targetSpots.forEach {
        if (it.first in input.indices && it.second in input[it.first].indices && input[it.first][it.second] == searchLetter) {
            results.add(it)
        }
    }
    return results
}

private fun part1() {
    var total = 0
    for (i in input.indices) {
        for (j in input[i].indices) {
            if (input[i][j] == word[0]) {
                var possibleSolutions = ArrayList<Pair<Int, Int>>()
                possibleSolutions.add(Pair(i, j))
                for (k in 1 until word.length) {
                    possibleSolutions = ArrayList(possibleSolutions.flatMap { findNextLetter(k, it.first, it.second) })
                }
                total += possibleSolutions.size
            }
        }
    }
    println(total)
}

fun main() {
    part1()
}