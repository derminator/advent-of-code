import java.io.File

val map = File(".aoc/2024/6").readText().lines().map { it.toCharArray() }

private fun isValidWalkingSpot(pos: Pair<Int, Int>): Boolean {
    return isOutside(pos) || map[pos.first][pos.second] != '#'
}

private fun isOutside(pos: Pair<Int, Int>): Boolean {
    return pos.first < 0 || pos.first >= map.size || pos.second < 0 || pos.second >= map[pos.first].size
}

private fun part1() {
    val walkedMap = map.map { line -> line.map { false }.toMutableList() }
    val guardStartLine = map.indexOfFirst { it.contains('^') }
    var guardPos = Pair(guardStartLine, map[guardStartLine].indexOf('^'))
    var guardDir = Pair(-1, 0)
    while (!isOutside(guardPos)) {
        walkedMap[guardPos.first][guardPos.second] = true
        var nextPos = Pair(guardPos.first + guardDir.first, guardPos.second + guardDir.second)
        while (!isValidWalkingSpot(nextPos)) {
            guardDir = when (guardDir) {
                Pair(1, 0) -> Pair(0, -1)
                Pair(0, -1) -> Pair(-1, 0)
                Pair(-1, 0) -> Pair(0, 1)
                Pair(0, 1) -> Pair(1, 0)
                else -> throw Exception("Invalid direction")
            }
            nextPos = Pair(guardPos.first + guardDir.first, guardPos.second + guardDir.second)
        }
        guardPos = nextPos
    }
    println(walkedMap.sumOf { line -> line.count { it } })
}

fun main() {
    part1()
}