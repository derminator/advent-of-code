import java.io.File

val map = File(".aoc/2024/8").readLines().map { it.toCharArray() }

private fun part1() {
    val antennas = mapAntennas()
    val antinodes = map.mapIndexed { i, row ->
        row.mapIndexed { j, _ ->
            antennas.values.any { list ->
                // Check if list contains 2 points that are in line with (i,j) and one is twice as far as the other from (i,j)
                list.any { (x, y) ->
                    list.any { (x2, y2) ->
                        if ((x == x2 && y == y2) || (x == i && y == j) || (x2 == i && y2 == j)) {
                            false
                        } else {
                            val dx = x - i
                            val dy = y - j
                            val dx2 = x2 - i
                            val dy2 = y2 - j
                            val d1 = dx * dx + dy * dy
                            val d2 = dx2 * dx2 + dy2 * dy2
                            val dx3 = x - x2
                            val dy3 = y - y2
                            (d1 / 4 == d2 || d2 / 4 == d1) && (dx * dy2 == dx2 * dy && dx * dy3 == dx3 * dy)
                        }
                    }
                }
            }
        }
    }
    println(antinodes.sumOf { row -> row.count { it } })
}

private fun part2() {
    val antennas = mapAntennas()
    val antinodes = map.mapIndexed { i, row ->
        row.mapIndexed { j, _ ->
            antennas.values.any { list ->
                // Check if list contains 2 points that are in line with (i,j) and one is twice as far as the other from (i,j)
                list.any { (x, y) ->
                    list.any { (x2, y2) ->
                        if ((x == x2 && y == y2)) {
                            false
                        } else {
                            val dx = x - i
                            val dy = y - j
                            val dx2 = x2 - i
                            val dy2 = y2 - j
                            val dx3 = x - x2
                            val dy3 = y - y2
                            (dx * dy2 == dx2 * dy && dx * dy3 == dx3 * dy)
                        }
                    }
                }
            }
        }
    }
    println(antinodes.sumOf { row -> row.count { it } })
}

private fun mapAntennas(): MutableMap<Char, MutableList<Pair<Int, Int>>> {
    val antennas = mutableMapOf<Char, MutableList<Pair<Int, Int>>>()
    map.forEachIndexed { i, row ->
        row.forEachIndexed { j, c ->
            if (c != '.') {
                val list = antennas.getOrPut(c) { mutableListOf() }
                list.add(Pair(i, j))
            }
        }
    }
    return antennas
}

fun main() {
    part1()
    part2()
}