import java.io.File

private val map = File(".aoc/2024/10").readLines().map { it.toCharArray() }
val trailHeads = mutableListOf<Pair<Int, Int>>().apply {
    map.forEachIndexed { y, row ->
        row.forEachIndexed { x, c ->
            if (c == '0') {
                add(x to y)
            }
        }
    }
}

private fun findNextPaths(current: Pair<Int, Int>): List<Pair<Int, Int>> {
    val (x, y) = current
    return listOf(
        x to y - 1,
        x + 1 to y,
        x to y + 1,
        x - 1 to y
    ).filter { (x, y) -> map.getOrNull(y)?.getOrNull(x) != null }.filter { (xt, yt) ->
        val c = map[yt][xt]
        c == map[y][x] + 1
    }
}

private fun getTrailHeadScore(trailHead: Pair<Int, Int>): Int {
    var paths = listOf(trailHead)
    for (i in 1..9) {
        paths = paths.flatMap { findNextPaths(it) }.distinct()
    }
    return paths.size
}

private fun getTrailHeadRating(trailHead: Pair<Int, Int>): Int {
    var paths = listOf(trailHead)
    for (i in 1..9) {
        paths = paths.flatMap { findNextPaths(it) }
    }
    return paths.size
}

private fun part1() {
    println(trailHeads.sumOf { getTrailHeadScore(it) })
}

private fun part2() {
    println(trailHeads.sumOf { getTrailHeadRating(it) })
}

fun main() {
    part1()
    part2()
}