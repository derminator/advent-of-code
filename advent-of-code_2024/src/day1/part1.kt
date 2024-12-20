package day1

import java.io.File
import kotlin.math.abs

fun main() {
    val file = File("input.txt")

    val leftList = mutableListOf<Int>()
    val rightList = mutableListOf<Int>()

    file.forEachLine {
        val parts = it.split("\\s+".toRegex())
        leftList.add(parts[0].toInt())
        rightList.add(parts[1].toInt())
    }

    leftList.sort()
    rightList.sort()

    var sum = 0

    for (i in leftList.indices) {
        sum += abs(leftList[i] - rightList[i])
    }

    println(sum)
}