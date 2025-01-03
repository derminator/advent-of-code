import kotlinx.coroutines.*
import java.io.File

val startMap = File(".aoc/2024/6").readText().lines().map { it.toCharArray() }
val guardStartLine = startMap.indexOfFirst { it.contains('^') }
val guardStartPos = Pair(guardStartLine, startMap[guardStartLine].indexOf('^'))

class Guard(private val map: List<CharArray> = startMap) {
    var pos = guardStartPos
        private set
    var dir = Pair(-1, 0)
        private set

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

class Step(val pos: Pair<Int, Int>, val dir: Pair<Int, Int>) {
    override fun equals(other: Any?): Boolean {
        if (this === other) return true
        if (other !is Step) return false
        return pos == other.pos && dir == other.dir
    }

    override fun hashCode(): Int {
        var result = pos.hashCode()
        result = 31 * result + dir.hashCode()
        return result
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
    val walkedMap = ArrayList<Step>()
    val guard = Guard(map)
    while (!guard.isOutside()) {
        val step = Step(guard.pos, guard.dir)
        if (walkedMap.contains(step)) {
            return true
        }
        walkedMap.add(step)
        guard.walk()
    }
    return false
}

private fun part2() = runBlocking {
    val results = mutableListOf<Deferred<List<Boolean>>>()
    startMap.forEachIndexed { i, line ->
        results.add(async(Dispatchers.Default) {
            line.mapIndexed { j, c ->
                if (c == '.') {
                    val map = startMap.map { it.copyOf() }
                    map[i][j] = '#'
                    checkForLoop(map)
                } else {
                    false
                }
            }
        })
    }
    println(results.awaitAll().sumOf { line -> line.count { it } })
}

fun main() {
    part1()
    part2()
}