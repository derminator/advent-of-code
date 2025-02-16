import java.io.File

val startingStones = File(".aoc/2024/11").readText().trim().split(" ").map { it.toLong() }

private fun changeStone(stone: Long): List<Long> {
    if (stone == 0L) return listOf(1)
    val stoneStr = stone.toString()
    return if (stoneStr.length % 2 == 0) listOf(
        stoneStr.substring(0, stoneStr.length / 2).toLong(),
        stoneStr.substring(stoneStr.length / 2).toLong()
    )
    else listOf(stone * 2024)
}

private fun part1() {
    var stones = startingStones
    repeat(25) {
        stones = stones.flatMap { changeStone(it) }
    }
    println(stones.size)
}

fun main() {
    part1()
}