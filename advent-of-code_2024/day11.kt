import kotlinx.coroutines.runBlocking
import java.io.File

var stones = mutableMapOf<Long, Long>().apply {
    File(".aoc/2024/11").readText().trim().split(" ").map { it.toLong() }.forEachIndexed { _, stone ->
        this[stone] = this.getOrDefault(stone, 0L) + 1
    }
}

private fun changeStone(stone: Long): List<Long> {
    if (stone == 0L) return listOf(1)
    val stoneStr = stone.toString()
    return if (stoneStr.length % 2 == 0) listOf(
        stoneStr.substring(0, stoneStr.length / 2).toLong(),
        stoneStr.substring(stoneStr.length / 2).toLong()
    )
    else listOf(stone * 2024)
}

private fun blink() {
    val newStones = mutableMapOf<Long, Long>().apply {
        stones.forEach { (stone, count) ->
            changeStone(stone).forEach { newStone ->
                this[newStone] = this.getOrDefault(newStone, 0L) + count
            }
        }
    }
    stones = newStones
}

private fun countStones() {
    println(stones.values.sum())
}

private fun part1() {
    repeat(25) {
        blink()
    }
    countStones()
}

private fun part2() = runBlocking {
    repeat(50) {
        blink()
    }
    countStones()
}

fun main() {
    part1()
    part2()
}