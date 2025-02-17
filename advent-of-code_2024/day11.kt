import java.io.File

var stones = File(".aoc/2024/11").readText().trim().split(" ").map { it.toLong() }.asSequence()

private fun changeStone(stone: Long): Sequence<Long> {
    if (stone == 0L) return sequenceOf(1)
    val stoneStr = stone.toString()
    return if (stoneStr.length % 2 == 0) sequenceOf(
        stoneStr.substring(0, stoneStr.length / 2).toLong(),
        stoneStr.substring(stoneStr.length / 2).toLong()
    )
    else sequenceOf(stone * 2024)
}

private fun part1() {
    repeat(25) {
        stones = stones.flatMap { changeStone(it) }
    }
    println(stones.count())
}

private fun part2() {
    repeat(50) {
        stones = stones.flatMap { changeStone(it) }
    }
    println(stones.fold(0L) { acc, _ -> acc + 1 })
}

fun main() {
    part1()
    part2()
}