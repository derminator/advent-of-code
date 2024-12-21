import java.io.File
import kotlin.math.abs

private fun readLists(): Pair<MutableList<Int>, MutableList<Int>> {
    val file = File(".aoc/2024/1")

    val leftList = mutableListOf<Int>()
    val rightList = mutableListOf<Int>()

    file.forEachLine {
        val parts = it.split("\\s+".toRegex())
        leftList.add(parts[0].toInt())
        rightList.add(parts[1].toInt())
    }
    return Pair(leftList, rightList)
}

fun part1() {
    val (leftList, rightList) = readLists()

    leftList.sort()
    rightList.sort()

    var sum = 0

    for (i in leftList.indices) {
        sum += abs(leftList[i] - rightList[i])
    }

    println(sum)
}

fun part2() {
    val (leftList, rightList) = readLists()
    var sum = 0

    for (item in leftList) {
        val occurrences = rightList.count { it == item }
        sum += occurrences * item
    }
    println(sum)
}

fun main() {
    part1()
    part2()
}