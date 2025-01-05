import java.io.File

val map = File(".aoc/2024/8").readLines().map { it.toCharArray() }

private fun part1() {
    val antennas = mutableMapOf<Char, MutableList<Pair<Int, Int>>>()
    map.forEachIndexed { i, row ->
        row.forEachIndexed { j, c ->
            if (c != '.') {
                val list = antennas.getOrPut(c) { mutableListOf() }
                list.add(Pair(i, j))
            }
        }
    }
    val antinodes = map.mapIndexed { i, row ->
        row.mapIndexed { j, _ ->
            antennas.values.any { list ->
                false // TODO
            }
        }
    }
    println(antinodes.sumOf { row -> row.count { it } })
}

fun main() {
    part1()
}