import java.io.File

val map = File(".aoc/2024/6").readText().lines().map { it.toCharArray() }

private fun part1() {
    val walkedMap = map.map { line -> line.map { it == '^' } }
    // TODO
    println(walkedMap.sumOf { line -> line.count { it } })
}

fun main() {
    part1()
}