import java.io.File

val startMap = File(".aoc/2024/6").readText().lines().map { it.toCharArray() }
val guardStartLine = startMap.indexOfFirst { it.contains('^') }
val guardStartPos = Pair(guardStartLine, startMap[guardStartLine].indexOf('^'))

class Guard(private val map: List<CharArray> = startMap) {
    var pos = guardStartPos
        private set
    private var dir = Pair(-1, 0)

    fun walk() {
        var nextPos = Pair(pos.first + dir.first, pos.second + dir.second)
        while (!isValidWalkingSpot(nextPos)) {
            dir = when (dir) {
                Pair(1, 0) -> Pair(0, -1)
                Pair(0, -1) -> Pair(-1, 0)
                Pair(-1, 0) -> Pair(0, 1)
                Pair(0, 1) -> Pair(1, 0)
                else -> throw Exception("Invalid direction")
            }
            nextPos = Pair(pos.first + dir.first, pos.second + dir.second)
        }
        pos = nextPos
    }

    fun isOutside(checkPosition: Pair<Int, Int> = pos): Boolean {
        return checkPosition.first < 0 || checkPosition.first >= map.size || checkPosition.second < 0 || checkPosition.second >= map[checkPosition.first].size
    }

    private fun isValidWalkingSpot(potentialPos: Pair<Int, Int>): Boolean {
        return isOutside(potentialPos) || map[potentialPos.first][potentialPos.second] != '#'
    }
}

private fun part1() {
    val walkedMap = startMap.map { line -> line.map { false }.toMutableList() }
    val guard = Guard()
    while (!guard.isOutside()) {
        walkedMap[guard.pos.first][guard.pos.second] = true
        guard.walk()
    }
    println(walkedMap.sumOf { line -> line.count { it } })
}

private fun checkForLoop(map: List<CharArray>): Boolean {
    val walkedMap = map.map { line -> line.map { false }.toMutableList() }
    val guard = Guard(map)
    while (!guard.isOutside()) {
        if (walkedMap[guard.pos.first][guard.pos.second]) {
            return true
        }
        walkedMap[guard.pos.first][guard.pos.second] = true
        guard.walk()
    }
    return false
}

private fun part2() {
    var loops = 0
    startMap.forEach { line ->
        line.forEachIndexed { index, c ->
            if (c == '.') {
                val map = startMap.map { it.copyOf() }
                map[guardStartLine][index] = '#'
                if (checkForLoop(map)) {
                    loops++
                }
            }
        }
    }
    println(loops)
}

fun main() {
    part1()
    part2()
}