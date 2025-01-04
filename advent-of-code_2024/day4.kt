import java.io.File

const val word = "XMAS"
val input = File(".aoc/2024/4").readLines()

private fun checkForSolution(startPos: Pair<Int, Int>, vertDir: Int, horizDir: Int): Boolean {
    var nextPos = startPos
    for (i in 1 until word.length) {
        nextPos = Pair(nextPos.first + vertDir, nextPos.second + horizDir)
        if (nextPos.first !in input.indices || nextPos.second !in input[nextPos.first].indices || input[nextPos.first][nextPos.second] != word[i]) {
            return false
        }
    }
    return true
}

private fun part1() {
    var total = 0
    val options = arrayOf(
        Pair(-1, -1),
        Pair(-1, 0),
        Pair(-1, 1),
        Pair(0, -1),
        Pair(0, 1),
        Pair(1, -1),
        Pair(1, 0),
        Pair(1, 1)
    )
    for (i in input.indices) {
        for (j in input[i].indices) {
            if (input[i][j] == word[0]) {
                val results = options.map { checkForSolution(Pair(i, j), it.first, it.second) }
                total += results.count { it }
            }
        }
    }
    println(total)
}

private fun part2() {
    var total = 0
    for (i in input.indices) {
        for (j in input[i].indices) {
             if (input[i][j] == 'A' && i+1 in input.indices && j+1 in input[i+1].indices && i-1 in input.indices && j-1 in input[i-1].indices) {
                 if (((input[i-1][j-1] == 'M' && input[i+1][j+1] == 'S') ||
                             (input[i+1][j+1] == 'M' && input[i-1][j-1] == 'S')) &&
                     ((input[i-1][j+1] == 'M' && input[i+1][j-1] == 'S') || (input[i+1][j-1] == 'M' && input[i-1][j+1] == 'S'))) {
                     total += 1
                 }
             }
        }
    }
    println(total)
}

fun main() {
    part1()
    part2()
}